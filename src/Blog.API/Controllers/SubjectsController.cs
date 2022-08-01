using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.API.Entities;
using Blog.API.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
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
          if (_subjectRepository.GetSubjects() == null)
          {
              return NotFound();
          }
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
            catch (DbUpdateConcurrencyException)
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
          if (subject == null)
          {
              return Problem("Entity set 'Subject'  is null.");
          }
            //_subjectRepository.CreateSubject(subject);

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<SubjectEvent>(subject);

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
