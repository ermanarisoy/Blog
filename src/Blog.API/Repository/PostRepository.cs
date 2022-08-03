using Blog.API.Entities;
using Blog.API.Interface;

namespace Blog.API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogAPIContext _context;

        public PostRepository(BlogAPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreatePost(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public bool DeletePost(string id)
        {
            var post = _context.Post.FirstOrDefault(x => x.Id == id);
            _context.Remove(post);
            return _context.SaveChanges() > 0;
        }

        public Post GetPost(string id)
        {
            return _context.Post.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Post> GetPostsBySubject(string id)
        {
            return _context.Post.Where(x => x.SubjectId == id).ToList();
        }

        public IEnumerable<Post> GetPosts()
        {
            return _context.Post.ToList();
        }

        public bool UpdatePost(Post post)
        {
            _context.Update(post);
            return _context.SaveChanges() > 0;
        }
    }
}
