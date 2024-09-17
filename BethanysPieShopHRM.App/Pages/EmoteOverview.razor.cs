using ClassLibraryEmotes;
using BethanysPieShopHRM.App.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BethanysPieShopHRM.App.Services;
using Microsoft.AspNetCore.Components.Web;


namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmoteOverview
    {
        public List<Emote>? Emotes { get; set; } = default!;
        private Emote? _selectedEmote;
        private Emote? _selectedEmoteTrend;

        private string selectedSort { get; set; } = "trending"; // Valor por defecto
        private string searchQuery { get; set; } = ""; // Para la barra de búsqueda
        private List<Emote> filteredCards { get; set; } = new List<Emote>();


        private string Title = "Emote View";
        [Inject]
        public IEmoteDataService? EmoteDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var allEmotes = (await EmoteDataService.GetAllEmotes()).ToList();

            // Calculas cuáles emotes están en tendencia
            var emotesEnTendencia = EmoteDataService.ObtenerTendencias(allEmotes);

            // Usa un HashSet para comprobar la tendencia
            var emotesEnTendenciaSet = new HashSet<Emote>(emotesEnTendencia);

            // Marca los emotes como en tendencia y luego ordénalos
            Emotes = allEmotes
                .Select(e =>
                {
                    e.Trending = emotesEnTendenciaSet.Any(t => t.Name == e.Name);
                    return e;
                })
                .OrderByDescending(e => e.Trending) // Primero los emotes en tendencia
                .ThenByDescending(e => e.NumDownload) // Luego ordena los emotes restantes por descargas
                .ToList();

            FilteredCards = Emotes;
        }
        public void ShowEmotePopUp(Emote selectedEmote)
        {
            _selectedEmote = selectedEmote;
            _selectedEmoteTrend = null;
        }

        public void ShowEmotePopUpTrend(Emote selectedEmote)
        {
            _selectedEmoteTrend = selectedEmote;
            _selectedEmote = null;
        }

        private string SelectedSort
        {
            get => selectedSort;
            set
            {
                if (selectedSort != value)
                {
                    selectedSort = value;
                    ApplyFilters();
                }
            }
        }

        // Propiedad para SearchQuery con lógica en el setter
        private string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    ApplyFilters();
                }
            }
        }
        private void ApplyFilters()
        {
            // Filtrar por el término de búsqueda
            IEnumerable<Emote> filtered = Emotes;

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filtered = filtered.Where(card =>
                    card.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            // Aplicar el orden seleccionado
            switch (SelectedSort)
            {
                case "alphabetical":
                    filtered = filtered.OrderBy(card => card.Name);
                    break;
                case "creation-date":
                    filtered = filtered.OrderByDescending(card => card.CreationDate);
                    break;
                case "trending":
                    filtered = filtered.OrderByDescending(card => card.Trending);
                    break;
                default:
                    // No ordenar, mostrar todos
                    break;
            }

            // Actualizar la lista filtrada
            FilteredCards = filtered.ToList();
        }
        public void HandleKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                Search(); // Ejecuta el filtrado si es necesario
            }
        }
        private void Search()
        {
            ApplyFilters();
        }
        public List<Emote> FilteredCards
        {
            get => filteredCards;
            set
            {
                filteredCards = value;
                StateHasChanged();
            }
        }
    }
}
