using AutoMapper;
using Blog.API.Entities;
using EventBus.Messages.Events;

namespace Blog.API.Mapper
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectEvent>().ReverseMap();
        }
    }
}
