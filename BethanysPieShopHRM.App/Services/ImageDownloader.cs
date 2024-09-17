using Microsoft.JSInterop;

namespace BethanysPieShopHRM.App.Services
{
    public class ImageDownloader
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ImageDownloader(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task DownloadImageAsync(string imageUrl)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute) || !imageUrl.StartsWith("https://localhost"))
            {
                throw new ArgumentException("La URL no es válida o no es de un dominio permitido.");
            }

            if (!imageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                !imageUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                !imageUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("El archivo debe ser una imagen PNG o JPG.");
            }

            try
            {
                var response = await _httpClient.GetAsync(imageUrl);
                response.EnsureSuccessStatusCode();

                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                await _jsRuntime.InvokeVoidAsync("downloadImage", Convert.ToBase64String(imageBytes), Path.GetFileName(imageUrl));
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Error descargando la imagen: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
