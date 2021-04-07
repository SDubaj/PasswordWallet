using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Models.Passwords;

namespace PasswordWallet_console.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<FunctionModel> Functions { get; set; }
        public DbSet<functionRun> functionRun { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<DataChange> DataChanges { get; set; }

    }
}