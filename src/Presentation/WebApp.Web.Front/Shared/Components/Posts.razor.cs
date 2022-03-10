using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.GrpcServices;
using WebApp.Web.Front.Models.Common;
using WebApp.Web.Front.Models.Posts;
using WebApp.Web.Front.Services;

namespace WebApp.Web.Front.Shared.Components
{
    public partial class Posts : SharedComponent<PostListModel>
    {
        #region Fields

        private ListView<PostOverviewModel> _listViewPosts;

        private IList<int> _selectedCategoryIds = new List<int>();
        
        #endregion

        #region Services

        [Inject] private IPostService PostService { get; set; }

        [Inject] private IPostCategoryService PostCategoryService { get; set; }

        [Inject] private PostGrpcService PostGrpcService { get; set; }
        
        #endregion

        #region Utilities

        /// <summary>
        /// Prepare post model
        /// </summary>
        /// <param name="model">Post model</param>
        /// <param name="post">Post</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Post model</returns>
        public virtual async Task<PostModel> PreparePostModelAsync(PostModel model, Post post, 
            bool excludeProperties = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (post != null)
            {
                model.Id = post.Id;
                model.Author = post.Author;
                model.Body = post.Body;
                model.PostCategoryId = post.PostCategoryId;
                model.ProjectId = post.ProjectId;
                model.Tags = post.Tags;
                model.PublishedOn = post.PublishedOn;
                model.CreatedOn = post.CreatedOn;
            }

            //set default values for the new model
            if (post == null)
            {
                //dates
                model.PublishedOn = DateTime.Now;
                model.CreatedOn = DateTime.Now;
            }
            
            return model;
        }

        /// <summary>
        /// Prepare the post models
        /// </summary>
        /// <param name="posts">Collection of posts</param>
        /// <returns>Collection of post model</returns>
        public virtual async Task<IEnumerable<PostModel>> PreparePostModelsAsync(IEnumerable<Post> posts)
        {
            if (posts == null)
                throw new ArgumentNullException(nameof(posts));

            var models = new List<PostModel>();
            foreach (var post in posts)
            {
                var model = new PostModel();
                model.Id = post.Id;
                model.Author = post.Author;
                model.Body = post.Body;
                model.PostCategoryId = post.PostCategoryId;
                model.ProjectId = post.ProjectId;
                model.Tags = post.Tags;
                model.PublishedOn = post.PublishedOn;
                model.CreatedOn = post.CreatedOn;
                
                models.Add(model);
            }
            return models;
        }

        /// <summary>
        /// Prepare the overview post models
        /// </summary>
        /// <param name="posts">Collection of posts</param>
        /// <returns>Collection of post model</returns>
        public virtual async Task<IEnumerable<PostOverviewModel>> PreparePostOverviewModelsAsync(IEnumerable<Post> posts)
        {
            if (posts == null)
                throw new ArgumentNullException(nameof(posts));

            var models = new List<PostOverviewModel>();
            foreach (var post in posts)
            {
                var model = new PostOverviewModel();
                model.Id = post.Id;
                model.Author = post.Author;
                model.Body = post.Body;
                model.CommentsCount = post.CommentsCount;
                model.LikesCount = post.LikesCount;
                model.Tags = post.Tags;
                model.PublishedOn = post.PublishedOn;
                model.CreatedOn = post.CreatedOn;

                models.Add(model);
            }
            return models;
        }
        
        #endregion

        #region Methods

        protected override async Task OnInitializedAsync()
        {
            Model.PageSize = 10;
            
            //Command = UriHelper.SetModelFromQuery<PostPagingFilteringModel>();
            
            await base.OnInitializedAsync();
        }

        protected override async Task DataRequestAsync()
        {
            //prepare available model filter categories
            var allFilters = await PostCategoryService.GetAllPostCategoriesAsync();

            //adverts categories
            Model.Categories = allFilters.Select(x =>
                new ListItemModel
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList();
        }

        private async Task<QueryData<PostOverviewModel>> OnPostListChange(QueryPageOptions queryModel)
        {
            Loading = true;

            //var user = await UserService.GetUserByIdAsync(1);

            //get posts
            //var result = await PostService.GetAllPostsAsync(1, _selectedCategoryIds.LastOrDefault(),
                //pageIndex: queryModel.PageIndex - 1, pageSize: queryModel.PageItems);

            var result = await PostGrpcService.GetAllPostsAsync(1, _selectedCategoryIds.LastOrDefault(), keyword: "",
                pageIndex: queryModel.PageIndex - 1, pageSize: queryModel.PageItems);

            var data = result.Data.Select(post => new Post()
            {
                Id = post.Id,
                Author = post.Author,
                Body = post.Body,
                PostCategoryId = post.PostCategoryId,
                CreatedOn = post.CreatedOn.ToDateTime(),
                PublishedOn = post.PublishedOn?.ToDateTime(),
                LikesCount = post.LikesCount
            }).ToList();

            //prepare posts
            //Model.Data = (await PreparePostOverviewModelsAsync((IEnumerable<Post>)result.Data)).ToList();
            Model.Data = (await PreparePostOverviewModelsAsync((IEnumerable<Post>)data)).ToList();

            Model.LoadPagedList(queryModel.PageIndex - 1,queryModel.PageItems, result.Total);
            
            Loading = false;

            return await Task.FromResult(new QueryData<PostOverviewModel>()
            {
                Items = Model.Data,
                TotalCount = Model.TotalItems
            });
        }

        private async Task Edit(int postId)
        {
            //UriHelper.NavigateTo(Url.RouteUrl("PostEdit", new {postId = postId}));
        }

        private async Task Edit(PostOverviewModel post)
        {
            //UriHelper.NavigateTo(Url.RouteUrl("PostEdit", new {postId = post.Id}));
        }
        
        #endregion
    }
}
