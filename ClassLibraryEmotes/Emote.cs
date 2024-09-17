using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEmotes
{
    public class Emote
    {
        public int EmoteId { get; set; }
        //[Required]
        //[StringLength(50, ErrorMessage = "Name is too long.")]
        public string? Name { get; set; }
        [StringLength(59, ErrorMessage = "Desc is too long.")]
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Weight { get; set; }
        public bool Approved { get; set; }
        public double Version { get; set; }
        public Status Status { get; set; }

        [NotMapped]
        public byte[]? ImageEmoteContent { get; set; }
        public string? ImageName { get; set; }
        public double NumDownload { get; set; }
        public bool Trending { get; set; }

    }
}
