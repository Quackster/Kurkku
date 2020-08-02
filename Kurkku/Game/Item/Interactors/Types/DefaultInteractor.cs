namespace Kurkku.Game
{
    public class DefaultInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public DefaultInteractor(Item item) : base(item) { }

        public override void OnInteract(IEntity entity, int requestData)
        {
            if (Item.Definition.Data.MaxStatus > 0)
            {
                int.TryParse(Item.Data.ExtraData, out int currentMode);

                int newMode = currentMode + 1;

                if (newMode >= Item.Definition.Data.MaxStatus)
                    newMode = 0;

                Item.UpdateState(newMode.ToString());
                Item.Save();
            }
        }
    }
}
