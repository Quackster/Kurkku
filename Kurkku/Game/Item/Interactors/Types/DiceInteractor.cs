namespace Kurkku.Game
{
    public class DiceInteractor : Interactor
    {
        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override bool RequiresTick => false;

        #endregion

        public DiceInteractor(Item item) : base(item) { }

        public override void OnInteract(IEntity entity)
        {

        }
    }
}
