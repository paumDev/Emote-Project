using ClassLibraryEmotes;

namespace BethanysPieShopHRM.Api.Models
{
    public class EmoteRepository : IEmoteRepository
    {
        private readonly AppDbContext _appDbContext;
        private Random random = new Random();

        public EmoteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Emote> GetAllEmotes()
        {
            return _appDbContext.Emotes;
        }

        public Emote GetEmoteById(int emoteId)
        {
            return _appDbContext.Emotes.FirstOrDefault(c => c.EmoteId == emoteId);
        }

        public Emote AddEmote(Emote emote)
        {
            var addedEntity = _appDbContext.Emotes.Add(emote);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Emote UpdateEmote(Emote emote)
        {
            var foundEmote = _appDbContext.Emotes.FirstOrDefault(e => e.EmoteId == emote.EmoteId);

            if (foundEmote != null)
            {
                foundEmote.Name = emote.Name;
                foundEmote.Version = emote.Version;
                foundEmote.Weight = emote.Weight;
                foundEmote.Approved = emote.Approved;
                foundEmote.Height = emote.Height;
                foundEmote.Description = emote.Description;
                foundEmote.CreationDate = emote.CreationDate;
                foundEmote.UserId = emote.UserId;
                foundEmote.NumDownload = emote.NumDownload;


                _appDbContext.SaveChanges();

                return foundEmote;
            }

            return null;
        }

        public void DeleteEmote(int emoteid)
        {
            var foundEmote = _appDbContext.Emotes.FirstOrDefault(e => e.EmoteId == emoteid);
            if (foundEmote == null) return;

            _appDbContext.Emotes.Remove(foundEmote);
            _appDbContext.SaveChanges();
        }
    }
}
