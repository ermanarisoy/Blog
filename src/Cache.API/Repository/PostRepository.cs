using Cache.API.Entities;
using Cache.API.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cache.API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IDistributedCache _redisCache;

        public PostRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public void CreatePost(Post post)
        {
            _redisCache.SetString(post.Id, JsonConvert.SerializeObject(post));
        }

        public void DeletePost(string id)
        {
            _redisCache.Remove(id);
        }

        public Post GetPost(string id)
        {
            var post = _redisCache.GetString(id);

            if (String.IsNullOrEmpty(post))
                return null;

            return JsonConvert.DeserializeObject<Post>(post);
        }

        public IEnumerable<Post> GetPosts()
        {
            throw new NotImplementedException();
        }

        public Post UpdatePost(Post post)
        {
            _redisCache.SetString(post.Id, JsonConvert.SerializeObject(post));

            return GetPost(post.Id);
        }
    }
}
