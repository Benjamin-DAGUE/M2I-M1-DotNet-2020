using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MessagingPattern
{
    /// <summary>
    ///     Classe qui distribue des messages à des abonnés.
    ///     Cette implémentation utilise une WeakReference pour ne pas conserver une référence forte vers l'abonnée.
    ///     Cette technique permet de ne pas avoir à gérer le désabonnement et évite les fuites mémoires.
    /// </summary>
    public static class MessageDispatcherV3
    {
        #region Fields

        /// <summary>
        ///     Dictionnaire de reférences faibles des abonnées à un type de message spécifié.
        /// </summary>
        private readonly static Dictionary<Type, List<WeakReference<Action<Message>>>> _Callbacks;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructeur static de la classe <see cref="MessageDispatcherV3"/>.
        /// </summary>
        static MessageDispatcherV3()
        {
            _Callbacks = new Dictionary<Type, List<WeakReference<Action<Message>>>>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Permet l'envoi d'un message aux abonnés.
        /// </summary>
        /// <typeparam name="T">Type de message à envoyer.</typeparam>
        /// <param name="message">Instance du message à envoyer.</param>
        public static void SendMessage<T>(T message)
            where T : Message
        {
            if (_Callbacks.ContainsKey(typeof(T)))
            {
                foreach (WeakReference<Action<Message>> weakRef in _Callbacks[typeof(T)].ToList())
                {
                    //On regarde si on arrive à obtenir le callback à travers la référence faible.
                    if (weakRef.TryGetTarget(out Action<Message> callback))
                    {
                        //Si on y arrive, l'instance existe toujours et peut être appelée.
                        callback(message);
                    }
                    else
                    {
                        //Si on arrive pas à obtenir l'instance, cela signifie que le GarbageCollector est passé
                        //et que l'instance cible a été collectée.
                        //On peut alors supprimer la WeakReference pour éviter de poluer la liste.
                        _Callbacks[typeof(T)].Remove(weakRef);
                    }
                }
            }
        }

        /// <summary>
        ///     Permet de s'abonner à un type de message.
        /// </summary>
        /// <typeparam name="T">Type de message pour lequel on souhaite s'abonner.</typeparam>
        /// <param name="callback">Méthode de rappel à appeler lors de la réception d'un message.</param>
        public static void Register<T>(Action<Message> callback)
        {
            if (!_Callbacks.ContainsKey(typeof(T)))
            {
                _Callbacks.Add(typeof(T), new List<WeakReference<Action<Message>>>());
            }
            //On créé une référence faible vers le callback.
            _Callbacks[typeof(T)].Add(new WeakReference<Action<Message>>(callback));
        }

        #endregion
    }
}
