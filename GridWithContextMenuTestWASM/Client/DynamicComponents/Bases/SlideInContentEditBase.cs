using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GridWithContextMenuTestWASM.Client.DynamicComponents.Bases
{
    public abstract class SlideInContentEditBase<TEditType> : MBComponentBase where TEditType : SampleData, new()
    {
        [CascadingParameter]
        protected Shared.SlideIn SlideInInstance { get; set; }

        protected TEditType vm { get; set; }
                                                                                                                                                    
        protected override async Task OnInitializedAsync()
        {
            if (IsEnvironmentSet())
            {
                vm = new TEditType();
                
                await base.OnInitializedAsync();
            }
        }

        protected async Task Cancel(MouseEventArgs obj)
        {
            await SlideInInstance.CancelAsync();
        }

        protected abstract string GetSaveMessage(TEditType model);
    }
}
