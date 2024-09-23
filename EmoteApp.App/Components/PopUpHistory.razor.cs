using ClassLibraryEmotes;
using EmoteApp.App.Services;
using Microsoft.AspNetCore.Components;

namespace EmoteApp.App.Components
{
    public partial class PopUpHistory : ComponentBase
    {
        [Parameter] public int EmoteId { get; set; }
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }
        [Inject] public IEmoteDataService? EmoteDataService { get; set; }

        private List<EmoteChangeLog> ChangeLogs { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            if (IsVisible)
            {
                ChangeLogs = await EmoteDataService.GetEmoteChangeLogsAsync(EmoteId);
            }
        }

        private async Task CloseModal()
        {
            await OnClose.InvokeAsync(false);
        }
    }
}