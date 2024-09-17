using Microsoft.AspNetCore.Components;
using ClassLibraryEmotes;
using BethanysPieShopHRM.App.Services;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.IO;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmoteDetail
    {
        [Inject]
        public IEmoteDataService? EmoteDataService { get; set; }

        [Inject]
        public IUserDataService? UserDataService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public int UserId { get; set; }

        [Parameter]
        public string? EmoteId { get; set; }

        public Emote? Emote { get; set; } = new Emote();
        public User? User { get; set; } = new User();

        protected override async Task OnInitializedAsync()
        {
            Emote = await EmoteDataService.GetEmoteDetails(int.Parse(EmoteId));
            UserId = Emote.UserId;
            User = await UserDataService.GetUserDetails(UserId);

            // URL de la imagen
            var imageUrl = Emote.ImageName;

            // Realizar una solicitud HTTP para obtener la imagen
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(imageUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentLength = response.Content.Headers.ContentLength ?? 0;

                using var imageStream = await response.Content.ReadAsStreamAsync();
                using var memoryStream = new MemoryStream();

                await imageStream.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using var img = await Image.LoadAsync(memoryStream);

                Emote.Weight = GetFileSizeInReadableFormat(contentLength);  // Tamaño del archivo
                Emote.Width = img.Width;  // Ancho de la imagen cargada
                Emote.Height = img.Height; // Alto de la imagen cargada
            }
            else
            {
                // Manejo de error: la solicitud HTTP falló
                throw new HttpRequestException($"No se pudo obtener la imagen desde la URL: {imageUrl}");
            }
        }

        // Método para descargar la imagen
        protected async Task DownloadImage()
        {
            var imageUrl = Emote?.ImageName;

            if (imageUrl != null)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    var fileName = Path.GetFileName(imageUrl);

                    // Invocar el método JavaScript para descargar la imagen y update de imagen 
                    await JSRuntime.InvokeVoidAsync("downloadImage", Convert.ToBase64String(imageBytes), fileName);

                    Emote.NumDownload++;
                    await EmoteDataService.UpdateEmote(Emote);
                }
                else
                {
                    throw new HttpRequestException($"No se pudo descargar la imagen desde la URL: {imageUrl}");
                }
            }
        }

        // Método para obtener el tamaño del archivo en un formato legible
        string GetFileSizeInReadableFormat(long sizeInBytes)
        {
            if (sizeInBytes >= 1024 * 1024)
            {
                return (sizeInBytes / (1024.0 * 1024.0)).ToString("0.##") + " MB";
            }
            else
            {
                return (sizeInBytes / 1024.0).ToString("0.##") + " KB";
            }
        }
    }
}
