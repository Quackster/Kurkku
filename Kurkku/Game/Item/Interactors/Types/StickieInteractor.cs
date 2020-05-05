using System.Text.Json;

namespace Kurkku.Game
{
    public class StickieInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public StickieInteractor(Item item) : base(item) { }

        public override object GetJsonObject()
        {
            return JsonSerializer.Deserialize<StickieExtraData>(Item.Data.ExtraData);
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                NeedsExtraDataUpdate = false;
                ExtraData = ((StickieExtraData)GetJsonObject()).Colour;
            }

            return ExtraData;
        }
    }
}
