using System.ComponentModel.DataAnnotations;

namespace NotesWebApp.Models
{
    public class NoteEditModel
    {
        public readonly int id;

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
