using ClassLibraryEmotes;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace BethanysPieShopHRM.App.Models
{
    public class MockDataService
    {
        private static List<Emote> _emotes = new();
        private static List<User> _users = new();

        public static List<Emote> ListEmotes
        {
            get
            {
                _users = InitializeMockUsers();
                _emotes = InitializeMockEmotes();

                return _emotes;
            }
        }

        private static List<User> InitializeMockUsers()
        {
            var u1 = new User
            {
                UserId = 1,
                UserName = "xXJoselitoXx",
                JoinedDate = new DateTime(2021, 5, 14),
                OwnEmote = new List<Emote>(),
            };
            var u2 = new User
            {
                UserId = 1,
                UserName = "AlvaritoFornite829839",
                JoinedDate = new DateTime(2022, 5, 14),
                OwnEmote = new List<Emote>(),
            };
            return new List<User> { u1, u2 };
        }

        private static List<Emote> InitializeMockEmotes()
        {
            var e1 = new Emote
            {
                EmoteId = 1,
                Name = "oh",
                Description = "the poor coballa this one regrets what he has done",
                CreationDate = new DateTime(2023, 3, 11),
                UserId = _users[0].UserId,
                User = _users[0],
                Width = 128,
                Height = 128,
                Weight = "160KB",
                Approved = true,
                Version = 1.0,
                Status = Status.Active,
            };
            var e2 = new Emote
            {
                EmoteId = 2,
                Name = "LMAO",
                Description = "this freak cat laughs at you resoundingly",
                CreationDate = new DateTime(2023, 8, 21),
                UserId = _users[1].UserId,
                User = _users[1],
                Width = 384,
                Height = 96,
                Weight = "29KB",
                Approved = true,
                Version = 1.2,
                Status = Status.Inactive,
            };
            return new List<Emote> { e1, e2};
        }
    }
}


