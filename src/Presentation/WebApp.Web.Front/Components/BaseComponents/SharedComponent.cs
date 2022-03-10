using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using WebApp.Web.Front.Helpers;
using WebApp.Web.Front.Services;

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// Base class for SharedComponent with model
    /// </summary>
    public abstract class SharedComponent<TModel> : BaseComponent
    {
        #region Fields

        private ElementReference _ref;

        private TModel _model;

        #endregion

        #region Properties
        
        /// <summary>
        /// Returned ElementRef reference for DOM element.
        /// </summary>
        public virtual ElementReference Ref
        {
            get => _ref;
            set => _ref = value;
        }

        /// <summary>
        /// Present loading data
        /// </summary>
        protected bool Loading { get; set; }
        
        #endregion

        #region Parameter

        /// <summary>
        /// Router present model
        /// </summary>
        [Parameter]
        public TModel Model
        {
            get => _model ??= Activator.CreateInstance<TModel>();
            set => _model = value;
        }

        /// <summary>
        /// Points if data loads (requests) before rendering of a page or after.
        /// </summary>
        [Parameter] public bool IsDataLoading { get; set; }

        #endregion

        #region Services
        
        [Inject] public NavigationManager UriHelper { get; set; }
        
        [Inject] public IJSService JsService { get; set; }

        #endregion
        
        #region Lifecicle methods

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            
            if (!IsDataLoading)
            {
                Loading = true;
                await DataRequestAsync();
                Loading = false;
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        protected override async Task OnFirstAfterRenderAsync()
        {
            await base.OnFirstAfterRenderAsync();

            if (IsDataLoading)
            {
                //await DataRequestAsync();
                //Loading = false;
                //StateHasChanged();
            }
        }

        /// <summary>
        /// Loading data if a page path has been changed for the same (generic/template) page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual async Task OnLocationChangedAsync(object sender, LocationChangedEventArgs args)
        {
            Loading = true;
            StateHasChanged();

            await DataRequestAsync();
            Loading = false;
            StateHasChanged();

            //if (Router.IsPathChangedOnCurrentPage())
            //{
            //    Loading = true;
            //    StateHasChanged();

            //    await DataRequestAsync();
            //    Loading = false;
            //    StateHasChanged();
            //}
        }

        /// <summary>
        /// Method invoked after initialized or after render
        /// </summary>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        protected virtual Task DataRequestAsync()
            => Task.CompletedTask;

        /// <summary>
        /// Executes one time after loading page even if it is a template page.
        /// It ensures once loading scripts for example.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        protected virtual Task OnceOnAfterRenderAsync()
            => Task.CompletedTask;

        #endregion
        
        #region Render

        /// <summary>
        /// Render the component via the code
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="vs"></param>
        /// <returns></returns>
        public RenderFragment RenderComponent<T>(params object[] vs) => RenderComponent(typeof(T), vs);

        /// <summary>
        /// Render the component via the code
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="vs"></param>
        /// <returns></returns>
        public RenderFragment RenderComponent(Type type, params object[] vs) => builder =>
        {
            builder.OpenComponent(0, type);
            int i = 1;
            foreach (var obj in vs)
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    builder.AddAttribute(i++, prop.Name, prop.GetValue(obj, null));
                }
            }
            builder.CloseComponent();
        };

        #endregion

        #region Localizer

        private Localizer _localizer;
        
        public delegate string Localizer(string text, params object[] args);

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = format;//LocalizationService.GetResourceAsync(format).Result;
                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return format;
                        }
                        return (args == null || args.Length == 0)
                            ? resFormat
                            : string.Format(resFormat, args);
                    };
                }
                return _localizer;
            }
        }

        ///// <summary>
        ///// Get a localized resources
        ///// </summary>
        //public Localizer T
        //{
        //    get
        //    {
        //        if (LocalizationService == null)
        //            LocalizationService = EngineContext.Current.Resolve<ILocalizationService>();

        //        if (_localizer == null)
        //        {
        //            _localizer = (format, args) =>
        //            {
        //                var resFormat = LocalizationService.GetResource(format);
        //                if (string.IsNullOrEmpty(resFormat))
        //                {
        //                    return new LocalizedString(format);
        //                }
        //                return new LocalizedString((args == null || args.Length == 0)
        //                    ? resFormat
        //                    : string.Format(resFormat, args));
        //            };
        //        }
        //        return _localizer;
        //    }
        //}

        //public async Task<string> T(string format, params object[] args)
        //{
        //    var resFormat = await LocalizationService.GetResourceAsync(format);
        //    if (string.IsNullOrEmpty(resFormat))
        //    {
        //        return format;
        //    }
        //    return args == null || args.Length == 0
        //        ? resFormat
        //        : string.Format(resFormat, args);
        //}

        #endregion
        
        #region Theme

        /// <summary>
        /// Return a value indicating whether the working language and theme support RTL (right-to-left)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ShouldUseRtlThemeAsync()
        {
            var supportRtl = false;
            return supportRtl;
        }

        #endregion

        #region Style

        protected CssMapper CssMapper { get; } = new CssMapper();

        protected SharedComponent()
        {
            CssMapper.Get(() => this.Class);
        }

        private string _class;

        /// <summary>
        /// Specifies one or more class names for an DOM element.
        /// </summary>
        [Parameter]
        public string Class
        {
            get => _class;
            set
            {
                _class = value;
                CssMapper.OriginalClass = value;
            }
        }

        private string _style;

        /// <summary>
        /// Specifies an inline style for an DOM element.
        /// </summary>
        [Parameter]
        public string Style
        {
            get => _style;
            set
            {
                _style = value;
                this.StateHasChanged();
            }
        }

        protected virtual string GenerateStyle()
        {
            return Style;
        }

        #endregion
    }

    /// <summary>
    /// Base class for SharedComponent
    /// </summary>
    public abstract class SharedComponent : SharedComponent<dynamic>
    {
    }
}
