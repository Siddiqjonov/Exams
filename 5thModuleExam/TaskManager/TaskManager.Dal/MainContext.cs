using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Entities;
using TaskManager.Dal.EntityConfiguration;

namespace TaskManager.Dal;

public class MainContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
