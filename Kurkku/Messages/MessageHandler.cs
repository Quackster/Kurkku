using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Messages.Incoming;
using Kurkku.Messages.Incoming.Catalogue;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kurkku.Messages
{
    public class MessageHandler : ILoadable
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly MessageHandler Instance = new MessageHandler();

        #endregion

        #region Properties

        private Dictionary<short, IMessageEvent> Events { get; }
        private Dictionary<string, short> Composers { get; }


        #endregion

        #region Constructors

        public MessageHandler()
        {
            Events = new Dictionary<short, IMessageEvent>();
            Composers = new Dictionary<string, short>();
        }

        public void Load()
        {
            ResolveMessages();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Resolve events, instead of assigning to every event file, associate by file name instead
        /// </summary>
        public void ResolveMessages()
        {
            Type incomingEventType = typeof(IncomingEvents);
            Type outgoingEventType = typeof(OutgoingEvents);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                (typeof(IMessageEvent).IsAssignableFrom(p) && p != typeof(IMessageEvent)) || 
                (typeof(IMessageComposer).IsAssignableFrom(p) && p != typeof(IMessageComposer)));

            foreach (var packetType in types)
            {
                if (typeof(IMessageEvent).IsAssignableFrom(packetType)) {
                    var incomingField = incomingEventType.GetField(packetType.Name);

                    if (incomingField != null)
                        Events[Convert.ToInt16(incomingField.GetValue(null))] = (IMessageEvent)Activator.CreateInstance(packetType);
                    else
                        log.Error($"Event {packetType.Name} has no header defined");
                }

                if (typeof(IMessageComposer).IsAssignableFrom(packetType)) {
                    var composerField = outgoingEventType.GetField(packetType.Name);

                    if (composerField != null)
                        Composers[packetType.Name] = Convert.ToInt16(composerField.GetValue(null));
                    else
                        log.Error($"Composer {packetType.Name} has no header defined");
                }
                /**/
            }
        }

        /// <summary>
        /// Get composer id for type
        /// </summary>
        internal short? GetComposerId(IMessageComposer composer)
        {
            short header;

            if (Composers.TryGetValue(composer.GetType().Name, out header))
                return header;

            return null;
        }

        /// <summary>
        /// Handler for incoming message
        /// </summary>
        /// <param name="player"></param>
        /// <param name="request"></param>
        public void HandleMesage(Player player, Request request)
        {
            try
            {
                if (Events.ContainsKey(request.Header))
                {
                    var message = Events[request.Header];

                    // Not allowed to handle once logged in
                    if (!player.Authenticated &&
                        !(message is VersionCheckMessageEvent ||
                            message is InitCryptoMessageEvent ||
                            message is GenerateSecretKeyMessageEvent ||
                            message is SSOTicketMessageEvent))
                    {
                        player.Connection.Channel.CloseAsync();
                        return;
                    }

                    // Only allowed to handle when NOT logged in
                    if (player.Authenticated &&
                        (message is VersionCheckMessageEvent ||
                            message is InitCryptoMessageEvent ||
                            message is GenerateSecretKeyMessageEvent ||
                            message is SSOTicketMessageEvent))
                    {
                        player.Connection.Channel.CloseAsync();
                        return;
                    }

                    player.Log.Debug($"RECEIVED {message.GetType().Name}: {request.Header} / {request.MessageBody}");
                    message.Handle(player, request);
                } 
                else
                {
                    player.Log.Debug($"Unknown: {request.Header} / {request.MessageBody}");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error occurred: ", ex);
            }
        }

        #endregion
    }
}
