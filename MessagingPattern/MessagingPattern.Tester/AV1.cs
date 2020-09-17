using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    class AV1
    {
        public AV1() => MessageDispatcherV1.SendMessage(new HelloWorldMessage());
    }
}
