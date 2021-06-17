﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Files
{
    public class FileOnFileSystem : FileModel
    {
        public string FilePath { get; set; }
    }

    public class FileOnFileSystemConfiguration : IEntityTypeConfiguration<FileOnFileSystem>
    {
        public void Configure(EntityTypeBuilder<FileOnFileSystem> builder)
        {
            builder.Property(p => p.FilePath).HasMaxLength(500);
        }
    }
}
