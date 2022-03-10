using System.Collections.Generic;
using WebApp.Web.Front.Models.Common;
using WebApp.Web.Front.Models.Paging;

namespace WebApp.Web.Front.Models.Posts
{
    public class PostListModel : BasePageableListModel<PostOverviewModel>
    {
        public PostListModel()
        {
            Categories = new List<ListItemModel>();
        }

        public int PostTotalCount { get; set; }

        public IList<ListItemModel> Categories { get; set; }
    }
}
