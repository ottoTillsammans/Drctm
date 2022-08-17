using Microsoft.EntityFrameworkCore;

namespace ServerSln
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options): base(options)
        { }

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
