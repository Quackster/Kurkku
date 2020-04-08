using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Headers
{
    public class IncomingEvents
    {
        public static readonly short VersionCheckMessageEvent = 4000;
        public static readonly short InitCryptoMessageEvent = 1266;
        public static readonly short GenerateSecretKeyMessageEvent = 3987;
        public static readonly short SSOTicketMessageEvent = 1461;
        public static readonly short LandingViewMessageEvent = 839;
        public static readonly short InitMessengerMessageEvent = 3621;
        public static readonly short UserInfoMessageEvent = 2671;
        public static readonly short SearchMessengerEvent = 1903;
        public static readonly short BuddyRequestMessengerEvent = 202;
    }
}
