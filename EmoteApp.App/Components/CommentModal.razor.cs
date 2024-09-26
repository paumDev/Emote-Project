using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmoteApp.App.Components
{
    public partial class CommentModal
    {
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
            // Enviar el comentario
            await OnSubmit.InvokeAsync(Comment);

            // Cerrar el modal después de enviar
            Comment = string.Empty;
            await Close();
        }
    }
}
