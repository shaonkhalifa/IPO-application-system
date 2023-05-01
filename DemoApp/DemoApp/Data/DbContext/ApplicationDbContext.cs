using DemoApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;

namespace DemoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Company> Company { get; set; } = default!;
        public DbSet<IpoApplicationDetails> ApplicationDetails { get; set; } = default!;
        public DbSet<IpoInformation> IpoInformations { get; set; } = default!;

    }
}