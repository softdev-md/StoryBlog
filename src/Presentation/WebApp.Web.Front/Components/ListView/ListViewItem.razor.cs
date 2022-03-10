using System;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Components
{
    public partial class ListViewItem : BootstrapComponentBase
    {
        protected virtual string? ClassName => CssBuilder.Default("listview-item")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Parameter] public string Content { get; set; }

        [Parameter] public RenderFragment Extra { get; set; }

        [Parameter] public RenderFragment[] Features { get; set; }

        [Parameter] public RenderFragment[] Actions { get; set; }
        
        [Parameter] public RenderFragment ChildContent { get; set; }

        [CascadingParameter(Name = "Grid")]
        public ListGridType Grid { get; set; }

        [Parameter] public string ColStyle { get; set; }

        [Parameter] public int ItemCount { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

        [Parameter] public bool NoFlex { get; set; }
        
        [CascadingParameter(Name = "ItemClick")] 
        public Action ItemClick { get; set; }
        
        private void HandleClick()
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(this);
            }

            ItemClick?.Invoke();
        }
    }
}
