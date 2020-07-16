using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Server
{

    public class MessageHandler
    {

        Session session;
        MyMessage message;

        public MessageHandler(Session session, MyMessage message)
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
            IHandler handler = null;


            MethodInfo methodInfo = handler.GetType().GetMethod(message.method);


            if (methodInfo != null)
            {
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();

                methodInfo.Invoke(handler, new Object[] { });

            }
        }
    }
}
