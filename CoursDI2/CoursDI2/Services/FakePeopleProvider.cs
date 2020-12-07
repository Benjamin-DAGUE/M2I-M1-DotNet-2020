using CoursDI2.Absracts;
using CoursDI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI2.Services
{
    class FakePeopleProvider : IPeopleProvider
    {
        public IEnumerable<IPerson> GetAllCustomers()
        {
            return new List<IPerson>()
            {
                new Person()
                {
                    FirstName = "Benjamin",
                    LastName = "DAGUÉ"
                },
                new Person()
                {
                    FirstName = "Peter",
                    LastName = "BAUDRY"
                },
                new Person()
                {
                    FirstName = "Jérémy",
                    LastName = "BOISSIERE"
                }
            };
        }

        public IEnumerable<IPerson> GetBestCustomers() => this.GetAllCustomers().Take(2);
    }
}
