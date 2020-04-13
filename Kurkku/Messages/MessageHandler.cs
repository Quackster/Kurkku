using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Messages.Incoming;
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

        private static readonly ILog m_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly MessageHandler Instance = new MessageHandler();

        #endregion

        #region Properties

        private Dictionary<short, MessageEvent> Events { get; }
        private Dictionary<string, short> Composers { get; }


        #endregion

        #region Constructors

        public MessageHandler()
        {
            Events = new Dictionary<short, MessageEvent>();
            Composers = new Dictionary<string, short>();
        }

        public void Load()
        {
            registerHandshake();
            registerMessenger();
            registerUser();
            registerNavigator();
            registerRoom();
            
            ResolveComposers();
        }

        /// <summary>
        /// Resolve composers, instead of assigning to every composer file, associate by file name instead
        /// </summary>
        public void ResolveComposers()
        {
            Type outgoingEventType = typeof(OutgoingEvents);

            var type = typeof(IMessageComposer);
            var messageComposers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p != type);

            foreach (var composer in messageComposers)
            {
                var composerName = composer.Name;
                var composerField = outgoingEventType.GetField(composerName);

                if (composerField != null)
                    Composers[composerName] = Convert.ToInt16(composerField.GetValue(null));
            }
        }

        /// <summary>
        /// Get composer id for type
        /// </summary>
        internal short? GetComposerId(IMessageComposer composer)
        {
            short header = 0;
            
            if (Composers.TryGetValue(composer.GetType().Name, out header))
                return header;

            return null;
        }


        /// <summary>
        /// Register handshake packets
        /// </summary>
        private void registerHandshake()
        {
            Events[IncomingEvents.VersionCheckMessageEvent] = new VersionCheckMessageEvent();
            Events[IncomingEvents.InitCryptoMessageEvent] = new InitCryptoMessageEvent();
            Events[IncomingEvents.GenerateSecretKeyMessageEvent] = new GenerateSecretKeyMessageEvent();
            Events[IncomingEvents.SSOTicketMessageEvent] = new SSOTicketMessageEvent();

        }
        
        /// <summary>
        /// Register messenger packets
        /// </summary>
        private void registerMessenger()
        {
            Events[IncomingEvents.InitMessengerMessageEvent] = new InitMessengerMessageEvent();
            Events[IncomingEvents.SearchMessengerEvent] = new SearchMessageEvent();
            Events[IncomingEvents.BuddyRequestMessengerEvent] = new BuddyRequestMessageEvent();
            Events[IncomingEvents.AcceptRequestsMessageEvent] = new AcceptRequestsMessageEvent();
            Events[IncomingEvents.DeclineRequestMessageEvent] = new DeclineRequestMessageEvent();
            Events[IncomingEvents.RemoveFriendMessageEvent] = new RemoveFriendMessageEvent();
            Events[IncomingEvents.InstantChatMessageEvent] = new InstantChatMessageEvent();
        }

        /// <summary>
        /// Register user packets
        /// </summary>
        private void registerUser()
        {
            Events[IncomingEvents.LandingViewMessageEvent] = new LandingViewMessageEvent();
            Events[IncomingEvents.UserInfoMessageEvent] = new UserInfoMessageEvent();
            Events[IncomingEvents.ScrGetUserInfoMessageEvent] = new ScrGetUserInfoMessageEvent();
        }

        /// <summary>
        /// Register navigator packets
        /// </summary>
        private void registerNavigator()
        {
            Events[IncomingEvents.PublicItemsMessageEvent] = new PublicItemsMessageEvent();
            Events[IncomingEvents.UserFlatsMessageEvent] = new UserFlatsMessageEvent();
            Events[IncomingEvents.UserFlatsCatsMessageEvent] = new UserFlatCatsMessageEvent();
            Events[IncomingEvents.CanCreateRoomMessageEvent] = new CanCreateRoomMessageEvent();
        }

        /// <summary>
        /// Register room packets
        /// </summary>
        private void registerRoom()
        {
            Events[IncomingEvents.OpenFlatConnectionMessageEvent] = new OpenFlatConnectionMessageEvent();
            Events[IncomingEvents.GoToFlatMessageEvent] = new GoToFlatMessageEvent();
            Events[IncomingEvents.GetFurnitureAliasesMessageEvent] = new GetFurnitureAliasesMessageEvent();
            Events[IncomingEvents.GetRoomEntryDataMessageEvent] = new GetRoomEntryDataMessageComposer();
            Events[IncomingEvents.QuitMessageEvent] = new QuitMessageEvent();
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
