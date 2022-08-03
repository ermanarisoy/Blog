using Cache.API.Entities;
using Cache.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubjectRepository _subjectRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPost()
        {
            var posts = _postRepository.GetPosts().ToList();

            if (posts == null)
            {
                return NotFound();
            }
            return posts;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(string id)
        {
            var post = _postRepository.GetPost(id);

            if (post == null)
            {
                return NotFound();
            }
            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPost(string id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            try
            {
                _postRepository.UpdatePost(post);
            }
            catch (Exception)
            {
                if (!SubjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Post> PostSubject(Post post)
        {
            if (post == null)
            {
                return Problem("Entity set 'Post'  is null.");
            }
            var subject = _subjectRepository.GetSubject(post.SubjectId);
            if (subject == null)
            {
                return Problem("Entity set 'Subject'  is null.");
            }
            _postRepository.CreatePost(post);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(string id)
        {
            if (_postRepository.GetPosts() == null)
            {
                return NotFound();
            }
            var post = _postRepository.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            _postRepository.DeletePost(id);

            return NoContent();
        }

        private bool SubjectExists(string id)
        {
            return _postRepository.GetPost(id) != null;
        }
    }
}
