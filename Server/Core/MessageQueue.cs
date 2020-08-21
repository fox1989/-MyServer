using Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.Core
{

    /// <summary>
    /// 消息队列
    /// </summary>
    public class MessageQueue
    {
        Queue<MessageHandler> handlers = new Queue<MessageHandler>();

        private int maxCount;
        private bool isRun = false;

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

        private Thread thread;
        public void Start()
        {
            isRun = true;
            thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        public void Puase()
        {
            isRun = false;
        }


        public void Play()
        {
            isRun = true;
        }


        public void Stop()
        {
            isRun = false;
            if (thread != null)
                thread.Abort();

            
        }

        void Run()
        {
            while (true)
            {

                if (ThreadPool.ThreadCount >= maxCount && isRun)
                {
                    Thread.Sleep(10);
                    continue;
                }

                MessageHandler handler = Get();

                if (handler != null)
                {
                    ThreadPool.QueueUserWorkItem(handler.Do);

                }
                Thread.Sleep(10);
            }
        }
    }
}

