using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    class BV3
    {
        public BV3()
        {
            MessageDispatcherV3.Register<HelloWorldMessage>(HelloWorldMessageReceived);
        }

        private void HelloWorldMessageReceived(Message message)
        {
            Console.WriteLine("Message received in BV3");
        }
    }
}
