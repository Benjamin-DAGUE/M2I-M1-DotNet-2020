using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateMethods
{
    //On peut également créer un délégué sans Action et Func
    //Action et Func sont une simplification de cette syntaxe
    delegate void MyDelegate(bool param);

    class Worker
    {
        private Action<bool> _Callback;
        public void DoWork(Action<bool> callback)
        {
            _Callback = callback;
            Task.Factory.StartNew(DoWorkInternal);
            //Il est possible de créer des méthodes anonymes.
            //Une méthode anonyme peut être créée inline avec la syntaxe suivante
            // (paramètre) => { corps }
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(2000);
            //    callback(true);
            //});
        }

        private void DoWorkInternal()
        {
            Thread.Sleep(2000);
            _Callback(true);
        }
    }

    class Worker2
    {
        public event EventHandler WorkEnded;

        public void DoWork()
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                this.WorkEnded?.Invoke(this, new EventArgs());
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            Console.WriteLine("Starting work");
            //DoWork attend un paramètre de type Action<bool>.
            //Action et Func sont des Delegate, c'est un type qui permet de passer une référence vers une méthode.
            //Action retourne void et Func retourne un type.
            //Action<bool> attend donc une référence d'une méthode qui prend un booléen en paramètre et retourne void.
            worker.DoWork(WorkEnded);
            Console.WriteLine("Work in progress...");

            Console.ReadLine();

            Console.WriteLine("Starting work2");

            Worker2 worker2 = new Worker2();
            //worker2.WorkEnded += Worker2_WorkEnded;
            worker2.WorkEnded += (sender, arg) => Console.WriteLine("Work2 ended..." + sender.ToString());
            worker2.DoWork();
            Console.WriteLine("Work2 in progress...");

            Console.ReadLine();

        }

        private static void Worker2_WorkEnded(object sender, EventArgs e)
        {
            Console.WriteLine("Work2 ended...");
        }

        private static void WorkEnded(bool allWasOK)
        {
            Console.WriteLine("Work ended");
        }

    }
}
