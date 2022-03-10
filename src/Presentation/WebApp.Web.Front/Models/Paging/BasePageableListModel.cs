using System.Collections.Generic;

namespace WebApp.Web.Front.Models.Paging
{
    public abstract class BasePageableListModel<T> : BasePageableModel
    {
        #region Ctor

        public BasePageableListModel()
        {
            //set the default values
            this.PageNumber = 1;
            this.PageSize = 10;

            this.Data = new List<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets data records
        /// </summary>
        public IList<T> Data { get; set; }
        
        #endregion

        #region Methods

        /// <summary>
        /// Set list page parameters
        /// </summary>
        public void SetListPageSize()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        
        #endregion
    }
}
