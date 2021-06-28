using GridWithContextMenuTestWASM.Client.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GridWithContextMenuTestWASM.Client.Shared
{
    public partial class GenericColumnsComponent<TRowInfoType> where TRowInfoType : SampleData
    {
        [Parameter]
        public EventCallback<(MouseEventArgs, TRowInfoType)> OnContextMenuClick { get; set; }
    }
}
