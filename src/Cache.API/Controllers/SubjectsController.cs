using Cache.API.Entities;
using Cache.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        // GET: api/Subjects
        [HttpGet]
        public ActionResult<IEnumerable<Subject>> GetSubject()
        {
            var subjects = _subjectRepository.GetSubjects().ToList();

            if (subjects == null)
            {
                return NotFound();
            }
            return subjects;
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubject(string id)
        {
            var subject = _subjectRepository.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }
            return subject;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutSubject(string id, Subject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            try
            {
                _subjectRepository.UpdateSubject(subject);
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Subject> PostSubject(Subject subject)
        {
            _subjectRepository.CreateSubject(subject);

            return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(string id)
        {
            if (_subjectRepository.GetSubjects() == null)
            {
                return NotFound();
            }
            var subject = _subjectRepository.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }

            _subjectRepository.DeleteSubject(id);

            return NoContent();
        }

        private bool SubjectExists(string id)
        {
            return _subjectRepository.GetSubject(id) != null;
        }
    }
}
