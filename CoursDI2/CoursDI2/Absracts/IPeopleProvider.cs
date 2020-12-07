using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI2.Absracts
{
    public interface IPeopleProvider
    {
        IEnumerable<IPerson> GetBestCustomers();

        IEnumerable<IPerson> GetAllCustomers();
    }
}
