using Blog.Web.Models;

namespace Blog.Web.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<IEnumerable<Post>> GetPost(string id);
        Task<Post> CreatePost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(string id);
    }
}
