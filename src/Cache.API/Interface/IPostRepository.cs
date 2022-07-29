using Cache.API.Entities;

namespace Cache.API.Interface
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(int id);
        void CreatePost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int id);
    }
}
