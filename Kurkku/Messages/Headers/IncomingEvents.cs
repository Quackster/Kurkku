namespace Kurkku.Messages.Headers
{
    public class IncomingEvents
    {
        public static readonly short VersionCheckMessageEvent = 4000;
        public static readonly short InitCryptoMessageEvent = 2332;
        public static readonly short GenerateSecretKeyMessageEvent = 249;
        public static readonly short SSOTicketMessageEvent = 2463;
        public static readonly short LandingViewMessageEvent = 927;
        public static readonly short InitMessengerMessageEvent = 1034;
        public static readonly short UserInfoMessageEvent = 2032;
        public static readonly short UserInfoMessageEvent2 = 2032;
        public static readonly short SearchMessengerMessageEvent = 2318;
        public static readonly short BuddyRequestMessageEvent = 381;
        public static readonly short AcceptRequestsMessageEvent = 2769;
        public static readonly short DeclineRequestMessageEvent = 762;
        public static readonly short RemoveFriendMessageEvent = 3100;
        public static readonly short InstantChatMessageEvent = 3292;
        public static readonly short OfficialRoomsMessageEvent = 3292;
        public static readonly short PublicItemsMessageEvent = 435;
        public static readonly short UserFlatsMessageEvent = 1218;
        public static readonly short UserFlatCatsMessageEvent = 2515;
        public static readonly short CanCreateRoomMessageEvent = 2507;
        public static readonly short ScrGetUserInfoMessageEvent = 2;
        public static readonly short OpenFlatConnectionMessageEvent = -2807;
        public static readonly short GetHabboGroupBadgesMessageEvent = 3181;
        public static readonly short GetFurnitureAliasesMessageEvent = 2576;
        public static readonly short GoToFlatMessageEvent = -3320;
        public static readonly short GetRoomEntryDataMessageEvent = -329;
        public static readonly short GetGuestRoomMessageEvent = -634;
        public static readonly short QuitMessageEvent = -549;
        public static readonly short PopularFlatsMessageEvent = 666;//512
        public static readonly short WalkMessageEvent = -2600;
        public static readonly short CreateRoomMessageEvent = 3025;//9
        public static readonly short GetCreditsMessageEvent = 1241;//2101
        public static readonly short ToggleRoomMuteMessageEvent = 1415;//574
        public static readonly short FollowFriendMessageEvent = 1206;//1177
        public static readonly short GetRoomSettingsMessageEvent = 456;//1102
        public static readonly short GetPublicRoomMessageEvent = -3897;
        public static readonly short LoadPublicRoomMessageEvent = -3583;
        public static readonly short OpenCatalogueMessageEvent = 41;//3071
        public static readonly short GetCataloguePageMessageEvent = 1481;
        public static readonly short GetPromotableRoomsMessageEvent = 1850;//225
        public static readonly short PurchaseItemMessageEvent = -1416;
        public static readonly short StartTypingMessageEvent = -678;
        public static readonly short StopTypingMessageEvent = -1236;
        public static readonly short ChatMessageMessageEvent = -2275;
        public static readonly short ShoutMessageMessageEvent = -1454;
        public static readonly short InventoryMessageEvent = 274;//2297
        public static readonly short PlaceItemMessageEvent = -2696;
        public static readonly short RemoveItemMessageEvent = -1019;
        public static readonly short MoveFloorItemMessageEvent = -1757;
        public static readonly short MoveWallItemMessageEvent = -1897;
        public static readonly short PlaceStickieMessageEvent = -361;
        public static readonly short GetStickieMessageEvent = -1797;
        public static readonly short UpdateStickieMessageEvent = -1561;
        public static readonly short DeleteStickieMessageEvent = -980;
        public static readonly short ApplyDecorationMessageEvent = 893;//2421
        public static readonly short CatalogueClubMessageEvent = 2150;//947
        public static readonly short CatalogueClubGiftsMessageEvent = 1937;//1259
        public static readonly short ChoseClubGiftMessageEvent = 1043;//1619
        public static readonly short GetMoodlightConfigEvent = -3264;
        public static readonly short ToggleMoodlightMessageEvent = -2017;
        public static readonly short SaveMoodlightMessageEvent = -2541;
        public static readonly short InteractItemMessageEvent = -3820;
    }
}
