using Google.Protobuf;
using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server.Core;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //UdpClient udpClient = new UdpClient("127.0.0.1", 10086);




            //EndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10086);
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //socket.Bind(point);
            ////socket.Listen(10);
            //while (true)
            //{
            //    Socket connectSocket = socket.Accept();
            //    if (connectSocket != null)
            //    {
            //        Console.WriteLine("connected:" + connectSocket.LocalEndPoint.ToString());
            //        GetMessage(connectSocket);


            //    }
            //}



            //Message message = new Message();
            //message.Id = 1;
            //message.Handler = "dddd";
            //message.AddParam("fox");




            //int length = CodedOutputStream.ComputeMessageSize(message);
            //byte[] bytes = new byte[length];
            //CodedOutputStream cos = new CodedOutputStream(bytes);
            //cos.WriteMessage(message);


            //Type tm = message.GetType();

            //Console.WriteLine(tm);





            //Type t = Message.GetTypeByName(tm.FullName);


            //Console.WriteLine(t + "    " + typeof(IMessage).IsAssignableFrom(t));

            //CodedInputStream cis = new CodedInputStream(bytes);


            //object obj = Activator.CreateInstance(t);



            //cis.ReadMessage((IMessage)obj);
            //Console.WriteLine(obj);


            MessageQueue messageQueue = new MessageQueue(100, 100);
            messageQueue.Start();


            // TCPService service = new TCPService("127.0.0.1", 10086);


            UDPService service = new UDPService("127.0.0.1", 10086);

            service.receiveMsg = (ISession session, byte[] bytes) =>
            {




                Message m = Message.Parser.ParseFrom(bytes);

                MessageHandler mh = new MessageHandler(session, m);
                messageQueue.Add(mh);

                Message mm = m.Clone();
                mm.Handler = "收到：" + ((UDPSession)session).iPEnd;


                session.Send(mm);
                ////MyMessage m = new MyMessage();
                ////m.Read(bytes);

                //string ss = Encoding.Default.GetString(m.Param[0].ToByteArray());

                //Console.WriteLine("session:" + session.SessionId + "message:" + m.ToString() + " param:" + m.Param.Count);
                //Console.WriteLine(ss);

            };

            service.Start();


            while (true)
            {
                Console.ReadKey();

            }


        }


        static void GetMessage(Socket socket)
        {
            byte[] bytes = new byte[1024 * 2];

            int count = socket.Receive(bytes);
            string message = Encoding.Default.GetString(bytes, 0, count);
            Console.WriteLine("message:" + message);

            byte[] bytes2 = Encoding.Default.GetBytes("惊喜：" + message);


            socket.Send(bytes2);
            socket.Close();
        }







    }
}
