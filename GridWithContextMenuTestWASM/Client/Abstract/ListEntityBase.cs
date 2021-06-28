using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.DTOs;
using GridWithContextMenuTestWASM.Client.DynamicComponents.Bases;
using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace GridWithContextMenuTestWASM.Client.Abstract
{
    public abstract class ListEntityBase<TRowInfoType> : SlideInLoaderBase
        where TRowInfoType : SampleData, new()
    {
        protected IEnumerable<TRowInfoType> vm { get; set; }

        protected TelerikContextMenu<TelerikGridContextMenuItem> ContextMenuRef { get; set; }

        protected TelerikGrid<TRowInfoType> GridRef { get; set; }

        protected List<TelerikGridContextMenuItem> MenuItems { get; set; }

        protected DataSourceRequest GridRequest { get; set; } //store the last request so we can repeat it when changing the data

        protected IEnumerable<TRowInfoType> CurrentGridData { get; set; }

        protected int PageSize { get; set; } = 100;
        protected int TotalCount { get; set; }
        protected bool IsRefreshingGrid { get; set; }

        //metadata for the context menu actions
        protected TRowInfoType SelectedRowInfo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (IsEnvironmentSet())
            {
                await FetchData();
                await base.OnInitializedAsync();
            }
        }

        /// <summary>
        /// Can fetch any additional data here when the component is initialized
        /// </summary>
        /// <returns></returns>
#pragma warning disable 1998
        protected virtual async Task FetchData()
#pragma warning restore 1998
        {
        }

        protected abstract Task<IEnumerable<TRowInfoType>> RefreshAsyncMethod();

        protected bool UseCustomGridPaging { get; set; } = false;

        protected async Task ShowContextMenuOptionsForRow(MouseEventArgs e, TRowInfoType row)
        {
            SelectedRowInfo = row;

            await ContextMenuRef.ShowAsync(e.ClientX, e.ClientY);
        }

        protected async Task OnContextMenuItemClick(TelerikGridContextMenuItem item)
        {
            if (item.Action != null)
            {
                await InvokeAsync(item.Action);
            }
        }

        protected override async Task OnCancelButtonClickAsync()
        {
            await base.OnCancelButtonClickAsync();

            //reset
            SelectedRowInfo = default;
        }

        protected override async Task OnSaveButtonClickAsync()
        {
            await base.OnSaveButtonClickAsync();

            //refresh grid with data
            await RefreshGrid();
        }

        protected virtual async Task OnReadHandler(GridReadEventArgs e)
        {
            GridRequest = e.Request;

            await RefreshGrid();
        }

        /// <summary>
        /// Determines how to refresh the grid
        /// </summary>
        /// <returns></returns>
        protected async Task RefreshGrid()
        {
            if (UseCustomGridPaging)
            {
                await LoadGridWithPagedData();
            }
            else
            {
                await LoadGridWithData();
            }
        }

        private async Task LoadGridWithData()
        {
            // reset and get data
            SelectedRowInfo = default;

            IsRefreshingGrid = true;

            vm = await RefreshAsyncMethod();

            //filter data based on LastRequest
            IEnumerable<TRowInfoType> filteredData;
            int totalCount;

            if (GridRequest != null)
            {
                var filteredDataSource = await Enumerable.Range(1, 1000).Select(x => new TRowInfoType
                {
                    Id = x,
                    Name = "name " + x,
                    Team = "team " + x % 5,
                    HireDate = DateTime.Now.AddDays(-x).Date
                }).ToDataSourceResultAsync(GridRequest);

                filteredData = filteredDataSource.Data as IEnumerable<TRowInfoType>;
                totalCount = filteredDataSource.Total;
            }
            else
            {
                filteredData = vm;
                totalCount = 1000;
            }

            CurrentGridData = filteredData;
            TotalCount = totalCount;

            IsRefreshingGrid = false;

            StateHasChanged();
        }

        private async Task LoadGridWithPagedData()
        {
            // reset and get data
            SelectedRowInfo = default;

            IsRefreshingGrid = true;

            vm = await RefreshAsyncMethod();

            CurrentGridData = vm;
            TotalCount = 1000;

            IsRefreshingGrid = false;

            StateHasChanged();
        }
    }
}
