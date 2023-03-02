using System.ComponentModel.DataAnnotations;

namespace NET_Project.Models {

    // Horses
    public class Horse {

        // Properties
        public int HorseId { get; set; } // ID

        [Required]
        public string? Name { get; set; } // Name

        public string? Nickname { get; set; } // Nickname

        [Required]
        public string? Gender { get; set; } // Gender

        [Required]
        public string? Breed { get; set; } // Breed

        [Required]
        public int Level { get; set; } // Level

        [Required]
        public string? Owner { get; set; } // Owner

        public string? Picture { get; set; } // Picture

        [DataType(DataType.Text)]
        public string? Info { get; set; } // Info

    }

        public class Note {

        // Properties
        public int NoteId { get; set; } // ID

        [Required]
        [Display(Name = "Titel")]
        public string? Title { get; set; } // Title

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Anteckning")]
        public string? Content { get; set; } // Content

    }

}