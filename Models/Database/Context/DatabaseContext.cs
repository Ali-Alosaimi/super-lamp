using System;
using Microsoft.EntityFrameworkCore;
using Models.Database.Entities;

namespace Models.Database.Context
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Users> users { get; set; }
        public DbSet<UserRoles> userRoles { get; set; }
        public DbSet<Roles> roles { get; set; }

        public DbSet<Categories> categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<ProductCart> ProductCart { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<UserCard> userCards { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        //public DatabaseContext() { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.
        //        UseSqlServer(@"Server=.\SQLEXPRESS;Database=Binosaimi;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new Users()
            {
                email = "admin@gmail.com",
                password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5",//12345
                userId = 1,
                name = "Admin"
            };
            modelBuilder.Entity<Users>().HasData(user);
            var userRoles = new UserRoles()
            {
                id = 1,
                userId = 1,
                roleId = 1
            };
            modelBuilder.Entity<UserRoles>().HasData(userRoles);
            var adminRole = new Roles()
            {
                roleId = 1,
                roleName = "ADMIN"
            };
            modelBuilder.Entity<Roles>().HasData(adminRole);
            var userRole = new Roles()
            {
                roleId = 2,
                roleName = "USER"
            };
            modelBuilder.Entity<Roles>().HasData(userRole);
        }
    }
}
