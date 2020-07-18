namespace Kurkku.Game
{
    public class DiceInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public DiceInteractor(Item item) : base(item) { }

        public override void OnInteract(IEntity entity)
        {

        }
    }
}
