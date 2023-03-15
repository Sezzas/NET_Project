using System.ComponentModel.DataAnnotations;

namespace NET_Project.Models {

    // Horses
    public class Horse {

        // Properties
        public int HorseId { get; set; } // ID

        [Required(ErrorMessage = "Du måste ange hästens namn.")]
        public string? Name { get; set; } // Name

        public string? Nickname { get; set; } // Nickname

        [Required(ErrorMessage = "Du måste ange hästens kön.")]
        public string? Gender { get; set; } // Gender

        [Required(ErrorMessage = "Du måste ange hästens ras.")]
        public string? Breed { get; set; } // Breed

        [Required(ErrorMessage = "Du måste ange hästens level 1-15.")]
        [Range(0, 15, ErrorMessage = "Leveln kan endast vara 1-15.")]
        public int? Level { get; set; } // Level

        [Required(ErrorMessage = "Du måste ange hästens ägare.")]
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

        public class News {

        // Properties
        public int NewsId { get; set; } // ID

        [Required]
        public string? Title { get; set; } // Title

        [Required]
        [DataType(DataType.Text)]
        public string? Content { get; set; }  // Content

    }

}