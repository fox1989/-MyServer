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

namespace Server.Core
{
    class TCPService : IService
    {
        private Socket socket;

        public IPEndPoint iPEndPoint;

        public int maxConnect = 100;

        public Action<ISession, byte[]> receiveMsg;
        public Action<string> onError;
        public Action<string> onConnect;
        public Action<string> onColse;


        private int currConnect = 0;
        //TODO: session回收
        public ConcurrentDictionary<string, ISession> sessions = new ConcurrentDictionary<string, ISession>();

        SocketAsyncEventArgsPool socketPool;
        public TCPService()
        {

        }


        public TCPService(string ip, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            iPEndPoint = new IPEndPoint(iPAddress, port);
        }



        public void Init()
        {
            socketPool = new SocketAsyncEventArgsPool(100);

            for (int i = 0; i < maxConnect; i++)
            {
                SocketAsyncEventArgs sae = new SocketAsyncEventArgs();
                sae.Completed += new EventHandler<SocketAsyncEventArgs>(IOCompleted);
                socketPool.Push(sae);

            }
        }


        public void Start()
        {
            Init();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iPEndPoint);
            socket.Listen(10);

            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();

            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            if (!socket.AcceptAsync(socketAsyncEventArgs))
                OnAccept(socketAsyncEventArgs);


            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start();
        }



        #region 得到连接
        void OnAcceptCompleted(object sender, SocketAsyncEventArgs eventArgs)
        {
            OnAccept(eventArgs);
        }

        void OnAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Socket acceptSocket = e.AcceptSocket;
                L.i("连接...");

                if (acceptSocket.Connected)
                {
                    Interlocked.Increment(ref currConnect);

                    SocketAsyncEventArgs sae = socketPool.Pop();

                    ISession session = new TCPSession(this, sae, acceptSocket);
                    if (!sessions.TryAdd(session.SessionId, session))
                        L.i("add session fali: id:" + session.SessionId);

                    sae.UserToken = session;
                    int bufferSize = 1024 * 4;
                    sae.SetBuffer(new byte[bufferSize], 0, bufferSize);

                    if (!acceptSocket.ReceiveAsync(sae))
                    {
                        OnReceive(sae);
                    }
                }
                else
                {
                    //colse
                    CloseClientSocket(e);
                }
            }
        }



        #endregion
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="e"></param>
        void OnReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if (e.BytesTransferred > 0)
                {
                    TCPSession session = (TCPSession)e.UserToken;
                    if (session.socket.Available == 0)
                    {
                        byte[] data = new byte[e.BytesTransferred];
                        Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                        if (receiveMsg != null)
                            receiveMsg(session, data);
                    }

                    if (!session.socket.ReceiveAsync(e))
                    {
                        OnReceive(e);
                    }
                }
            }
            else
            {
                //close
                CloseClientSocket(e);
            }
        }


        /// <summary>
        /// 异步的发送数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public void Send(SocketAsyncEventArgs e, byte[] data)
        {
            if (e.SocketError == SocketError.Success)
            {
                Socket s = e.AcceptSocket;//和客户端关联的socket
                if (s.Connected)
                {
                    Array.Copy(data, 0, e.Buffer, 0, data.Length);//设置发送数据
                    //e.SetBuffer(data, 0, data.Length); //设置发送数据

                    if (!s.SendAsync(e))//投递发送请求，这个函数有可能同步发送出去，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                    {
                        // 同步发送时处理发送完成事件
                        OnSendCompleted(e);
                    }
                    else
                    {
                        //close
                        CloseClientSocket(e);
                    }
                }
            }
        }


        public void Send(ISession session, Message message)
        {

            TCPSession tCPSession = session as TCPSession;

            Send(tCPSession.args, message.ToByteArray());
        }




        private void OnSendCompleted(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Socket s = (Socket)e.UserToken;
                //TODO
                if (!s.ReceiveAsync(e))
                {
                    OnReceive(e);
                }
            }
            else
            {
                //close
                CloseClientSocket(e);
            }
        }


        void IOCompleted(object sender, SocketAsyncEventArgs eventArgs)
        {
            if (eventArgs != null)
            {
                L.i("async :" + eventArgs.LastOperation.ToString());
                switch (eventArgs.LastOperation)
                {
                    case SocketAsyncOperation.Receive:
                        OnReceive(eventArgs);
                        break;
                    case SocketAsyncOperation.Accept:
                        OnAccept(eventArgs);
                        break;
                    case SocketAsyncOperation.Send:
                        OnSendCompleted(eventArgs);
                        break;
                    default:
                        throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                }
            }
        }



        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            TCPSession s = e.UserToken as TCPSession;
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
