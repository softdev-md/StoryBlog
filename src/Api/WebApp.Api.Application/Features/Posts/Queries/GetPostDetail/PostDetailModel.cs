using System;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostDetail
{
    public class PostDetailModel
    {
        public int Id { get; set; }

        public int PostCategoryId { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }
        
        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string Tags { get; set; }
        
        public DateTime? PublishedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public int DisplayOrder { get; set; }
    }
}
