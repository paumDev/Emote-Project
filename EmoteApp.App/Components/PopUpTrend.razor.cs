using ClassLibraryEmotes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmoteApp.App.Components
{
    public partial class PopUpTrend
    {
        private Emote? _Emote;

        [Parameter]
        public Emote? Emote { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }
        private bool IsVisible { get; set; } = true;

        protected override Task OnParametersSetAsync()
        {
            if (Emote != null)
            {
                _Emote = Emote;
            }
            return Task.CompletedTask; // No necesitas await si no hay operaciones asincrónicas
        }

        public async Task Close()
        {
            _Emote = null;
            IsVisible = false;
            await OnClose.InvokeAsync();
        }
    }
}
