namespace WebApp.Api.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostDto
    {
        public int Id { get; set; }

        public int PostCategoryId { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }
    }
}
