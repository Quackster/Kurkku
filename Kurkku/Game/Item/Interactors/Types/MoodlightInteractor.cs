using Kurkku.Util.Extensions;
using Newtonsoft.Json;
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
            return JsonConvert.DeserializeObject<MoodlightExtraData>(Item.Data.ExtraData);
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
