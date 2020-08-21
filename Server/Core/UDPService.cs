using Google.Protobuf;
using Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.Core
{
    class UDPService : IService
    {
        private Socket socket;

        private Socket sendSocket;

        public IPEndPoint iPEndPoint;

        public int maxConnect = 100;

        public Action<ISession, byte[]> receiveMsg;

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
            public const int BUFFER_SIZE = 1024 * 8;//1024;8K
            public byte[] buffer = new byte[BUFFER_SIZE];
            public StringBuilder sb = new StringBuilder();
            public List<byte> datas = new List<byte>();
            public EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

        }


        public void Start()
        {
            Init();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(iPEndPoint);

            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10085));


            StateObject so = new StateObject();
            socket.BeginReceiveFrom(so.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, ref so.remote, new AsyncCallback(OnReceive), so);
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

                    if (receiveMsg != null)
                    {
                        ISession session = new UDPSession(this, so.remote);

                        receiveMsg(session, bytes);
                        so.datas.Clear();
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



                L.i("send to:" + iPEnd);



                sendSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, iPEnd, new AsyncCallback(OnSendEnd), sendSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        void OnSendEnd(IAsyncResult result)
        {
            int i = ((Socket)result.AsyncState).EndSendTo(result);
            L.i("已发送：" + i);
        }




        public void Send(ISession session, Message message)
        {
            UDPSession uDPSession = session as UDPSession;

            //IPEndPoint ip = uDPSession.iPEnd as IPEndPoint;

            //ip.Port = message.Id;


            //uDPSession.iPEnd = ip;
            SendTo(uDPSession.iPEnd, message.ToByteArray());
        }


    }
}
