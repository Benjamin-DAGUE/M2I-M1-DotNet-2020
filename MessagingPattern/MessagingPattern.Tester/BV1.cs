using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    class BV1
    {
        public BV1()
        {
            MessageDispatcherV1.Register<HelloWorldMessage>(HelloWorldMessageReceived);
        }

        private void HelloWorldMessageReceived(Message message)
        {
            Console.WriteLine("Message received in BV1");
        }
    }
}
