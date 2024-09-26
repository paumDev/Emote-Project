using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEmotes
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int EmoteId { get; set; }
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public int Upvotes { get; set; } // Contador de votos positivos
        public int Downvotes { get; set; } // Contador de votos negativos
        public DateTime CreatedAt { get; set; }
    }

}
