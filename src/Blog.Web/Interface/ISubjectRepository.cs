using Blog.Web.Models;

namespace Blog.Web.Interface
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>>GetSubjects();
        Task<Subject> GetSubject(string id);
        Task<Subject> CreateSubject(Subject subject);
        Task<bool> UpdateSubject(Subject subject);
        Task<bool> DeleteSubject(string id);
    }
}
