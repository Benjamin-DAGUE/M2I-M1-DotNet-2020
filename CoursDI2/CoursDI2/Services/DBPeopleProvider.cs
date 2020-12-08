using CoursDI2.Absracts;
using CoursDI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI2.Services
{
    class DBPeopleProvider : IPeopleProvider
    {
        public IEnumerable<IPerson> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPerson> GetBestCustomers() => throw new NotImplementedException();
    }
}
