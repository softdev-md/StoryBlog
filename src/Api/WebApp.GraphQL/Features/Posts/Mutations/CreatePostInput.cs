namespace WebApp.GraphQL.Features.Posts.Mutations
{
    public class CreatePostInput
    {
        public int ProjectId { get; set; }

        public int PostCategoryId { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }
    }
}
