using AutoMapper;
using Data.Entities.Identity;
using Services.Domain;

namespace SalootProject.Api.Configuration
{
    public class TenantProfiles : Profile
    {
        #region Ctor

        public TenantProfiles()
        {
            CreateMap<Tenant, TenantViewModel>();

            CreateMap<Tenant, TenantListViewModel>();
        }

        #endregion Ctor
    }
}
