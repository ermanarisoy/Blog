using AutoMapper;
using Blog.API.Entities;
using Blog.API.Interface;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepository, ISubjectRepository subjectRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _postRepository = postRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPost()
        {
            var posts = _postRepository.GetPosts();

            if (posts == null)
            {
                return NotFound();
            }
            return posts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Post>> GetPosts(string id)
        {
            var posts = _postRepository.GetPosts().Where(x => x.SubjectId == id).ToList();

            if (posts == null)
            {
                return NotFound();
            }
            return posts;
        }

        // GET: api/Posts/5
        //[HttpGet("{id}")]
        //public ActionResult<Post> GetPost(string id)
        //{
        //    if (_postRepository.GetPosts() == null)
        //    {
        //        return NotFound();
        //    }
        //    var post = _postRepository.GetPost(id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return post;
        //}

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
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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
        public ActionResult<Post> PostPost(Post post)
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
            post.Id = Guid.NewGuid().ToString();
            post.CreationDate = DateTime.Now;
            _postRepository.CreatePost(post);
            subject = _subjectRepository.GetSubject(post.SubjectId);
            var postList = _postRepository.GetPostsBySubject(post.SubjectId);
            subject.Posts = new List<Post>();
            subject.Posts.AddRange(postList);
            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<SubjectEvent>(subject);
            _publishEndpoint.Publish<SubjectEvent>(eventMessage);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(string id)
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

        private bool PostExists(string id)
        {
            return _postRepository.GetPost(id) != null;
        }
    }
}
