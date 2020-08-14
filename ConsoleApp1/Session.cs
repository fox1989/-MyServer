﻿using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Session
    {

        private string sessionId;
        public SocketAsyncEventArgs args;
        public Socket socket;
        public Service service;
        public EndPoint iPEnd;

        public string SessionId { get => sessionId; }

        public Session(Service service, SocketAsyncEventArgs args, Socket socket)
        {
            sessionId = Guid.NewGuid().ToString();
            this.args = args;
            this.service = service;
            this.socket = socket;
        }


        public Session(Service service, EndPoint iPEnd, Socket socket)
        {
            sessionId = Guid.NewGuid().ToString();
            this.iPEnd = iPEnd;
            this.service = service;
            this.socket = socket;
        }






        public void Send(Message message)
        {
            service.Send(this, message);
        }


    }
}
