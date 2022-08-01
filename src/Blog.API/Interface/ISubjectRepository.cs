using Blog.API.Entities;

namespace Blog.API.Interface
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubject(string id);
        void CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(string id);
    }
}
