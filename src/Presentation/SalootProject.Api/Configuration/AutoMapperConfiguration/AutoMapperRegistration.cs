using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace SalootProject.Api.Configuration
{
    public static class AutoMapperRegistration
    {
        public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Profile> profileList = new List<Profile>()
            {
                new AuthenticationProfiles(),
                new TeamProfiles(),
                new TenantProfiles(),
                new TicketTypeProfiles()
            };

            services.AddAutoMapper(config =>
            {
                config.AddProfiles(profileList);
            }, assemblies);
        }
    }
}