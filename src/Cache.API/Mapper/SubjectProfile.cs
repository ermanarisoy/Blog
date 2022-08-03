using AutoMapper;
using Cache.API.Entities;
using EventBus.Messages.Events;

namespace Cache.API.Mapper
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectEvent>().ForMember(x => x.PostEvents, cfg => cfg.MapFrom(y => y.Posts)).ReverseMap();
            CreateMap<Post, PostEvent>().ReverseMap();
        }
    }
}
