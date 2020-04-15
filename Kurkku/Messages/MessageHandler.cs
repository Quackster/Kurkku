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
            RegisterHandshake();
            RegisterMessenger();
            RegisterUser();
            RegisterNavigator();
            RegisterRoom();
            RegisterRoomUser();
            registerRoomSettings();
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
                var composerField = outgoingEventType.GetField(composer.Name);

                if (composerField != null)
                    Composers[composer.Name] = Convert.ToInt16(composerField.GetValue(null));
                else
                    log.Error($"Composer {composer.Name} has no header defined");
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
        /// Register handshake packets
        /// </summary>
        private void RegisterHandshake()
        {
            Events[IncomingEvents.VersionCheckMessageEvent] = new VersionCheckMessageEvent();
            Events[IncomingEvents.InitCryptoMessageEvent] = new InitCryptoMessageEvent();
            Events[IncomingEvents.GenerateSecretKeyMessageEvent] = new GenerateSecretKeyMessageEvent();
            Events[IncomingEvents.SSOTicketMessageEvent] = new SSOTicketMessageEvent();

        }
        
        /// <summary>
        /// Register messenger packets
        /// </summary>
        private void RegisterMessenger()
        {
            Events[IncomingEvents.InitMessengerMessageEvent] = new InitMessengerMessageEvent();
            Events[IncomingEvents.SearchMessengerEvent] = new SearchMessageEvent();
            Events[IncomingEvents.BuddyRequestMessengerEvent] = new BuddyRequestMessageEvent();
            Events[IncomingEvents.AcceptRequestsMessageEvent] = new AcceptRequestsMessageEvent();
            Events[IncomingEvents.DeclineRequestMessageEvent] = new DeclineRequestMessageEvent();
            Events[IncomingEvents.RemoveFriendMessageEvent] = new RemoveFriendMessageEvent();
            Events[IncomingEvents.InstantChatMessageEvent] = new InstantChatMessageEvent();
            Events[IncomingEvents.FollowFriendMessageEvent] = new FollowFriendMessageEvent();
        }

        /// <summary>
        /// Register user packets
        /// </summary>
        private void RegisterUser()
        {
            Events[IncomingEvents.LandingViewMessageEvent] = new LandingViewMessageEvent();
            Events[IncomingEvents.UserInfoMessageEvent] = new UserInfoMessageEvent();
            Events[IncomingEvents.ScrGetUserInfoMessageEvent] = new ScrGetUserInfoMessageEvent();
            Events[IncomingEvents.GetCreditsMessageEvent] = new GetCreditsMessageEvent();
        }

        /// <summary>
        /// Register navigator packets
        /// </summary>
        private void RegisterNavigator()
        {
            Events[IncomingEvents.PublicItemsMessageEvent] = new PublicItemsMessageEvent();
            Events[IncomingEvents.UserFlatsMessageEvent] = new UserFlatsMessageEvent();
            Events[IncomingEvents.UserFlatsCatsMessageEvent] = new UserFlatCatsMessageEvent();
            Events[IncomingEvents.CanCreateRoomMessageEvent] = new CanCreateRoomMessageEvent();
            Events[IncomingEvents.PopularFlatsMessengerEvent] = new PopularFlatsMessengerEvent();
            Events[IncomingEvents.CreateRoomMessageEvent] = new CreateRoomMessageEvent();
        }

        /// <summary>
        /// Register room packets
        /// </summary>
        private void RegisterRoom()
        {
            Events[IncomingEvents.OpenFlatConnectionMessageEvent] = new OpenFlatConnectionMessageEvent();
            Events[IncomingEvents.GoToFlatMessageEvent] = new GoToFlatMessageEvent();
            Events[IncomingEvents.GetFurnitureAliasesMessageEvent] = new GetFurnitureAliasesMessageEvent();
            Events[IncomingEvents.GetRoomEntryDataMessageEvent] = new GetRoomEntryDataMessageComposer();
            Events[IncomingEvents.QuitMessageEvent] = new QuitMessageEvent();
            Events[IncomingEvents.WalkMessageEvent] = new WalkMessageEvent();
            Events[IncomingEvents.GetGuestRoomMessageEvent] = new GetGuestRoomMessageEvent();
        }

        /// <summary>
        /// Register room packets
        /// </summary>
        private void RegisterRoomUser()
        {
            Events[IncomingEvents.WalkMessageEvent] = new WalkMessageEvent();
        }

        /// <summary>
        /// Register room setting packets
        /// </summary>
        private void registerRoomSettings()
        {
            Events[IncomingEvents.ToggleRoomMuteMessageEvent] = new ToggleRoomMuteMessageEvent();
            Events[IncomingEvents.GetRoomSettingsMessageEvent] = new GetRoomSettingsMessageEvent();
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
