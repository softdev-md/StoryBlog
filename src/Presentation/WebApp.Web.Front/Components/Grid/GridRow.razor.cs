using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Components
{
    public partial class GridRow : BootstrapComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected virtual string ClassName => CssBuilder.Default("row")
            .AddClass($"row-top", () => Align == "top")
            .AddClass($"row-middle", () => Align == "middle")
            .AddClass($"row-bottom", () => Align == "bottom")
            .AddClass($"row-start", () => Justify == "start")
            .AddClass($"row-end", () => Justify == "end")
            .AddClass($"row-center", () => Justify == "center")
            .AddClass($"row-space-around", () => Justify == "space-around")
            .AddClass($"row-space-between", () => Justify == "space-between")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();
        
        /// <summary>
        /// 'top' | 'middle' | 'bottom'
        /// </summary>
        [Parameter]
        public string Align { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public string Justify { get; set; }
        
        public IList<GridCol> Cols { get; } = new List<GridCol>();
    }
}
