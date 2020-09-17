using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingPattern.Tester
{
    /// <summary>
    ///     Cette implémentation utilise le Dispose pour déléguer le désabonnement à la classe appelante.
    /// </summary>
    class BV2 : IDisposable
    {
        public BV2()
        {
            MessageDispatcherV2.Register<HelloWorldMessage>(this.HelloWorldMessageReceived);
        }

        public void Dispose()
        {
            MessageDispatcherV2.Unregister<HelloWorldMessage>(this.HelloWorldMessageReceived);
            //Permet d'indiquer au GarbageCollector que la méthode Dispose a bien été appelée.
            //Si ce n'est pas le cas, le GC va appeler la méthode Dispose lors de la collecte.
            GC.SuppressFinalize(this);
        }

        private void HelloWorldMessageReceived(Message message)
        {
            Console.WriteLine("Message received in BV2");
        }
    }
}
