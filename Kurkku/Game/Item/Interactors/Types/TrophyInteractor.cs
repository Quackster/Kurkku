using Kurkku.Util.Extensions;
using System.Text;
using System.Text.Json;

namespace Kurkku.Game
{
    public class TrophyInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public TrophyInteractor(Item item) : base(item)
        {

        }

        public override object GetJsonObject()
        {
            return JsonSerializer.Deserialize<TrophyExtraData>(Item.Data.ExtraData);
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
