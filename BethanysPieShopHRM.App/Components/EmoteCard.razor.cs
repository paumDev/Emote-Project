using ClassLibraryEmotes;
using Microsoft.AspNetCore.Components;
namespace BethanysPieShopHRM.App.Components
{
    public partial class EmoteCard
    {
        [Parameter]
        public Emote Emote { get; set; } = default!;
        [Parameter]
        public EventCallback<Emote> EmoteQuickViewClicked { get; set; }
        [Parameter]
        public EventCallback<Emote> EmoteTrendPopUp { get; set; }
    }
}
