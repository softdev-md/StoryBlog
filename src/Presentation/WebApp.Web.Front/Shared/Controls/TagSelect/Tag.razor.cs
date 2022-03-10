﻿using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebApp.Web.Front.Components;

namespace WebApp.Web.Front.Shared.Controls
{
    public partial class Tag : SharedComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        ///  'default' | 'closeable' | 'checkable'
        /// </summary>
        [Parameter]
        public string Mode { get; set; } = "default";

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Closable { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public bool Checked { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool NoAnimation { get; set; }

        [Parameter]
        public EventCallback AfterClose { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClose { get; set; }

        [Parameter]
        public EventCallback<bool> CheckedChange { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        private bool _presetColor;
        private bool _closed;

        protected override Task OnInitializedAsync()
        {
            this.UpdateClassMap();
            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            this.UpdateClassMap();
            base.OnParametersSet();
        }

        private static bool IsPresetColor(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return false;
            }

            bool result = Regex.IsMatch(color, "^(pink|red|yellow|orange|cyan|green|blue|purple|geekblue|magenta|volcano|gold|lime)(-inverse)?$");
            if (!result) result = Regex.IsMatch(color, "^#([0-9a-fA-F]{6}|[0-9a-fA-F]{3})$");
            return result;
        }

        private void UpdateClassMap()
        {
            this.CssMapper.Clear()
                .If($"has-color", () => !string.IsNullOrEmpty(Color) && !_presetColor)
                .If($"hidden", () => Visible == false)
                .If($"checkable", () => Mode == "checkable")
                .If($"checkable-checked", () => Checked);
        }

        private async Task UpdateCheckedStatus()
        {
            if (Mode == "checkable")
            {
                this.Checked = !this.Checked;
                await this.CheckedChange.InvokeAsync(this.Checked);
                this.UpdateClassMap();
            }
        }

        private async Task CloseTag(MouseEventArgs e)
        {
            await this.OnClose.InvokeAsync(e);
            this._closed = true;
        }

        private async Task ClickTag(MouseEventArgs e)
        {
            await this.UpdateCheckedStatus();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(this);
            }
        }
    }
}
