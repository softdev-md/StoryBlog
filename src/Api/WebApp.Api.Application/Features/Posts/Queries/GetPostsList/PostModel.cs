using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostsList
{
    public class DataSourceResult<T>
    {
        public IList<T> Data { get; set; }
        
        public int Total { get; set; }
    }

    public class PostModel
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
