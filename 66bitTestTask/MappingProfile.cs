using _66bitTestTask.Models;
using _66bitTestTask.ViewModels;
using AutoMapper;

namespace _66bitTestTask
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<SoccerPlayerViewModel, SoccerPlayer>();
            CreateMap<SoccerPlayer, SoccerPlayerViewModel>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team.Name));
        }
    }
}
