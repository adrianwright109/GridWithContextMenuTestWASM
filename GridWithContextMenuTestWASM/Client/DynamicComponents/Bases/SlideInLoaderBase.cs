using System;
using System.Threading.Tasks;
using GridWithContextMenuTestWASM.Client.DynamicComponents.SlideIn;
using Microsoft.AspNetCore.Components;

namespace GridWithContextMenuTestWASM.Client.DynamicComponents.Bases
{
    public abstract class SlideInLoaderBase : MBComponentBase
    {
        /// <summary>
        /// Holds the markup for the rendered Slide-In component.
        /// </summary>
        protected RenderFragment SlideInRenderFragment { get; set; }

        /// <summary>
        /// Shows the slide-in with the component type.
        /// </summary>
        protected void ShowSlideIn<T>() where T : IComponent
        {
            ShowSlideIn<T>(string.Empty, new SlideInParameters(), new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified title.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        protected void ShowSlideIn<T>(string title) where T : IComponent
        {
            ShowSlideIn<T>(title, new SlideInParameters(), new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified title.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        /// <param name="options">Options to configure the slide-in.</param>
        protected void ShowSlideIn<T>(string title, SlideInOptions options) where T : IComponent
        {
            ShowSlideIn<T>(title, new SlideInParameters(), options);
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        protected void ShowSlideIn<T>(string title, SlideInParameters parameters) where T : IComponent
        {
            ShowSlideIn<T>(title, parameters, new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the slide-in.</param>
        protected void ShowSlideIn<T>(string title, SlideInParameters parameters, SlideInOptions options) where T : IComponent
        {
            ShowSlideIn(typeof(T), title, parameters, options);
        }

        /// <summary>
        /// Shows the slide-in with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        protected void ShowSlideIn(Type contentComponent)
        {
            ShowSlideIn(contentComponent, string.Empty, new SlideInParameters(), new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified title.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Slide-In title.</param>
        protected void ShowSlideIn(Type contentComponent, string title)
        {
            ShowSlideIn(contentComponent, title, new SlideInParameters(), new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified title.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="options">Options to configure the slide-in.</param>
        protected void ShowSlideIn(Type contentComponent, string title, SlideInOptions options)
        {
            ShowSlideIn(contentComponent, title, new SlideInParameters(), options);
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Slide-In title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        protected void ShowSlideIn(Type contentComponent, string title, SlideInParameters parameters)
        {
            ShowSlideIn(contentComponent, title, parameters, new SlideInOptions());
        }

        /// <summary>
        /// Shows the slide-in with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Slide-In title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the slide-in.</param>
        protected void ShowSlideIn(Type contentComponent, string title, SlideInParameters parameters, SlideInOptions options)
        {
            if (!typeof(IComponent).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
            }

            var slideInContent = new RenderFragment(builder =>
            {
                var i = 0;
                builder.OpenComponent(i++, contentComponent);
                foreach (var parameter in parameters._parameters)
                {
                    builder.AddAttribute(i++, parameter.Key, parameter.Value);
                }
                builder.CloseComponent();
            });

            var slideInCancelCallback = EventCallback.Factory.Create(this, OnCancelButtonClickAsync);
            var slideInSaveCallback = EventCallback.Factory.Create(this, OnSaveButtonClickAsync);

            var slideInInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<Shared.SlideIn>(0);
                builder.AddAttribute(1, "Title", title);
                builder.AddAttribute(2, "Options", options);
                builder.AddAttribute(3, "ChildContent", slideInContent);
                builder.AddAttribute(4, "CancelButtonClicked", slideInCancelCallback);
                builder.AddAttribute(5, "SaveButtonClicked", slideInSaveCallback);
                builder.CloseComponent();
            });

            SlideInRenderFragment = slideInInstance;
        }

#pragma warning disable 1998
        protected virtual async Task OnCancelButtonClickAsync()
#pragma warning restore 1998
        {
            //remove slide-in from the List component DOM by setting
            //the RenderFragment null
            SlideInRenderFragment = null;
        }

#pragma warning disable 1998
        protected virtual async Task OnSaveButtonClickAsync()
#pragma warning restore 1998
        {
            //remove slide-in from the List component DOM by setting
            //the RenderFragment null
            SlideInRenderFragment = null;
        }
    }
}
                                                                                