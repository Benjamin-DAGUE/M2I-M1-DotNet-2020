using CoursDI2.Absracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI2.Models
{
    public class Person : IPerson
    {
        public string FirstName { get ; set; }
        public string LastName { get; set; }
    }
}
