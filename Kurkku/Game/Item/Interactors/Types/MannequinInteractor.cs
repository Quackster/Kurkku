using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class MannequinInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringArrayData;

        public MannequinInteractor(Item item) : base(item) { }

        public override object GetJsonObject()
        {
            MannequinExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<MannequinExtraData>(Item.Data.ExtraData);
            }
            catch { }

            // Revert if any fields were left empty
            if (extraData != null)
            {
                if (string.IsNullOrEmpty(extraData.Gender) || string.IsNullOrEmpty(extraData.Figure) || string.IsNullOrEmpty(extraData.OutfitName))
                    extraData = null;
            }

            if (extraData == null)
            {
                /*[FloorItemUpdate] [8d6478866f292deeeb22b5f6f726e371]
                Incoming[3996, _-1w] <- [0][0][0]¾œÓ[13]¦[0][0]J[0][0][0][4][0][0][0][6][0][0][0][2][0][3]0.0[0][3]1.0[0][0][0][0][0][0][0][1][0][0][0][3][0][6]GENDER[0][1]m[0][6]FIGURE[0]Sch-876-1331-110.lg-280-110.sh-295-1331.ca-3217-91-91.wa-3263-110-92.cc-3007-109-109[0][11]OUTFIT_NAME[0]Concealed Guardÿÿÿÿ[0][0][0][2][1]g1Ò*/

                extraData = new MannequinExtraData()
                {
                    //"ch-3030-66.lg-275-64.ca-1806-73.cc-260-1408",
                    //"ch-875-1331-1331.lg-280-91.sh-295-1331.wa-3211-110-110.cc-3007-1331-1331"
                    //"hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80"
                    Figure = "lg-275-64.cc-260-62.ca-1806-73.ch-3030-62.hd-180-1.hr-100-61",
                    Gender = "M",
                    OutfitName = "Default Mannequin"
                };
            }

            return extraData;
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                var data = (MannequinExtraData)GetJsonObject();
                var values = new Dictionary<string, string>();

                values["GENDER"] = data.Gender;
                values["FIGURE"] = GenerateMannequinFigure(data.Figure);
                values["OUTFIT_NAME"] = data.OutfitName;

                NeedsExtraDataUpdate = false;
                ExtraData = values;
            }

            return ExtraData;
        }

        public override void OnInteract(IEntity entity, int requestData)
        {
            if (!(entity is Player))
                return;

            var room = entity.RoomEntity.Room;

            if (room == null)
                return;

            var player = (Player)entity;
            var mannequinData = (MannequinExtraData)GetJsonObject();

            var strippedFigureData = ExcludedFigureParts(player.Details.Figure);
            var newFigureData = GenerateMannequinFigure(mannequinData.Figure);
            var newFigure = string.Concat(strippedFigureData, ".", newFigureData);

            player.Details.Figure = newFigure;
            player.Details.Sex = mannequinData.Gender.ToLower();

            UserDao.Update(player.Details);

            player.Send(new UserChangeMessageComposer(-1, player.Details.Figure, player.Details.Sex, player.Details.Motto, player.Details.AchievementPoints));
            room.Send(new UserChangeMessageComposer(player.RoomEntity.InstanceId, player.Details.Figure, player.Details.Sex, player.Details.Motto, player.Details.AchievementPoints));
        }


        // ch-210-66.hr-100-61ch-210-66.sh-290-80.hd-180-1.lg-270-82
        private string GenerateMannequinFigure(string figure)
        {
            string newFigure = string.Empty;
            string[] figureParts = figure.Split('.');

            foreach (string figurePart in figureParts)
            {
                if (!figurePart.Contains("hr") && 
                    !figurePart.Contains("hd") && 
                    !figurePart.Contains("he") && 
                    !figurePart.Contains("ha") && 
                    !figurePart.Contains("ea") && 
                    !figurePart.Contains("fa"))
                {
                    newFigure += figurePart + ".";
                }
            }

            if (newFigure.Length == 0)
                return string.Empty;

            return newFigure.Substring(0, newFigure.Length - 1);
        }

        private string ExcludedFigureParts(string figure)
        {
            string newFigure = string.Empty;
            string[] figureParts = figure.Split('.');

            foreach (string figurePart in figureParts)
            {
                if (figurePart.Contains("hr") ||
                    figurePart.Contains("hd") ||
                    figurePart.Contains("he") ||
                    figurePart.Contains("ha") ||
                    figurePart.Contains("ea") ||
                    figurePart.Contains("fa"))
                {
                    newFigure += figurePart + ".";
                }
            }

            if (newFigure.Length == 0)
                return string.Empty;

            return newFigure.Substring(0, newFigure.Length - 1);
        }
    }
}
