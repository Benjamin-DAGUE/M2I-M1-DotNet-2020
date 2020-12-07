using CoursDI2.Absracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI2.Views
{
    public class CustomerView : ICustomerView
    {
        IPeopleProvider _PeopleProvider;

        //public CustomerView(IPeopleProvider peopleProvider)
        //{
        //    this._PeopleProvider = peopleProvider;
        //}

        public CustomerView(IServiceProvider serviceProvider)
        {
            this._PeopleProvider = serviceProvider.GetService(typeof(IPeopleProvider)) as IPeopleProvider;
        }

        public void ShowAllCustomers()
        {
            foreach (IPerson person in this._PeopleProvider.GetAllCustomers())
            {
                Console.WriteLine(person.FirstName + " " + person.LastName);
            }
        }

        public void ShowBestCustomers()
        {
            foreach (IPerson person in this._PeopleProvider.GetBestCustomers())
            {
                Console.WriteLine(person.FirstName + " " + person.LastName);
            }
        }
    }
}
