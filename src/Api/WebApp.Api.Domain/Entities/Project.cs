using System.Collections.Generic;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Domain.Entities
{
    public partial class Project : EntityBase
    {
        public Project()
        {
            PostCategories = new HashSet<PostCategory>();
            Posts = new HashSet<Post>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
