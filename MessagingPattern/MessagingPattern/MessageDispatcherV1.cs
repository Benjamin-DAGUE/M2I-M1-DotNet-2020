using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MessagingPattern
{
    /// <summary>
    ///     Classe qui distribue des messages à des abonnés.
    ///     Cette implémentation va forcément provoquer une fuite mémoire car elle conserve une référence forte aux abonnés (par le callback) sans proposé une méthode de désabonnement.
    /// </summary>
    public static class MessageDispatcherV1
    {
        #region Fields

        /// <summary>
        ///     Dictionnaire des abonnées à un type de message spécifié.
        /// </summary>
        private readonly static Dictionary<Type, List<Action<Message>>> _Callbacks;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructeur static de la classe <see cref="MessageDispatcherV1"/>.
        /// </summary>
        static MessageDispatcherV1()
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

        #endregion
    }
}
