using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server.Core
{
    public class TCPSession : ISession
    {

        private string sessionId;

        public SocketAsyncEventArgs args;
        public Socket socket;
        public IService service;

        public TCPSession(IService service, SocketAsyncEventArgs args, Socket socket)
        {
            sessionId = Guid.NewGuid().ToString();
            this.args = args;
            this.service = service;
            this.socket = socket;
        }

        public string SessionId => sessionId;

        public void Send(Message message)
        {
            service.Send(this, message);
        }
    }
}
