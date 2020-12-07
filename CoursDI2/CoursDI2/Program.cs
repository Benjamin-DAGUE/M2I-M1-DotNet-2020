using System;
using CoursDI2.Absracts;
using CoursDI2.Services;
using CoursDI2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CoursDI2
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IPeopleProvider>(sp => new FakePeopleProvider());

            //serviceCollection.AddTransient<ICustomerView>(serviceProvider => new CustomerView(serviceProvider.GetService<IPeopleProvider>()));
            serviceCollection.AddTransient<ICustomerView>(serviceProvider => new CustomerView(serviceProvider));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("-------------");
            serviceProvider.GetService<ICustomerView>().ShowAllCustomers();
            Console.WriteLine("-------------");
            serviceProvider.GetService<ICustomerView>().ShowBestCustomers();

            Console.ReadKey();
        }
    }
}
