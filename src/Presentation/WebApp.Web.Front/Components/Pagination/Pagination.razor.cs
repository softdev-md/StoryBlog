using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Pagination
    {
        [Inject]
        private IStringLocalizer<Pagination> Localizer { get; set; }
        
        private string PageLabel { get; set; }
        
        private string PrevPageText { get; set; }
        
        private string FirstPageText { get; set; }
        
        private string NextPageText { get; set; }
        
        private string PrePageInfoText { get; set; }
        
        private string RowInfoText { get; set; }
        
        private string PageInfoText { get; set; }
        
        private string TotalInfoText { get; set; }
        
        private string SelectItemsText { get; set; }
        
        private string LabelString { get; set; }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PageLabel ??= Localizer[nameof(PageLabel)];
            PrevPageText ??= Localizer[nameof(PrevPageText)];
            FirstPageText ??= Localizer[nameof(FirstPageText)];
            NextPageText ??= Localizer[nameof(NextPageText)];
            PrePageInfoText ??= Localizer[nameof(PrePageInfoText)];
            RowInfoText ??= Localizer[nameof(RowInfoText)];
            PageInfoText ??= Localizer[nameof(PageInfoText)];
            TotalInfoText ??= Localizer[nameof(TotalInfoText)];
            SelectItemsText ??= Localizer[nameof(SelectItemsText)];
            LabelString ??= Localizer[nameof(LabelString)];
        }
        
        //private IEnumerable<SelectedItem> GetPageItems()
        //{
        //    var pages = PageItemsSource ?? new List<int>() { 20, 40, 80, 100, 200 };
        //    var ret = new List<SelectedItem>();
        //    for (var i = 0; i < pages.Count(); i++)
        //    {
        //        var item = new SelectedItem(pages.ElementAt(i).ToString(), string.Format(SelectItemsText, pages.ElementAt(i)));
        //        ret.Add(item);
        //        if (pages.ElementAt(i) >= TotalCount) break;
        //    }
        //    return ret;
        //}

        private string GetPageInfoText() => string.Format(PageInfoText, StarIndex, EndIndex);

        private string GetTotalInfoText() => string.Format(TotalInfoText, TotalCount);

        private string GetLabelString => string.Format(LabelString, PageIndex);
    }
}
