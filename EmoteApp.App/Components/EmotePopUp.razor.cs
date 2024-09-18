using Microsoft.AspNetCore.Components;
using ClassLibraryEmotes;
using EmoteApp.App.Services;

namespace EmoteApp.App.Components
{
    public partial class EmotePopUp
    {
        private Emote? _Emote;
        public User? UserDetails{ get; set; } = new User();
        private int _UserId;
        [Parameter]
        public Emote? Emote { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Inject]
        public IUserDataService? UserDataService { get; set; }
        private bool IsVisible { get; set; } = true;

        protected override async Task OnParametersSetAsync()
        {
            if (Emote != null)
            {
                _Emote = Emote;
                _UserId = _Emote.UserId;

                if (UserDataService != null)
                {
                    UserDetails = await UserDataService.GetUserDetails(_UserId);
                }
                else
                {
                    // Manejar el caso cuando UserDataService es null
                    UserDetails = new User(); // o manejarlo como sea necesario
                }
            }
            else
            {
                // Manejar el caso cuando Emote es null
                _UserId = 0; // o algún valor por defecto o acción
                UserDetails = new User(); // o manejarlo como sea necesario
            }
        }

        public async Task Close()
        {
            _Emote = null;
            IsVisible = false;
            await OnClose.InvokeAsync();
        }
    }
}
