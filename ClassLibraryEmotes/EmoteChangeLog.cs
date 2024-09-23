using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEmotes
{
    public class EmoteChangeLog
    {
        public int EmoteChangeLogId { get; set; }
        [ForeignKey("Emote")]
        public int EmoteId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? Description { get; set; }
        public Emote? Emote { get; set; }
    }

}
