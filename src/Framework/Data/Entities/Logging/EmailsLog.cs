﻿using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data.Entities.Logging
{
    public class EmailsLog : IBaseEntity, ICreatedOn
    {
        public int Id { get; set; }

        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int? ToUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        // Navigation properties
        public User ToUser { get; set; }

        public ICollection<EmailsLogFileModel> EmailsLogFileModels { get; set; }
    }

    public class EmailsLogConfiguration : IEntityTypeConfiguration<EmailsLog>
    {
        public void Configure(EntityTypeBuilder<EmailsLog> builder)
        {
            builder.Property(p => p.ToEmail).HasMaxLength(320);

            builder.HasOne(p => p.ToUser)
                   .WithMany(p => p.EmailsLogs)
                   .HasForeignKey(p => p.ToUserId);
        }
    }
}
