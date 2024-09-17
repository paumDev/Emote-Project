using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class InboxPeopleOnline
    {
        private int CountOnline;

        [Inject]
        public AppState? AppState { get; set; }

        protected override void OnInitialized()
        {
            CountOnline = new Random().Next(100000);
            AppState.PeopleOnline = CountOnline;
        }
    }
}
