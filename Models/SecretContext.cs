using Microsoft.EntityFrameworkCore;

namespace DojoSecrets.Models{
    public class SecretContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public SecretContext(DbContextOptions<SecretContext> options) : base(options) { }

        public DbSet<users> users{get;set;}

        public DbSet<secrets> secrets{get;set;}

        public DbSet<likes> likes{get;set;}
        
    }
}