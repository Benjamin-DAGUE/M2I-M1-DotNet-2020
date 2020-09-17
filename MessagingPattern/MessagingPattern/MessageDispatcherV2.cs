using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MessagingPattern
{
    /// <summary>
    ///     Classe qui distribue des messages à des abonnés.
    ///     Cette implémentation corrige le problème de la V1 en proposant une méthode de désabonnement.
    ///     Cependant, si le désabonnement n'est pas fait, il y aura une fuite mémoire.
    /// </summary>
    public static class MessageDispatcherV2
    {
        #region Fields

        /// <summary>
        ///     Dictionnaire des abonnées à un type de message spécifié.
        /// </summary>
        private readonly static Dictionary<Type, List<Action<Message>>> _Callbacks;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructeur static de la classe <see cref="MessageDispatcherV2"/>.
        /// </summary>
        static MessageDispatcherV2()
        {
            _Callbacks = new Dictionary<Type, List<Action<Message>>>();
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
            //Si on a des abonnés pour ce type de message.
            if (_Callbacks.ContainsKey(typeof(T)))
            {
                foreach (Action<Message> callback in _Callbacks[typeof(T)])
                {
                    //On appel le callback de chaque abonné.
                    callback(message);
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
            //Si on a aucun abonné pour ce type
            if (!_Callbacks.ContainsKey(typeof(T)))
            {
                //On créer la liste des abonnés
                _Callbacks.Add(typeof(T), new List<Action<Message>>());
            }
            //Ajout de l'abonné à la liste des abonnés.
            _Callbacks[typeof(T)].Add(callback);
        }

        /// <summary>
        ///     Permet de se désabonner à un type de message.
        /// </summary>
        /// <typeparam name="T">Type de message pour lequel on souhaite se désabonner.</typeparam>
        /// <param name="callback">Méthode qui a été utilisée pour le rappel qui doit être oubliée par le Dispatcher.</param>
        public static void Unregister<T>(Action<Message> callback)
        {
            if (_Callbacks.ContainsKey(typeof(T)) && _Callbacks[typeof(T)].Contains(callback))
            {
                _Callbacks[typeof(T)].Remove(callback);
            }
        }

        #endregion
    }
}
