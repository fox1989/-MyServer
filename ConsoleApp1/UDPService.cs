using Google.Protobuf;
using Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class UDPService : Service
    {
        private Socket socket;

        public IPEndPoint iPEndPoint;

        public int maxConnect = 100;

        public Action<Session, byte[]> receiveMsg;
        public Action<string> onError;
        public Action<string> onConnect;
        public Action<string> onColse;


        private int currConnect = 0;
        //TODO: session回收
        public ConcurrentDictionary<string, Session> sessions = new ConcurrentDictionary<string, Session>();




        SocketAsyncEventArgsPool socketPool;
        public UDPService()
        {

        }


        public UDPService(string ip, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            iPEndPoint = new IPEndPoint(iPAddress, port);
        }



        public void Init()
        {

        }


        public class StateObject
        {
            public Socket workSocket = null;
            public const int BUFFER_SIZE = 1024*8;//1024;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public StringBuilder sb = new StringBuilder();
            public List<byte> datas = new List<byte>();
            public EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

        }


        public override void Start()
        {
            Init();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(iPEndPoint);
            //socket.Listen(10);

            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
            StateObject so = new StateObject();
            socket.BeginReceiveFrom(so.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, ref so.remote, new AsyncCallback(OnReceive), so);
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start();


        }



        void OnReceive(IAsyncResult asyncResult)
        {
            StateObject so = asyncResult.AsyncState as StateObject;
            //EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            int len = -1;
            try
            {

                len = socket.EndReceiveFrom(asyncResult, ref so.remote);

                L.i("Receive length:" + len);

                if (len > 0)
                {
                    byte[] bytes = new byte[len];
                    Array.Copy(so.buffer, bytes, len);
                    so.datas.AddRange(bytes);
                   
                }
                else
                {
                    if (so.datas.Count > 0)
                    {
                        if (receiveMsg != null)
                        {
                            Session session = new Session(this, so.remote, socket);

                            if (!sessions.TryAdd(session.SessionId, session))
                                L.i("add session fali: id:" + session.SessionId);

                            receiveMsg(session, so.datas.ToArray());
                            so.datas.Clear();
                        }
                    }
                }

                //RaiseDataReceived(so);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //TODO 处理异常
                //RaiseOtherException(so);
            }
            finally
            {
                socket.BeginReceiveFrom(so.buffer, 0, so.buffer.Length, SocketFlags.None,
                ref so.remote, new AsyncCallback(OnReceive), so);
            }


        }



        /// <summary>
        /// 异步的发送数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public void SendTo(EndPoint iPEnd, byte[] data)
        {
            try
            {
                socket.BeginSendTo(data, 0, data.Length, SocketFlags.None, iPEnd, new AsyncCallback(OnSendEnd), socket);
            }
            catch (Exception e)
            {

            }
        }


        void OnSendEnd(IAsyncResult result)
        {
            int i = ((Socket)result.AsyncState).EndSendTo(result);
            L.i("已发送：" + i);
        }




        public override void Send(Session session, Message message)
        {
            SendTo(session.iPEnd, message.ToByteArray());
        }



        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            Session s = e.UserToken as Session;
            // close the socket associated with the client
            try
            {
                s.socket.Shutdown(SocketShutdown.Send);
            }
            // throws if client process has already closed
            catch (Exception) { }
            s.socket.Close();

            e.UserToken = null;
            socketPool.Push(e);
            Interlocked.Decrement(ref currConnect);
        }

    }
}
