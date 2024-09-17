using ClassLibraryEmotes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Components
{
    public partial class PopUpTrend
    {
        private Emote? _Emote;

        [Parameter]
        public Emote? Emote { get; set; }

        protected override Task OnParametersSetAsync()
        {
            if (Emote != null)
            {
                _Emote = Emote;
            }
            return Task.CompletedTask; // No necesitas await si no hay operaciones asincrónicas
        }

        public void Close()
        {
            _Emote = null;
        }
    }
}
