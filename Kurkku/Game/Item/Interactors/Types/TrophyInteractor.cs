using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Kurkku.Game
{
    public class TrophyInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public TrophyInteractor(Item item) : base(item)
        {

        }

        public override object GetExtraData()
        {
            if (NeedsExtraDataUpdate)
            {
                NeedsExtraDataUpdate = false;
                var trophyData = JsonConvert.DeserializeObject<TrophyExtraData>(Item.Data.ExtraData);

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
