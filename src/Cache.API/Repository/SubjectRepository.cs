using Cache.API.Entities;
using Cache.API.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cache.API.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDistributedCache _redisCache;

        public SubjectRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public void CreateSubject(Subject subject)
        {
            _redisCache.SetString(subject.Id, JsonConvert.SerializeObject(subject));
        }

        public void DeleteSubject(string id)
        {
            _redisCache.Remove(id);
        }

        public Subject GetSubject(string id)
        {
            var subject = _redisCache.GetString(id);

            if (String.IsNullOrEmpty(subject))
                return null;

            return JsonConvert.DeserializeObject<Subject>(subject);
        }

        public IEnumerable<Subject> GetSubjects()
        {
            throw new NotImplementedException();
        }

        public Subject UpdateSubject(Subject subject)
        {
            _redisCache.SetString(subject.Id, JsonConvert.SerializeObject(subject));

            return GetSubject(subject.Id);
        }
    }
}
