using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace EmoteApp.App.Shared
{
    public partial class MainLayout
    {
        private bool isDarkTheme = false;

        [Inject]
        public IJSRuntime js { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Cargar el tema guardado del localStorage
                var theme = await js.InvokeAsync<string>("localStorage.getItem", "theme");
                isDarkTheme = theme == "dark";
                await js.InvokeVoidAsync("loadTheme");
                StateHasChanged();
            }
        }

        private async Task ToggleTheme(ChangeEventArgs e)
        {
            isDarkTheme = (bool)e.Value;
            var theme = isDarkTheme ? "dark" : "light";
            await js.InvokeVoidAsync("setTheme", theme);
        }
    }
}
