using Core.Enums;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Entities
{
    public class ActionsLog : IBaseEntity, ICreatedOn
    {
        public int Id { get; set; }

        public string ClientIpAddress { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string HttpMethod { get; set; }

        public string ActionArguments { get; set; }

        public string RequestPath { get; set; }

        public ApiResultStatusCode ApiResultStatusCode { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class ActionsLogConfiguration : IEntityTypeConfiguration<ActionsLog>
    {
        public void Configure(EntityTypeBuilder<ActionsLog> builder)
        {
            builder.Property(p => p.ClientIpAddress).HasMaxLength(50);

            builder.Property(p => p.ControllerName).HasMaxLength(15);

            builder.Property(p => p.ActionName).HasMaxLength(15);

            builder.Property(p => p.HttpMethod).HasMaxLength(10);

            builder.Property(p => p.RequestPath).HasMaxLength(50);

            builder.Property(p => p.ActionArguments);
        }
    }
}