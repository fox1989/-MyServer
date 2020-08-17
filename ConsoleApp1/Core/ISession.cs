using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core
{
    public interface ISession
    {
        string SessionId { get; }

        void Send(Message message);

    }
}
