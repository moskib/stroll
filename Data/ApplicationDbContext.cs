using Microsoft.EntityFrameworkCore;
using Stroll.Models;

namespace Stroll.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Usertypes> UserTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usertypes>().HasData(new Usertypes[]{
                new Usertypes
                {
                    UID = 1,
                    Type = "client"
                },
                new Usertypes
                {
                    UID = 2,
                    Type = "business_employee"
                },
                new Usertypes
                {
                    UID = 3,
                    Type = "business_admin"
                }
            }) ;

            modelBuilder.Entity<AppointmentStatus>().HasData(new AppointmentStatus[]
            {
                new AppointmentStatus
                {
                    UID = 1,
                    Status = "open"
                },
                new AppointmentStatus
                {
                    UID = 2,
                    Status = "taken"
                },
                new AppointmentStatus
                {
                    UID = 3,
                    Status = "cancelled"
                }
            });

            //modelBuilder.Entity<User>().HasData(new User[]
            //{
            //    new User
            //    {
            //        UID = new System.Guid(),
            //        Email = "client@text.com",
            //        Password = "123456",
            //        Type = 1
            //    },
            //    new User
            //    {
            //        UID = new System.Guid(),
            //        Email = "business-admin@test.com",
            //        Password = "123456",
            //        Type = 3
            //    }
            //});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
    }
}
