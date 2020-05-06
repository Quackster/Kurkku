using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class MoodlightInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public MoodlightInteractor(Item item) : base(item)
        {

        }

        public override object GetJsonObject()
        {
            MoodlightExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<MoodlightExtraData>(Item.Data.ExtraData);
            }
            catch { }

            if (extraData == null)
            {
                extraData = new MoodlightExtraData
                {
                    CurrentPreset = 1,
                    Presets = new List<MoodlightPresetData>
                            {
                                new MoodlightPresetData(),
                                new MoodlightPresetData(),
                                new MoodlightPresetData()
                            }
                };
            }

            return extraData;
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                NeedsExtraDataUpdate = false;
                ExtraData = "";
            }

            return ExtraData;
        }
    }
}
