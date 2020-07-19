using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Kurkku.Game
{
    public class TrophyInteractor : Interactor
    {
        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override bool RequiresTick => false;

        #endregion

        public TrophyInteractor(Item item) : base(item)
        {

        }

        public override object GetJsonObject()
        {
            return JsonConvert.DeserializeObject<TrophyExtraData>(Item.Data.ExtraData);
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                NeedsExtraDataUpdate = false;
                var trophyData = (TrophyExtraData)GetJsonObject();

                StringBuilder builder = new StringBuilder();
                builder.Append(PlayerManager.Instance.GetName(trophyData.UserId));
                builder.Append((char)9);
                builder.Append(trophyData.Date.ToDateTime().ToString("dd-MM-yyyy"));
                builder.Append((char)9);
                builder.Append(trophyData.Message.FilterInput(false));
                ExtraData = builder.ToString();
            }

            return ExtraData;
        }
    }
}
