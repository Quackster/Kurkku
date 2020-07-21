using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class MoodlightInteractor : Interactor
    {
        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        #endregion

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
                var data = (MoodlightExtraData)GetJsonObject();
                var preset = data.Presets[data.CurrentPreset - 1];

                StringBuilder builder = new StringBuilder();
                builder.Append(data.Enabled ? 2 : 1);
                builder.Append(",");
                builder.Append(data.CurrentPreset);
                builder.Append(",");
                builder.Append(preset.IsBackground ? 2 : 1);
                builder.Append(",");
                builder.Append(preset.ColorCode);
                builder.Append(",");
                builder.Append(preset.ColorIntensity);

                NeedsExtraDataUpdate = false;
                ExtraData = builder.ToString();
            }

            return ExtraData;
        }
    }
}
