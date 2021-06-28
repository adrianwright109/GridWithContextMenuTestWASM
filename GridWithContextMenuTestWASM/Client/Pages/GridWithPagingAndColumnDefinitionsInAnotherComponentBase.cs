using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.Abstract;
using GridWithContextMenuTestWASM.Client.DTOs;
using GridWithContextMenuTestWASM.Client.DynamicComponents.SlideIn;

namespace GridWithContextMenuTestWASM.Client.Pages
{
    public class GridWithPagingAndColumnDefinitionsInAnotherComponentBase : ListVehiclesBase<SampleDataType3>
    {
        protected override async Task OnInitializedAsync()
        {
            if (IsEnvironmentSet())
            {
                MenuItems = new List<TelerikGridContextMenuItem>
                {
                    new() {Text = "Edit Tyre", Icon = "edit", Action = EditSelectedTyre}
                };

                await base.OnInitializedAsync();
            }
        }

        protected override async Task<IEnumerable<SampleDataType3>> RefreshAsyncMethod()
        {
            var dataItems = new List<SampleDataType3>();
            await Task.Delay(1000);//Simulate waiting for data to come through. 
            dataItems.AddRange(Enumerable.Range(1, 1000).Select(x => new SampleDataType3
            {
                Id = x,
                Name = "name " + x,
                Team = "team " + x % 5,
                HireDate = DateTime.Now.AddDays(-x).Date
            }));

            return dataItems;
        }

        protected async Task AddNewTyre()
        {
            await ShowEditTyreSlideIn("Add Tyre");
        }

        private async Task EditSelectedTyre()
        {
            await ShowEditTyreSlideIn("Edit Tyre");
        }

#pragma warning disable 1998
        private async Task ShowEditTyreSlideIn(string slideInTitle)
#pragma warning restore 1998
        {
            if (SelectedRowInfo != null)
            {
                //from selected row context
                var parameters = new SlideInParameters();
                ShowSlideIn<EditTyreSlideIn>(slideInTitle, parameters);
            }
            else
            {
                //add new Tyre
                var parameters = new SlideInParameters();
                ShowSlideIn<EditTyreSlideIn>(slideInTitle, parameters);
            }
        }
    }
}
