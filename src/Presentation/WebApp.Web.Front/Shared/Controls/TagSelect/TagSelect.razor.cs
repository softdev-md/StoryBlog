using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.Components;

namespace WebApp.Web.Front.Shared.Controls
{
    public partial class TagSelect<TValue> : SharedComponent
    {
        private readonly IList<TagSelectOption<TValue>> _options = new List<TagSelectOption<TValue>>();
        private bool _checkedAll;
        private bool _expand = false;

        [Parameter] public bool Expandable { get; set; }

        [Parameter] public bool HideCheckAll { get; set; }

        [Parameter] public string SelectAllText { get; set; } = "Select All";

        [Parameter] public string CollapseText { get; set; } = "Collapse";

        [Parameter] public string ExpandText { get; set; } = "Expand";

        [Parameter] public IList<TValue> Value { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }
        
        [Parameter] public EventCallback<TValue> OnCheckedChange { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetClassMap();
        }

        protected void SetClassMap()
        {
            CssMapper
                .Clear()
                .Add("tagSelect")
                .If("hasExpandTag", () => Expandable)
                .If("expanded", () => _expand);
        }

        private void HandleExpand()
        {
            _expand = !_expand;
        }

        private void HandleCheckedChange(bool isChecked)
        {
            _checkedAll = isChecked;
            foreach (var option in _options) option.Check(_checkedAll);
        }

        public void AddOption(TagSelectOption<TValue> option)
        {
            _options.Add(option);
        }

        public void SelectItem(TValue value)
        {
            Value?.Add(value);
        }

        public void UnSelectItem(TValue value)
        {
            Value?.Remove(value);
        }
    }
}