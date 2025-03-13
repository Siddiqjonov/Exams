using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegisterBot.Dal.Entities;
using UserRegisterBot.Dal.EntityConfiguration;

namespace UserRegisterBot.Dal;

public class MainContext : DbContext
{
    public DbSet<BotUser> Users { get; set; }

    public object Find(long id)
    {
        throw new NotImplementedException();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured is false)
        {
            var connectionString = "Data Source=localhost\\SQLEXPRESS;User ID=sa;Password=1;Initial Catalog=UserRegisterBot;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BotUserConfiguration());
    }
}
