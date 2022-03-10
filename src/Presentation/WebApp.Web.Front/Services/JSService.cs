using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WebApp.Web.Front.Services
{

    /// <summary>
    /// Implementation of the calls to public.common.js and public.ajaxcart.js for manipulating the DOM elements
    /// </summary>
    public class JSService : IJSService
    {
        private readonly IJSRuntime _js;

        public JSService(IJSRuntime js)
        {
            this._js = js;
        }

        /// <summary>
        /// Get the alert windiw with the pointed message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        public async Task Alert(string message)
        {
            try
            {
                await _js.InvokeAsync<object>("alert", message);
            }
            catch { }
        }

        /// <summary>
        /// Open a modal window wicth content from the query
        /// </summary>
        /// <param name="query">url/query for window content</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scroll">enable/disable scrolling</param>
        /// <returns></returns>
        public async Task OpenWindow(string query, int width, int height, bool scroll)
        {
            try
            {
                await _js.InvokeAsync<object>("OpenWindow", query, width, height, scroll);
            }
            catch { }
        }

        /// <summary>
        /// Display/hide a load waiting stub
        /// </summary>
        /// <param name="display">true - display; false - hide;</param>
        public async Task DisplayAjaxLoading(bool display)
        {
            try
            {
                await _js.InvokeAsync<object>("displayAjaxLoading", display);
            }
            catch { }
        }

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName">name of a cookie</param>
        /// <returns></returns>
        public async Task<string> GetCookie(string cookieName)
        {
            try
            {
                return await _js.InvokeAsync<string>("CookiesService.Get", cookieName);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="experienceDays"></param>
        /// <returns></returns>
        public async Task SetCookie(string cookieName, string cookieValue, int experienceDays)
        {
            try
            {
                await _js.InvokeAsync<string>("CookiesService.Set", cookieName, cookieValue, experienceDays);
            }
            catch { }
        }

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName">name of the erasing cookie</param>
        /// <returns></returns>
        public async Task EraseCookie(string cookieName)
        {
            try
            {
                await _js.InvokeAsync<string>("CookiesService.Erase", cookieName);
            }
            catch { }
        }
    }
}
