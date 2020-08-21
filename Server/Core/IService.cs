using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core
{
    public interface IService
    {
        void Start();
        void Send(ISession session, Message message);
    }
}
