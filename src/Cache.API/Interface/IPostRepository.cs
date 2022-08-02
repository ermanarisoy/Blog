using Cache.API.Entities;

namespace Cache.API.Interface
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(string id);
        void CreatePost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(string id);
    }
}
