using Blog.Web.Extensions;
using Blog.Web.Interface;
using Blog.Web.Models;

namespace Blog.Web.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly HttpClient _client;

        public SubjectRepository(HttpClient client, ILogger<SubjectRepository> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Subject> CreateSubject(Subject model)
        {
            var response = await _client.PostAsJson($"/api/Subjects", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Subject>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public Task<bool> DeleteSubject(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Subject> GetSubject(string id)
        {
            var response = await _client.GetAsync($"/api/Subjects/{id}");
            return await response.ReadContentAs<Subject>();
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            var response = await _client.GetAsync("/api/Subjects");
            return await response.ReadContentAs<List<Subject>>();
        }

        public Task<bool> UpdateSubject(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}
