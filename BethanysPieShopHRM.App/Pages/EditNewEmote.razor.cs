using BethanysPieShopHRM.App.Services;
using ClassLibraryEmotes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EditNewEmote
    {
        public EditNewEmote() { }

        [Inject]
        public IEmoteDataService? EmoteDataService { get; set; }
        [Inject]
        public IUserDataService? UserDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string? EmoteId { get; set; }
        public Emote Emote { get; set; } = new Emote();
        public List<User>? Users { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Users = new List<User>();

            Users = (await UserDataService.GetAllUsers()).ToList();

            int.TryParse(EmoteId, out var emoteId);

            if (emoteId == 0)
            {
                Emote = new Emote { UserId = 1, CreationDate = DateTime.Now };
            }
            else 
            {
                Emote = await EmoteDataService.GetEmoteDetails(int.Parse(EmoteId));
            }
        }
        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (Emote.EmoteId == 0) //new emote
            {
                //image add 
                if (selectedFile != null)
                {
                    var file = selectedFile;
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    Emote.ImageName = file.Name;
                    Emote.ImageEmoteContent = ms.ToArray();
                }

                var addEmote = await EmoteDataService.AddEmote(Emote);
                if (addEmote != null)
                {
                    StatusClass = "alert-succes";
                    Message = "New emote added!!!";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something goes wrong!(espabila notas), try again!!!!";
                    Saved = false;
                }
            }
            else
            {
                Emote.Version++;
                await EmoteDataService.UpdateEmote(Emote);
                StatusClass = "alert-succes";
                Message = "Emote updated!!!";
                Saved = true;
            }
        }
        private IBrowserFile selectedFile;
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            StateHasChanged();
        }
        protected async Task HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors, Try again.";
        }

        protected async Task DeleteEmote()
        {
            await EmoteDataService.DeleteEmote(Emote.EmoteId);

            StatusClass = "alert-succes";
            Message = "Emote deleted!";
            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/emotes");
        }
    }
}