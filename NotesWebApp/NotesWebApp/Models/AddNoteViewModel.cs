using System.ComponentModel.DataAnnotations;

namespace NotesWebApp.Models
{
    public class AddNoteViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
