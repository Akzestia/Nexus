using Microsoft.EntityFrameworkCore;
using Nexus.Server.Entity;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Nexus.Server.NexusContext
{
    public class NexusAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public NexusAppContext(DbContextOptions<NexusAppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
