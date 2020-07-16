using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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



            TCPService service = new TCPService("127.0.0.1", 10086);

            service.receiveMsg = (Session session, byte[] bytes) =>
            {

                Message m = Message.Parser.ParseFrom(bytes);



                //MyMessage m = new MyMessage();
                //m.Read(bytes);

                string ss = Encoding.Default.GetString(m.Param[0].ToByteArray());

                Console.WriteLine("session:" + session.SessionId + "message:" + m.ToString()+" param:"+m.Param.Count);
                Console.WriteLine(ss);

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


    public class Service
    {
        public virtual void Start()
        {

        }



        public virtual void Send(SocketAsyncEventArgs sae, MyMessage message) { }

    }

}
