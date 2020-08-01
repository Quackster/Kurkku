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

            if (extraData == null)
            {
                extraData = new MannequinExtraData()
                {
                    Figure = ".ch-210-1321.lg-285-92",
                    Gender = "m",
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
                NeedsExtraDataUpdate = false;

                var values = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(data.Gender) && !string.IsNullOrEmpty(data.Figure) && !string.IsNullOrEmpty(data.OutfitName))
                {
                    values["GENDER"] = data.Gender;
                    values["FIGURE"] = data.Figure;
                    values["OUTFIT_NAME"] = data.OutfitName;
                }

                ExtraData = values;
            }

            return ExtraData;
        }
    }
}
