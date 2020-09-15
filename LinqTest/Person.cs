using System;
using System.Collections.Generic;
using System.Text;

namespace LinqTest
{
    class Person : Object
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() => this.LastName + " " + this.FirstName;
    }
}
