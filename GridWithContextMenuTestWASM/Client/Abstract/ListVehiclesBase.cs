using System.Collections.Generic;
using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.DTOs;
using GridWithContextMenuTestWASM.Client.DynamicComponents.SlideIn;
using GridWithContextMenuTestWASM.Client.Pages;
using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor.Components;

namespace GridWithContextMenuTestWASM.Client.Abstract
{
    public abstract class ListVehiclesBase<TRowInfoType> : ListEntityBase<TRowInfoType>
        where TRowInfoType : SampleData, new()
    {

#pragma warning disable 1998
        protected override async Task OnInitializedAsync()
#pragma warning restore 1998
        {
            UseCustomGridPaging = true;

            if (IsEnvironmentSet())
            {
                MenuItems = new List<TelerikGridContextMenuItem>
                {
                    new() {Text = "Edit Vehicle", Icon = "edit", Action = EditVehicle}
                };
            }
        }

        protected override async Task OnReadHandler(GridReadEventArgs e)
        {
            GridRequest = e.Request;

            //no grid state exists so call the database for data
            await base.OnReadHandler(e);
        }

        protected override async Task FetchData()
        {
            await RefreshGrid();
        }

        protected abstract override Task<IEnumerable<TRowInfoType>> RefreshAsyncMethod();

        protected async Task ContextMenuClick((MouseEventArgs MouseEventArgs, TRowInfoType ContextRow) args)
        {
            await ShowContextMenuOptionsForRow(args.MouseEventArgs, args.ContextRow);
        }

        #region Row Context Edit Menus

        private async Task EditVehicle()
        {
            await ShowEditVehicleSlideIn("Edit Vehicle");
        }

#pragma warning disable 1998
        private async Task ShowEditVehicleSlideIn(string slideInTitle)
#pragma warning restore 1998
        {
            if (SelectedRowInfo != null)
            {
                //from selected row context
                var parameters = new SlideInParameters();

                ShowSlideIn<EditTyreSlideIn>(slideInTitle, parameters);
            }
        }
        #endregion
    }
}
