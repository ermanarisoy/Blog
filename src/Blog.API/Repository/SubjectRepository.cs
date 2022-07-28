using Blog.API.Entities;
using Blog.API.Interface;

namespace Blog.API.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly BlogAPIContext _context;

        public SubjectRepository(BlogAPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateSubject(Subject subject)
        {
            subject.CreateOn = DateTime.Now;
            _context.Add(subject);
            _context.SaveChanges();
        }

        public bool DeleteSubject(int id)
        {
            var subject = _context.Subject.FirstOrDefault(x => x.Id == id);
            _context.Remove(subject);
            return _context.SaveChanges() > 0;
        }

        public Subject GetSubject(int id)
        {
            return _context.Subject.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return _context.Subject.ToList();
        }

        public bool UpdateSubject(Subject subject)
        {
            _context.Update(subject);
            return _context.SaveChanges() > 0;
        }
    }
}
