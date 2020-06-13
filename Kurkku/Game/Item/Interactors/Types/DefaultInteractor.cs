namespace Kurkku.Game
{
    public class DefaultInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public DefaultInteractor(Item item) : base(item) { }

        public override void OnInteract(IEntity entity)
        {
            /*
             if (item.getDefinition().getMaxStatus() > 0) {
            int currentMode = StringUtils.isNumeric(item.getCustomData()) ? Integer.valueOf(item.getCustomData()) : 0;
            int newMode = currentMode + 1;

            if (newMode >= item.getDefinition().getMaxStatus()) {
                newMode = 0;
            }


            item.setCustomData(String.valueOf(newMode));
            item.updateStatus();
            item.save();
        }*/

            if (Item.Definition.Data.MaxStatus > 0)
            {
                int currentMode;
                int.TryParse(Item.Data.ExtraData, out currentMode);

                int newMode = currentMode + 1;

                if (newMode >= Item.Definition.Data.MaxStatus)
                    newMode = 0;

                Item.UpdateStatus(newMode.ToString());
                Item.Save();
            }
        }
    }
}
