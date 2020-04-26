namespace Kurkku.Game
{
    public class StickieInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public StickieInteractor(Item item) : base(item)
        {

        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {

            }

            return "";
        }
    }
}
