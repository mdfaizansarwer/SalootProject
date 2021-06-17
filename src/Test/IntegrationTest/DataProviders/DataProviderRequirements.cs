using AutoMapper;
using Date;
using Microsoft.EntityFrameworkCore;
using SalootProject.Api.Configuration;

namespace IntegrationTest.DataProviders
{
    public static class DataProviderRequirements
    {
        public static DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGenericDataProvider")
                .Options;
        }

        public static Mapper GetAutoMapper()
        {
            var ticketTypeProfiles = new TicketTypeProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(ticketTypeProfiles));
            return new Mapper(configuration);
        }
    }
}