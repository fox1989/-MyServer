using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Server.Core
{

    public class MessageHandler
    {

        ISession session;
        Message message;

        public MessageHandler(ISession session, Message message)
        {
            this.session = session;
            this.message = message;
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="o"></param>
        public void Do(Object o)
        {

            Type t = Type.GetType("Server.Handlers." + message.Handler);
            Console.WriteLine(t);
            
            if (t != null)
            {
                object obj = Activator.CreateInstance(t);

                Dictionary<Type, object> pairs = message.ReadParam();

                MethodInfo method = t.GetMethod(message.Method, pairs.Keys.ToArray());
                if (method != null)
                    method.Invoke(obj, pairs.Values.ToArray());
            }
        }
    }
}
