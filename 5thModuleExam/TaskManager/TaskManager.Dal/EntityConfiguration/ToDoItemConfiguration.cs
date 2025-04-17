using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Entities;

namespace TaskManager.Dal.EntityConfiguration;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable("ToDoItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title).
            IsRequired(true).
            HasMaxLength(50);

        builder.Property(i => i.Description).
            IsRequired(true).
            HasMaxLength(50);

        builder.Property(i => i.IsCompleted).
            IsRequired(true);

        builder.Property(i => i.CreatedAt).
            IsRequired(true);

        builder.Property(i => i.DueDate).
            IsRequired(true);
    }
}
