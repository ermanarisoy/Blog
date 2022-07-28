using Blog.API.Entities;

namespace Blog.API.Interface
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(int id);
        void CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(int id);
    }
}
