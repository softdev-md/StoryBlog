using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Shared.Layouts
{
    public partial class BasicLayout : LayoutComponent
    {
        private readonly bool _isChildrenLayout = false;
        private string _genLayoutStyle;
        private string _weakModeStyle;
        
        public string BaseClassName => $"b-basicLayout";
        public CssMapper ContentClassMapper { get; set; } = new CssMapper();
        
        [Parameter] public bool IsMobile { get; set; }
        [Parameter] public bool DisableContentMargin { get; set; }
        [Parameter] public string ContentStyle { get; set; }
        [Parameter] public string ColSize { get; set; } = "lg";
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetStyle();
            SetClassMap();
        }

        protected void SetStyle()
        {
            _genLayoutStyle = "min-height: 100%;";
        }

        protected void SetClassMap()
        {
            CssMapper
                .Clear()
                .Add(BaseClassName)
                .Add($"screen-{ColSize}")
                .If($"{BaseClassName}-is-children", () => _isChildrenLayout)
                .If($"{BaseClassName}-mobile", () => IsMobile);

            ContentClassMapper
                .Clear()
                .Add($"{BaseClassName}-content")
                .If($"{BaseClassName}-has-header", () => HeaderRender)
                .If($"{BaseClassName}-content-disable-margin", () => DisableContentMargin);
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();
            SetStyle();
        }
    }
}
