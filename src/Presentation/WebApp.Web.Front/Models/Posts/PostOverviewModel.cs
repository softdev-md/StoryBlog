using System;

namespace WebApp.Web.Front.Models.Posts
{
    /// <summary>
    /// Represents post model
    /// </summary>
    public partial class PostOverviewModel
    {
        #region Properties

        public int Id { get; set; }
        
        public string PostCategoryName { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string Tags { get; set; }

        public DateTime? PublishedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        #endregion
    }
}