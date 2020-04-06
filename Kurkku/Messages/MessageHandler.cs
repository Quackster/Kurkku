using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Messages.Incoming.Handshake;
using Kurkku.Messages.Incoming.Messenger;
using Kurkku.Network.Streams;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kurkku.Messages
{
    public class MessageHandler
    {
        #region Fields

        private static readonly ILog m_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<short, MessageEvent> m_Events;

        public static readonly MessageHandler Instance = new MessageHandler();

        #endregion

        #region Constructors

        /// <summary>
        /// Get the message handler instance
        /// </summary>
        public MessageHandler()
        {
            m_Events = new Dictionary<short, MessageEvent>();
            registerHandshake();
            registerMessenger();

        }


        /// <summary>
        /// Register handshake packets
        /// </summary>
        private void registerHandshake()
        {
            m_Events[IncomingEvents.VersionCheckMessageEvent] = new VersionCheckMessageEvent();
            m_Events[IncomingEvents.InitCryptoMessageEvent] = new InitCryptoMessageEvent();
            m_Events[IncomingEvents.GenerateSecretKeyMessageEvent] = new GenerateSecretKeyMessageEvent();
            m_Events[IncomingEvents.SSOTicketMessageEvent] = new SSOTicketMessageEvent();
            m_Events[IncomingEvents.LandingViewMessageEvent] = new LandingViewMessageEvent();

        }
        
        /// <summary>
        /// Register messenger packets
        /// </summary>
        private void registerMessenger()
        {
            m_Events[IncomingEvents.InitMessengerMessageEvent] = new InitMessengerMessageEvent();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Handler for incoming message
        /// </summary>
        /// <param name="player"></param>
        /// <param name="request"></param>
        public void HandleMesage(Player player, Request request)
        {
            try
            {
                if (m_Events.ContainsKey(request.Header))
                {
                    var message = m_Events[request.Header];
                    player.Log.Debug($"Message {message.GetType().Name}: {request.Header} / {request.MessageBody}");
                    message.Handle(player, request);
                } 
                else
                {
                    player.Log.Debug($"Unregistered message: {request.Header} / {request.MessageBody}");
                }
            }
            catch (Exception ex)
            {
                m_Log.Error("Error occurred: ", ex);
            }
        }

        #endregion
    }
}
