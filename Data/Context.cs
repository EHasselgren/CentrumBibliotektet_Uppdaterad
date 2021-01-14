using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentrumBibliotektet_Uppdaterad.Models;

namespace CentrumBibliotektet_Uppdaterad.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationAuthor> PublicationAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublicationAuthor>()
                .HasKey(pf => new { pf.AuthorId, pf.PublicationId });

            //modelBuilder.Entity<PublikationFörfattare>()
            //    .HasOne(pf => pf.publicationId)
            //    .WithMany(pf => )
        }
    }
}
