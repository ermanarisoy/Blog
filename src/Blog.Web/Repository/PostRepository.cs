using Blog.Web.Extensions;
using Blog.Web.Interface;
using Blog.Web.Models;

namespace Blog.Web.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly HttpClient _client;

        public PostRepository(HttpClient client, ILogger<PostRepository> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<Post> CreatePost(Post model)
        {
            model.Id = "";
            var response = await _client.PostAsJson($"/api/Posts", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Post>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public Task<bool> DeletePost(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetPost(string id)
        {
            var response = await _client.GetAsync($"/api/Posts/{id}");
            return await response.ReadContentAs<List<Post>>();
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _client.GetAsync("/api/Subjects");
            return await response.ReadContentAs<List<Post>>();
        }

        public Task<bool> UpdatePost(Post model)
        {
            throw new NotImplementedException();
        }
    }
}
