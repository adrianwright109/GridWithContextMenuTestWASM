using System;
using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.DynamicComponents.SlideIn;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;

namespace GridWithContextMenuTestWASM.Client.Shared
{
    public partial class SlideIn : IDisposable
    {
        private TelerikAnimationContainer _slideInRef;

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public SlideInOptions Options { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Used to inform the List component hosting the SlideIn
        /// that the cancel button on the SlideIn has been clicked. 
        /// </summary>
        [Parameter]
        public EventCallback CancelButtonClicked { get; set; }

        /// <summary>
        /// Used to inform the List component hosting the SlideIn
        /// that the save button on the SlideIn has been clicked. 
        /// </summary>
        [Parameter]
        public EventCallback SaveButtonClicked { get; set; }

        public async Task CancelAsync()
        {
            //hide slide-in
            await _slideInRef.HideAsync();

            //inform list that the slide-in has been cancelled
            //so it can be removed from the DOM
            await CancelButtonClicked.InvokeAsync();
        }

        public async Task SaveAsync()
        {
            //hide slide-in
            await _slideInRef.HideAsync();

            //inform list that the slide-in has been cancelled
            //so it can be removed from the DOM
            await SaveButtonClicked.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _slideInRef.ShowAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        public void Dispose()
        {
        }
    }
}
