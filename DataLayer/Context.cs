using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLayer;

namespace DataLayer
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-OERL0I6O; Database=School; Trusted_Connection=true");
        }

        public DbSet<Teachers> tbl_teachers { get; set; }

        public DbSet<Students> tbl_students { get; set; }

        public DbSet<Lessons> tbl_lessons { get; set; }

        public DbSet<Notes> tbl_notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teachers>()
                .HasOne(t => t.Lesson)
                .WithMany()
                .HasForeignKey(t => t.LessonID);

            modelBuilder.Entity<Students>()
                .HasOne(s => s.Note)
                .WithMany()
                .HasForeignKey(s => s.NoteID);

            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Lesson)
                .WithMany()
                .HasForeignKey(n => n.LessonID);
        }
    }
}
