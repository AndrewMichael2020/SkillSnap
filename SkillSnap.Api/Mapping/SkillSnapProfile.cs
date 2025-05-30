using AutoMapper;
using SkillSnap.Api.Models;
using SkillSnap.Shared;

namespace SkillSnap.Api.Mapping
{
    public class SkillSnapProfile : Profile
    {
        public SkillSnapProfile()
        {
            CreateMap<PortfolioUser, PortfolioUserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ReverseMap()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
