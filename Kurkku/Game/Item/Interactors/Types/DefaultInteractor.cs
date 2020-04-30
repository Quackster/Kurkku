namespace Kurkku.Game
{
    public class DefaultInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public DefaultInteractor(Item item) : base(item) { }
    }
}
