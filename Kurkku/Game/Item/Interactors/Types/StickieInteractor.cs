using Newtonsoft.Json;

namespace Kurkku.Game
{
    public class StickieInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public StickieInteractor(Item item) : base(item)
        {

        }

        public override object GetJsonObject()
        {
            return JsonConvert.DeserializeObject<StickieExtraData>(Item.Data.ExtraData);
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                var stickieData = (StickieExtraData)GetJsonObject();

                ExtraDataInventoryView = stickieData.Colour.ToString();
                ExtraData = stickieData.Colour.ToString();
            }

            return inventoryView ? ExtraDataInventoryView : ExtraData;
        }
    }
}
