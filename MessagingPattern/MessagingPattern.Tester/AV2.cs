using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    class AV2
    {
        public AV2() => MessageDispatcherV2.SendMessage(new HelloWorldMessage());
    }
}
