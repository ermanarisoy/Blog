using AutoMapper;
using Cache.API.Entities;
using EventBus.Messages.Events;

namespace Cache.API.Mapper
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectEvent>().ReverseMap();
        }
    }
}
