using Kurkku.Messages.Outgoing;
using Kurkku.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kurkku.Game
{
    public class PlayerTaskObject : ITaskObject
    {
        private class PlayerAttribute
        {
            public const string AFK_CHECK = "AFK_CHECK";
            public const string SPEECH_PATTERN = "SPEECH_PATTERN";
        }

        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public PlayerTaskObject(IEntity entity) : base(entity) { }

        #endregion

        #region Override Properties

        public override bool RequiresTick => true;

        #endregion

        #region Public methods

        public override void OnTick() { }
        public override void OnTickComplete()
        {
            if (!EventQueue.ContainsKey(PlayerAttribute.AFK_CHECK))
                QueueEvent(PlayerAttribute.AFK_CHECK, 1.0, new Dictionary<object, object>() { });

            TicksTimer = RoomTaskManager.GetProcessTime(0.5);
        }

        public override void ProcessQueuedEvent(QueuedEvent queuedEvent)
        {
            var player = (Player)Entity;

            switch (queuedEvent.EventName)
            {
                case PlayerAttribute.AFK_CHECK:
                    {
                        if (player.RoomUser.TimerManager.SpeechBubbleDate != -1 && DateUtil.GetUnixTimestamp() > player.RoomUser.TimerManager.SpeechBubbleDate)
                        {
                            player.RoomUser.TimerManager.ResetSpeechBubbleTimer();
                            player.RoomUser.Room.Send(new TypingStatusComposer(player.RoomUser.InstanceId, false));
                        }

                        break;
                    }
                case PlayerAttribute.SPEECH_PATTERN:
                    {
                        player.RoomEntity.Talk(ChatMessageType.CHAT, "test lol");

                        break;
                    }
            }
        }


        #endregion
    }
}
