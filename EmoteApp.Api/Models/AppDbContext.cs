using ClassLibraryEmotes;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopHRM.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Emote> Emotes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmoteChangeLog> EmoteChangeLogs { get; set; }
        public DbSet<Feedback> Feedback { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<User>().HasData(new User { UserId = 1, UserName = "JuanitoXD3234542" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 2, UserName = "Jorge353424234" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 3, UserName = "Abascalterrorista" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 4, UserName = "gato_vietnamita" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 5, UserName = "jose:D" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 6, UserName = "chinchon" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 7, UserName = "bobooklk" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 8, UserName = "miguelin_de_murcia" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 9, UserName = "nayara_rivas_who" });

            modelBuilder.Entity<Emote>().HasData(new Emote
            {
                EmoteId = 1,
                Name = "oh",
                Description = "the poor coballa this one regrets what he has done",
                CreationDate = new DateTime(2023, 3, 11),
                UserId = 1,
                Width = 128,
                Height = 128,
                Weight = "160KB",
                Approved = true,
                Version = 1.0,
                Status = Status.Active,
            });
            modelBuilder.Entity<Emote>().HasData(new Emote
            {
                EmoteId = 2,
                Name = "LMAO",
                Description = "this freak cat laughs at you resoundingly",
                CreationDate = new DateTime(2023, 8, 21),
                UserId = 2,
                Width = 384,
                Height = 96,
                Weight = "29KB",
                Approved = true,
                Version = 1.2,
                Status = Status.Inactive,
            });
        }
    }
}
