using AutoMapper;
using Data.Entities;
using Services.Domain;

namespace SalootProject.Api.Configuration
{
    public class TeamProfiles : Profile
    {
        #region Ctor

        public TeamProfiles()
        {
            TeamProfile();
        }

        #endregion Ctor

        #region Methods

        public void TeamProfile()
        {
            CreateMap<TeamCreateUpdateViewModel, Team>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<Team, TeamViewModel>();

            CreateMap<Team, TeamListViewModel>();
        }

        #endregion Methods
    }
}