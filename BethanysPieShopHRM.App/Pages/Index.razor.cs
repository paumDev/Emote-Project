using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class Index
    {
        [Inject]
        public AppState? AppState { get; set; }
        private int NumOnline;

        protected override void OnInitialized()
        {
            NumOnline = AppState.PeopleOnline;
        }
    }
}
