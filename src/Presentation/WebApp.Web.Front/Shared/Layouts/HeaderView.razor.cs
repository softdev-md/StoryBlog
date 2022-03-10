using System.Text;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;

namespace WebApp.Web.Front.Shared.Layouts
{
    public partial class HeaderView : LayoutComponent
    {
        private string _headerStyle;
        private string _fixedHeaderStyle;
        private bool _needFixedHeader;
        public string PrefixCls { get; set; } = "b-layout";
        [Parameter] public bool IsMobile { get; set; }
        [Parameter] public string Logo { get; set; }
        [Parameter] public RenderFragment HeaderContentRender { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetStyle();
            SetClassMap();
        }

        protected void SetStyle()
        {
            _needFixedHeader = FixedHeader;
            var width = "100%";
            var zIndex = 9;
            var sb = new StringBuilder();
            sb.Append("padding: 0;");
            sb.Append($"height: {HeaderHeight}px;");
            sb.Append($"width: {width};");
            sb.Append($"z-index: {zIndex};");
            if (_needFixedHeader)
            {
                sb.Append("position: fixed;");
                sb.Append("top: 0;");
                sb.Append("right: 0;");
            }
            sb.Append(Style);
            _headerStyle = sb.ToString();
            _fixedHeaderStyle = $"height:{HeaderHeight}px; background: transparent;";
        }

        protected void SetClassMap()
        {
            CssMapper
                .Clear()
                .If($"{PrefixCls}-fixed-header", () => _needFixedHeader)
                .If("wide", () => ContentWidth == "Fixed");
        }

        protected override void OnStateChanged()
        {
            SetStyle();
            SetClassMap();
            base.OnStateChanged();
        }
    }
}