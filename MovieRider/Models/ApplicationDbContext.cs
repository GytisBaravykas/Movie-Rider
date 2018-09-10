using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieRider.Models;

namespace MovieRider.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //For seeding same stuff, for updates also 
            modelBuilder.Entity<MembershipType>().HasData(new MembershipType { Id = 1, SignUpFee=0, DurationInMonths=0, DiscountRate=0, Name="Pay as you go" });
            modelBuilder.Entity<MembershipType>().HasData(new MembershipType { Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Name="Monthly" });
            modelBuilder.Entity<MembershipType>().HasData(new MembershipType { Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Name="Season pass" });
            modelBuilder.Entity<MembershipType>().HasData(new MembershipType { Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20 , Name="Year pass"});

            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 1, Name="Action"});
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 2, Name="Comedy"});
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 3, Name="Drama"});
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 4, Name="Adventure"});
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 5, Name="Animation"});
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 6, Name="Fantacy"});


            /*modelBuilder.Entity<Customer>().HasData(new Customer { Id = 2, FirstName="Sarah",LastName="Denings", BirthDate=new DateTime(1993,6,5),IsSubscibedToNewsLetter=true, MembershipTypeId=2 });*/

        }
    }
}
