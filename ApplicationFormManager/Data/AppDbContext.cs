using ApplicationFormManager.ApplicationFormManager.Models;
using ApplicationFormManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.ApplicationFormManager.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ApplicationForm> ApplicationForms { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationForm>()
                .ToContainer("ApplicationForms")
                .HasPartitionKey(a => a.Id)
                .HasNoDiscriminator();

            modelBuilder.Entity<Submission>()
                .ToContainer("ApplicationSubmissions")
                .HasPartitionKey(x => x.Id)
                .HasNoDiscriminator();

        }
    }
}