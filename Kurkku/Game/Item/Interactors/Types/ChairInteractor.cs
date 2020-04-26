using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Kurkku.Game
{
    public class ChairInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public ChairInteractor(Item item) : base(item)
        {

        }

        public virtual void OnStop(IEntity entity)
        {

        }
    }
}
