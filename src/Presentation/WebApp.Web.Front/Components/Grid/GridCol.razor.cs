using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Components
{
    public partial class GridCol : BootstrapComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected virtual string ClassName => CssBuilder.Default(null)
            .AddClass($"col-xs-{Xs}", () => Xs != null)
            .AddClass($"col-sm-{Sm}", () => Sm != null)
            .AddClass($"col-md-{Md}", () => Md != null)
            .AddClass($"col-lg-{Lg}", () => Lg != null)
            .AddClass($"col-xl-{Xl}", () => Xl != null)
            .AddClass($"col-xxl-{Xxl}", () => Xxl != null)
            .AddClass($"col-{Column}", () => Column != null)
            .AddClass("col", () => Xs == null && Sm == null && Md == null && Lg == null && Xl == null && Xxl == null)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Parameter]
        public string Column { get; set; }
        
        [Parameter]
        public string Xs { get; set; }

        [Parameter]
        public string Sm { get; set; }

        [Parameter]
        public string Md { get; set; }

        [Parameter]
        public string Lg { get; set; }

        [Parameter]
        public string Xl { get; set; }

        [Parameter]
        public string Xxl { get; set; }

        [CascadingParameter]
        public GridRow Row { get; set; }
        
        protected override void OnInitialized()
        {
            this.Row?.Cols.Add(this);
            
            base.OnInitialized();
        }
    }
}
