using Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server
{
    public class MessageQueue
    {


        Dictionary<string, IHandler> keyValuePairs = new Dictionary<string, IHandler>();//TODO:



        Queue<MessageHandler> handlers = new Queue<MessageHandler>();

        int maxCount;

        bool isRun = false;

        public MessageQueue(int maxMessages, int maxThread)
        {
            ThreadPool.SetMaxThreads(maxThread, maxThread);
            maxCount = maxMessages;
        }



        public void Add(MessageHandler handler)
        {
            lock (handlers)
            {
                handlers.Enqueue(handler);
            }
        }


        public MessageHandler Get()
        {
            lock (handlers)
            {
                if (handlers.Count > 0)
                    return handlers.Dequeue();
                else
                    return null;
            }
        }


        public void Start()
        {
            isRun = true;
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }


        void Run()
        {
            while (true)
            {

                if (ThreadPool.ThreadCount >= maxCount)
                {
                    Thread.Sleep(10);
                    continue;
                }


                MessageHandler handler = Get();

                if (handler != null)
                {
                    ThreadPool.QueueUserWorkItem(handler.Do);
                   // Handler handler = new Handler(message,);

                }
                Thread.Sleep(10);
            }
        }


    }
}

