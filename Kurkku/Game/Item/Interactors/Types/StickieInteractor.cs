using Newtonsoft.Json;

namespace Kurkku.Game
{
    public class StickieInteractor : Interactor
    {
        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override bool RequiresTick => false;

        #endregion

        public StickieInteractor(Item item) : base(item) { }

        public override object GetJsonObject()
        {
            StickieExtraData extraData = null;

            try
            {
                extraData =  JsonConvert.DeserializeObject<StickieExtraData>(Item.Data.ExtraData);
            } catch { }

            if (extraData == null)
            {
                extraData = new StickieExtraData
                {
                    Message = string.Empty,
                    Colour = "FFFF33"
                };
            }

            return extraData;
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
