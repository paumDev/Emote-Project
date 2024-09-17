using ClassLibraryEmotes;
namespace BethanysPieShopHRM.Api.Models
{
    public interface IEmoteRepository
    {
        IEnumerable<Emote> GetAllEmotes();
        Emote GetEmoteById(int employeeId);
        Emote AddEmote(Emote employee);
        Emote UpdateEmote(Emote employee);
        void DeleteEmote(int employeeId);
        //IEnumerable<EmployeeListModel> GetLongEmployeeList();
        //IEnumerable<EmployeeListModel> GetTakeLongEmployeeList(int request, int count);
    }
}
