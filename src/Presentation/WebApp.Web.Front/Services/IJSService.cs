using System.Threading.Tasks;

namespace WebApp.Web.Front.Services
{
    public interface IJSService
    {
        /// <summary>
        /// Get the alert window with the pointed message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        Task Alert(string message);

        /// <summary>
        /// Open a modal window witch content from the query
        /// </summary>
        /// <param name="query">url/query for window content</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scroll">enable/disable scrolling</param>
        Task OpenWindow(string query, int width, int height, bool scroll);

        /// <summary>
        /// Display/hide a load waiting stub
        /// </summary>
        /// <param name="display">true - display; false - hide;</param>
        Task DisplayAjaxLoading(bool display);

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName">name of a cookie</param>
        /// <returns></returns>
        Task<string> GetCookie(string cookieName); 

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="experienceDays"></param>
        /// <returns></returns>
        Task SetCookie(string cookieName, string cookieValue, int experienceDays);

        /// <summary>
        /// Get a cookie by a name
        /// </summary>
        /// <param name="cookieName">name of the erasing cookie</param>
        /// <returns></returns>
        Task EraseCookie(string cookieName);
    }
}