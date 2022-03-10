using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Tests.WebApp.Api.Persistence
{
    public static class MockPostRepository
    {
        public static Mock<IPostRepository> GetPostRepository()
        {
            var  posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Author = "John",
                    PostCategoryId = 1,
                    Body = "Test content 1",
                    CreatedOn = DateTime.Now
                },
                new Post()
                {
                    Id = 2,
                    Author = "Smith",
                    PostCategoryId = 2,
                    Body = "Test content 2",
                    CreatedOn = DateTime.Now
                }
            };

            var pagedPosts = new PagedList<Post>(posts, 0, int.MaxValue);
            //pagedPosts.AddRange(posts);

            var mockRepository = new Mock<IPostRepository>();

            //get paged list
            mockRepository.Setup(r => 
                    r.GetAllPostsAsync(It.IsAny<int>(), It.IsAny<IList<int>>(), 
                        It.IsAny<int>(), 
                        It.IsAny<string>(), It.IsAny<string>(), 
                        It.IsAny<DateTime?>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(pagedPosts);

            //insert
            mockRepository.Setup(r => r.InsertAsync(It.IsAny<Post>())).ReturnsAsync((Post post) =>
            {
                pagedPosts.Add(post);

                return post;
            });

            return mockRepository;
        }
    }
}
