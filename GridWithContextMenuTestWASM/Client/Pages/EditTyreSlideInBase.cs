using GridWithContextMenuTestWASM.Client.DTOs;
using GridWithContextMenuTestWASM.Client.DynamicComponents.Bases;

namespace GridWithContextMenuTestWASM.Client.Pages
{
    public class EditTyreSlideInBase : SlideInContentEditBase<SampleDataType1>
    {
        protected override string GetSaveMessage(SampleDataType1 model)
        {
            return $"Tyre: '{model.Id}' successfully saved.";
        }
    }
}
