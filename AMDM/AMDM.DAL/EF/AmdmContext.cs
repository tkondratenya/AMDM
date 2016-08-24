using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMDM.DAL.Entities;
using System.Data.Entity;

namespace AMDM.DAL.EF
{
    public class AmdmContext : DbContext
    {
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Chord> Chords { get; set; }

        static AmdmContext()
        {
            Database.SetInitializer<AmdmContext>(new AmdmDbInitializer());
        }
        public AmdmContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class AmdmDbInitializer : DropCreateDatabaseIfModelChanges<AmdmContext>
    {
        protected override void Seed(AmdmContext db)
        {
            db.Performers.Add(new Performer { Name = "Mindless Self Indulgence" });
            db.SaveChanges();
        }
    }
}
