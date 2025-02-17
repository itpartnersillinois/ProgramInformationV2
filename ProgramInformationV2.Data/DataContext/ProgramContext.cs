using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.DataContext {

    public class ProgramContext : DbContext {
        private readonly Guid _id;

        public ProgramContext() : base() {
            _id = Guid.NewGuid();
            Debug.WriteLine($"{_id} context created.");
        }

        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options) {
            _id = Guid.NewGuid();
            Debug.WriteLine($"{_id} context created.");
        }

        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldSource> FieldSources { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SecurityEntry> SecurityEntries { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<TagSource> TagSources { get; set; }

        public override void Dispose() {
            Debug.WriteLine($"{_id} context disposed.");
            base.Dispose();
        }

        public override ValueTask DisposeAsync() {
            Debug.WriteLine($"{_id} context disposed async.");
            return base.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            Debug.WriteLine($"{_id} context starting initial setup.");
            modelBuilder.Entity<Source>().HasData(new List<Source>
            {
                new() { Id = -1, Code = "test", Title = "Test Entry", CreatedByEmail = "jonker@illinois.edu", IsTest = true },
            });
            modelBuilder.Entity<SecurityEntry>().HasData(new List<SecurityEntry>
            {
                new("jonker", -1) { Id = -1, IsOwner = true }
            });
            modelBuilder.Entity<Field>().HasData(new List<Field> {
                new() { Id = 1, Title = "Alternate Link URL & Name", InitialDescription = "This should link to an alternative page by another college involved in running/hosting the program. You must also add the text the link will be applied to.", FieldType = FieldType.Link, CategoryType = CategoryType.Program  }
            });
            Debug.WriteLine($"{_id} context finishing initial setup.");
        }
    }
}