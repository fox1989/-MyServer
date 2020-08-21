using Google.Protobuf;
using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UDPTest();
        }




        static void TCPTest()
        {

            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            socketSend.Connect(ip, 10086);


            while (true)
            {
                string ss = Console.ReadLine();

                byte[] bytes = Encoding.Default.GetBytes(ss);

                Message message = new Message();
                message.Id = 0;
                message.Handler = "MainHandler";
                message.Method = "Say";
                Test tt = new Test();
                tt.Id = 222;
                tt.Name = "test";
                message.AddParam(tt);

                socketSend.Send(message.ToByteArray());

            }
        }


        static void UDPTest()
        {
            Dictionary<int, Socket> sendList = new Dictionary<int, Socket>();

            int port = 60852;

            for (int i = 0; i < 10; i++)
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");

                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socketSend.Connect(ip, 10086);

                Socket revice = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                revice.Bind(socketSend.LocalEndPoint);


                Thread thread = new Thread(new ParameterizedThreadStart(Reviec));
                thread.Start(revice);



                sendList.Add(port, socketSend);
                Thread.Sleep(100);
                port++;
            }


            while (true)
            {
                string ss = Console.ReadLine();

                byte[] bytes = Encoding.Default.GetBytes(ss);
                Message message = new Message();
                message.Id = 0;
                message.Handler = "MainHandler";
                message.Method = "Say";
                Test tt = new Test();
                tt.Id = 222;
                tt.Name = "test:" + ss;

                message.AddParam(tt);
                foreach (var item in sendList)
                {

                    message.Id = item.Key;

                    Thread.Sleep(100);
                    int iii = item.Value.Send(message.ToByteArray());
                    Console.WriteLine("send:" + iii);
                }
            }
        }







        static void Reviec(Object obj)
        {
            Socket ss = (Socket)obj;
            byte[] bytes = new byte[1024];
            Console.WriteLine(ss.LocalEndPoint);

            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                int i = ss.ReceiveFrom(bytes, ref endPoint);
                if (i > 0)
                {
                    byte[] bb = new byte[i];
                    Array.Copy(bytes, bb, i);
                    Message message = Message.Parser.ParseFrom(bb);
                    Console.WriteLine(message.Handler);

                }

            }
        }
    }
}
