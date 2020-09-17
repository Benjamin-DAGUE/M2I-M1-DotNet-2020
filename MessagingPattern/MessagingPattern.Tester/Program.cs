using System;

namespace MessagingPattern.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            V1();
            V2();
            V3();
        }

        private static void V1()
        {
            Console.WriteLine("Hello World! V1");

            for (int i = 0; i < 5; i++)
            {
                BV1 b = new BV1();
            }
            //Dans la V1, les instances de B ne vont pas être collectées car le MessageDispatcherV1 conserve une référence forte vers ces instances.
            GC.Collect();

            //Ici, on va bien avoir les messages dans la console, ce qui prouve que les instances de BV1 existent toujours.
            AV1 a = new AV1();
            Console.ReadLine();
        }

        private static void V2()
        {
            Console.WriteLine("Hello World! V2");

            for (int i = 0; i < 5; i++)
            {
                BV2 b = new BV2();
                
                if (i % 2 == 0) //On va appeler le dispose que sur cetaines instances.
                {
                    b.Dispose();
                }
            }

            //Dans la V2, uniquement les instances de B pour lesquelles la méthode Dispose a été appelée vont être collectées
            //car le MessageDispatcherV2 conserve une référence forte vers les instances pour lesquelles on a oublié d'appeler le Dispose.
            GC.Collect();

            //Ici, on va bien avoir les messages dans la console, ce qui prouve que certaines instances de BV2 existent toujours.
            AV2 a = new AV2();
            Console.ReadLine();
        }

        private static void V3()
        {
            Console.WriteLine("Hello World! V3");

            for (int i = 0; i < 5; i++)
            {
                BV3 b = new BV3();
            }

            //Dans la V3, toute les instances vont bien être nettoyées.
            GC.Collect();

            //Ici, on a aucun message dans la console, ce qui prouve que toutes les instances de BV3.
            AV3 a = new AV3();
            Console.ReadLine();
        }
    }
}
