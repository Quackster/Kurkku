using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Kurkku.Game
{
    public class BedInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public BedInteractor(Item item) : base(item)
        {

        }
    }
}
