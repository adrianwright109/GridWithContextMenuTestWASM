using Microsoft.AspNetCore.Components;

namespace GridWithContextMenuTestWASM.Client
{
    public abstract class MBComponentBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Do we have environment and SessionId set? Redirect if NOT.
        /// HACK: Route guards are not a thing yet in Blazor.
        /// </summary>
        /// <returns></returns>
        protected bool IsEnvironmentSet()
        {
            return true;
        }
    }
}
