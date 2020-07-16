using Google.Protobuf;
using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            socketSend.Connect(ip, 10086);


            while (true)
            {
                string ss = Console.ReadLine();

                byte[] bytes = Encoding.Default.GetBytes(ss);

                Message message = new Message();
                message.Id = 0;
                // message.handler = "MainContorller";
                message.Method = "Say";
                message.Param.AddRange(new List<ByteString>() { ByteString.CopyFrom(bytes), ByteString.CopyFromUtf8("ddddd"), ByteString.CopyFromUtf8("bbbbb") });

                socketSend.Send(message.ToByteArray());

            }




        }
    }
}
