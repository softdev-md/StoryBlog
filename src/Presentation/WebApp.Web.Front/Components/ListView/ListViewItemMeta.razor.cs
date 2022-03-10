using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.Helpers;

namespace WebApp.Web.Front.Components
{
    public partial class ListViewItemMeta : BootstrapComponentBase
    {
        protected string? ClassName => CssBuilder.Default("listview-item-meta")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Parameter] public string Title { get; set; }

        [Parameter] public RenderFragment TitleTemplate { get; set; }

        [Parameter] public string Avatar { get; set; }

        [Parameter] public RenderFragment AvatarTemplate { get; set; }

        [Parameter] public string Description { get; set; }

        [Parameter] public RenderFragment DescriptionTemplate { get; set; }

        [CascadingParameter] public ListViewItem ListViewItem { get; set; }
    }
}
