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

            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            socketSend.Connect(ip, 10086);

            Thread thread = new Thread(new ParameterizedThreadStart(Reviec));
            thread.Start(socketSend);

            Thread.Sleep(100);

            byte[] bb = new byte[1];
            int bbb = socketSend.Send(bb);
            Console.WriteLine("send:" + bbb);


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


                for (int i = 0; i < 1024; i++)
                {
                    tt.Name += "test:" + ss;
                }


                message.AddParam(tt);


                int iii = socketSend.Send(message.ToByteArray());
                Console.WriteLine("send:" + iii);
            }
        }



        static void Reviec(Object obj)
        {
            Socket ss = (Socket)obj;
            byte[] bytes = new byte[1024];

            while (true)
            {

                int i = ss.Receive(bytes);
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
