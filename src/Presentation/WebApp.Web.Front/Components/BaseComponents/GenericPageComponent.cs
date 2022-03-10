using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace WebApp.Web.Front.Components
{
    public abstract class GenericPageComponent : PageComponent
    {
        [Inject] private NavigationManager NavigationManager { get; set; }

        private EventHandler<LocationChangedEventArgs> _locationChanged;

        protected override async Task OnInitializedAsync()
        {
            _locationChanged = async (s, e) => await base.OnLocationChangedAsync(s, e);
            NavigationManager.LocationChanged += _locationChanged;
            await base.OnInitializedAsync();
        }

        protected override void Dispose(bool disposing)
        {
            NavigationManager.LocationChanged -= _locationChanged;
            base.Dispose(disposing);
        }
    }
}
