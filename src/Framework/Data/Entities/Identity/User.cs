﻿using Core.Enums;
using Data.Entities.Files;
using Data.Entities.Logging;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity, ICreatedOn
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime Birthdate { get; set; }

        public GenderType Gender { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginDate { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationTime { get; set; }

        public int TeamId { get; set; }

        public int? ProfilePictureId { get; set; }

        public DateTime CreatedOn { get; set; }

        // Navigation properties
        public Team Team { get; set; }

        public FileModel ProfilePicture { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<TicketProcess> TicketProcesses { get; set; }

        public ICollection<EmailsLog> EmailsLogs { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(15);

            builder.Property(p => p.Firstname).IsRequired().HasMaxLength(35);

            builder.Property(p => p.Lastname).IsRequired().HasMaxLength(35);

            builder.Property(p => p.Email).IsRequired().HasMaxLength(320);

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.Property(p => p.Birthdate).HasColumnType("date");

            builder.Property(p => p.RefreshToken).HasMaxLength(50);

            builder.Property(p => p.RefreshTokenExpirationTime);

            builder.HasOne(p => p.Team)
                   .WithMany(p => p.Users)
                   .HasForeignKey(p => p.TeamId);

            builder.HasOne(p => p.ProfilePicture)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(p => p.ProfilePictureId);
        }
    }
}