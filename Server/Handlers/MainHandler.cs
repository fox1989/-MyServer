using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Handlers
{
    class MainHandler : IHandler
    {

        public void Say(Test test)
        {
            Console.WriteLine("say:" + test);
        }

    }
}
