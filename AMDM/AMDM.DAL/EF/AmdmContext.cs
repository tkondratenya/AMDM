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
    
        public AmdmContext(string connectionString)
            : base(connectionString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasMany(c => c.Chords)
                .WithMany(s => s.Songs)
                .Map(t => t.MapLeftKey("SongId")
                .MapRightKey("ChordId")
                .ToTable("SongChord"));
        }
    }
}
