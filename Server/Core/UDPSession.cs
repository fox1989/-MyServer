using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Server.Core
{
    public class UDPSession : ISession
    {
        private string sessionId;

        public IService service;
        public EndPoint iPEnd;

        public UDPSession(IService service, EndPoint iPEnd)
        {
            sessionId = Guid.NewGuid().ToString();
            this.iPEnd = iPEnd;
            this.service = service;
        }


        public string SessionId => sessionId;

        public void Send(Message message)
        {
            service.Send(this, message);
        }
    }
}
