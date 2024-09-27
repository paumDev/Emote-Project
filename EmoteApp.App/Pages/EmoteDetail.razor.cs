using Microsoft.AspNetCore.Components;
using ClassLibraryEmotes;
using EmoteApp.App.Services;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.IO;

namespace EmoteApp.App.Pages
{
    public partial class EmoteDetail
    {
        [Inject]
        public IEmoteDataService? EmoteDataService { get; set; }

        [Inject]
        public IUserDataService? UserDataService { get; set; }
        [Inject] 
        public IFeedbackService? FeedbackService { get; set; }


        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public int UserId { get; set; }

        [Parameter]
        public string? EmoteId { get; set; }
        public List<Feedback> FeedbackList { get; set; } = new List<Feedback>();
        public Feedback NewFeedback { get; set; } = new Feedback();

        public Emote? Emote { get; set; } = new Emote();
        public User? User { get; set; } = new User();
        private int selectedEmoteId;
        private bool showModal;
        private bool isCommentModalVisible = false;

        private List<Feedback> Ordenar(List<Feedback> listaSinOrden)
        {
            listaSinOrden.OrderByDescending(f => f.Upvotes);
            return listaSinOrden;
        }
        protected override async Task OnInitializedAsync()
        {
            Emote = await EmoteDataService.GetEmoteDetails(int.Parse(EmoteId));
            UserId = Emote.UserId;
            User = await UserDataService.GetUserDetails(UserId);
            FeedbackList = (await FeedbackService.GetFeedback(int.Parse(EmoteId))).ToList();

            // URL de la imagen
            var imageUrl = Emote.ImageName;

            FeedbackList = (await FeedbackService.GetFeedback(int.Parse(EmoteId)))
                .ToList();

            FeedbackList = Ordenar(FeedbackList).ToList();

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
        private void OpenModal()
        {
            selectedEmoteId = Emote.EmoteId; // Ejemplo de un emote ID, aquí podrías pasar el ID del emote real
            showModal = true; // Esto abre el modal
        }
        private void OpenCommentModal()
        {
            isCommentModalVisible = true;
        }
        private void CloseCommentModal()
        {
            isCommentModalVisible = false;
        }
        private async Task HandleSubmitComment(string comment)
        {
            NewFeedback.Content = comment; // Asigna el comentario al feedback
            await SubmitFeedback(NewFeedback);
        }
        private void HandleModalClose(bool value)
        {
            showModal = value; // Esto cierra el modal
        }
        public async Task SubmitFeedback(Feedback newFeedback)
        {
            if (!string.IsNullOrEmpty(EmoteId))
            {
                newFeedback.EmoteId = int.Parse(EmoteId);  // Asigna el EmoteId
                await FeedbackService.AddFeedback(newFeedback);
                FeedbackList = (await FeedbackService.GetFeedback(int.Parse(EmoteId))).ToList();
                FeedbackList = Ordenar(FeedbackList).ToList();
                NewFeedback = new Feedback();  // Limpia el formulario
            }
            else
            {
                Console.WriteLine("EmoteId is null or empty");
            }
        }

        public async Task Vote(int feedbackId, bool isUpvote)
        {
            await FeedbackService.Vote(feedbackId, isUpvote);
            FeedbackList = (await FeedbackService.GetFeedback(int.Parse(EmoteId))).ToList();
        }
        public async Task DeleteFeedback(int feedbackId)
        {
            await FeedbackService.DeleteFeedback(feedbackId);
            FeedbackList = (await FeedbackService.GetFeedback(int.Parse(EmoteId))).ToList();
        }
    }
}
