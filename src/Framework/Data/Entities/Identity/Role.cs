using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Role : IdentityRole<int>, IBaseEntity, ICreatedOn
    {
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        // Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(15);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
        }
    }
}