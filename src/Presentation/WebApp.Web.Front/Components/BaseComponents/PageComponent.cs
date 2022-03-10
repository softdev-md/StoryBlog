using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace WebApp.Web.Front.Components
{
    /// <summary>
    /// Base class for PageComponent with model
    /// </summary>
    public abstract class PageComponent<TModel> : SharedComponent<TModel> 
    {
        #region Consts

        internal const string BodyPropertyName = nameof(Body);

        #endregion

        #region Parameter

        /// <summary>
        /// Gets the content to be rendered inside the layout.
        /// </summary>
        [Parameter]
        public RenderFragment? Body { get; set; }

        #endregion
        
        #region Render

        /// <summary>
        /// RenderSection implementation
        /// </summary>
        /// <param name="name"></param>
        public RenderFragment RenderSection(string name)
        {
            if (Body != null && name != null)
            {
                var pageType = (Body.Target as RouteView)?.RouteData?.PageType;
                if (pageType != null)
                {
                    var renderFragment = pageType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => x.PropertyType == typeof(RenderFragment))
                        .FirstOrDefault(x => x.Name.Equals(name + "section", StringComparison.InvariantCultureIgnoreCase));

                    if (renderFragment != null)
                    {
                        var renderingSection = renderFragment.GetValue(Activator.CreateInstance(pageType));
                        return renderingSection as RenderFragment;
                    }
                }
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    /// Base class for PageComponent
    /// </summary>
    public abstract class PageComponent : PageComponent<dynamic>
    {
    }
}
