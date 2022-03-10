using System.Collections;
using System.Collections.Generic;

namespace WebApp.Web.Front.Models.Common
{
    /// <summary>
    /// DataSource result
    /// </summary>
    public class DataSourceResult<T>
    {
        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        
        /// <summary>
        /// Total records
        /// </summary>
        public int Total { get; set; }
    }
}
