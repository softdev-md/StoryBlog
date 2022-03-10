using System.Collections.Generic;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Domain.Entities
{
    public partial class PostCategory : EntityBase
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public int PictureId { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
