using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    class AV3
    {
        public AV3() => MessageDispatcherV3.SendMessage(new HelloWorldMessage());
    }
}
