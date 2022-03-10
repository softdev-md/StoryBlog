using System;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Domain.Entities
{
    public partial class Post : EntityBase
    {
        public int ProjectId { get; set; }
        public int PostCategoryId { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public int PictureId { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string Tags { get; set; }
        public int PostStatusId { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public int DisplayOrder { get; set; }

        public virtual PostCategory PostCategory { get; set; }
        public virtual Project Project { get; set; }
    }
}
