using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEmotes
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? JoinedDate { get; set; }
        public List<Emote>? OwnEmote { get; set; }
    }
}
