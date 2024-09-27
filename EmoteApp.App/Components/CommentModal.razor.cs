using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ClassLibraryEmotes;
using EmoteApp.App.Pages;

namespace EmoteApp.App.Components
{
    public partial class CommentModal
    {
        [Parameter]
        public string? EmoteId { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<string> OnSubmit { get; set; }

        private string Comment { get; set; } = string.Empty;

        private async Task Close()
        {
            IsVisible = false;
            await OnClose.InvokeAsync();
        }

        private async Task SubmitComment()
        {
            if (!string.IsNullOrWhiteSpace(Comment))
            {
                await OnSubmit.InvokeAsync(Comment);
                Comment = string.Empty;
                await Close();
            }
        }
    }
}
