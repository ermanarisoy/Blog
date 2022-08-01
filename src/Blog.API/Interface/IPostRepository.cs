using Blog.API.Entities;

namespace Blog.API.Interface
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(string id);
        IEnumerable<Post> GetPostsBySubject(string id);
        void CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(string id);
    }
}
