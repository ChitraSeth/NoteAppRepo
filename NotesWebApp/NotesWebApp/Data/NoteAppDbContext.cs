using Microsoft.EntityFrameworkCore;
using NotesWebApp.Models.Entites;

namespace NotesWebApp.Data
{
    public class NoteAppDBContext:DbContext
    {
        public NoteAppDBContext(DbContextOptions<NoteAppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }

        //public DbSet<Tag> Tags { get; set; }

        //public DbSet<NoteTag> NoteTags { get; set; }
    }
}
