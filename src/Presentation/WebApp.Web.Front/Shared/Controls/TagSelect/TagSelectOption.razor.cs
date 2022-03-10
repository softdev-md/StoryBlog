using Microsoft.AspNetCore.Components;

namespace WebApp.Web.Front.Shared.Controls
{
    public partial class TagSelectOption<TValue>
    {
        [Parameter] public TValue Value { get; set; }

        [Parameter] public bool Checked { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [CascadingParameter] public TagSelect<TValue> Parent { get; set; }
        
        

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Parent.AddOption(this);
        }

        protected void HandleCheckedChange(bool isChecked)
        {
            Checked = isChecked;
            if (isChecked)
                Parent.SelectItem(Value);
            else
                Parent.UnSelectItem(Value);
            
            if (Parent.OnCheckedChange.HasDelegate)
            {
                Parent.OnCheckedChange.InvokeAsync();
            }
        }

        public void Check(bool isChecked)
        {
            Checked = isChecked;
        }
    }
}