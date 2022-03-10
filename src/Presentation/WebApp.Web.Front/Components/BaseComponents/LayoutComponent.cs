using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace WebApp.Web.Front.Components
{
    public abstract class LayoutComponent : SharedComponent
    {
        [Parameter]
        public int HeaderHeight
        {
            get => SettingState.Value.HeaderHeight;
            set => SettingState.Value.HeaderHeight = value;
        }
        
        [Parameter] 
        public string ContentWidth
        {
            get => SettingState.Value.ContentWidth;
            set => SettingState.Value.ContentWidth = value;
        }

        [Parameter]
        public bool FixedHeader
        {
            get => SettingState.Value.FixedHeader; 
            set => SettingState.Value.FixedHeader = value;
        }
        
        [Parameter] 
        public string Title
        {
            get => SettingState.Value.Title;
            set => SettingState.Value.Title = value;
        }
        
        [Parameter]
        public bool HeaderRender
        {
            get => SettingState.Value.HeaderRender;
            set => SettingState.Value.HeaderRender = value;
        }

        [Parameter]
        public bool FooterRender
        {
            get => SettingState.Value.FooterRender;
            set => SettingState.Value.FooterRender = value;
        }

        [Parameter]
        public RenderFragment FooterContent { get; set; }
        
        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Inject]
        protected IOptions<ProSettings> SettingState { get; set; }

        protected virtual void OnStateChanged()
        {
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SettingState.Value.OnStateChange += OnStateChanged;
        }

        protected override void Dispose(bool disposing)
        {
            SettingState.Value.OnStateChange -= OnStateChanged;
            base.Dispose(disposing);
        }
    }

    public class ProSettings
    {
        private string _layout = "mix";         // side | top | mix
        private string _contentWidth = "Fluid"; // Fluid | Fixed
        private bool _headerRender = true;
        private bool _footerRender = true;
        private bool _fixedHeader = false;
        private int _headerHeight = 60;
        private string _title = "Comedy Story";
        public event Action OnStateChange; // todo: replace with service for updating state.
        
        public int HeaderHeight
        {
            get => _headerHeight;
            set
            {
                if (_headerHeight == value) return;
                _headerHeight = value;
                OnStateChange?.Invoke();
            }
        }

        public string Layout
        {
            get => _layout;
            set
            {
                if (_layout == value) return;
                _layout = value;
                OnStateChange?.Invoke();
            }
        } 

        public string ContentWidth
        {
            get => _contentWidth;
            set
            {
                if (_contentWidth == value) return;
                _contentWidth = value;
                OnStateChange?.Invoke();
            }
        }

        public bool FixedHeader
        {
            get => _fixedHeader;
            set
            {
                if (_fixedHeader == value) return;
                _fixedHeader = value;
                OnStateChange?.Invoke();
            }
        }
        
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnStateChange?.Invoke();
            }
        }
        
        public bool HeaderRender
        {
            get => _headerRender;
            set
            {
                if (_headerRender == value) return;
                _headerRender = value;
                OnStateChange?.Invoke();
            }
        }

        public bool FooterRender
        {
            get => _footerRender;
            set
            {
                if (_footerRender == value) return;
                _footerRender = value;
                OnStateChange?.Invoke();
            }
        }
    }
}