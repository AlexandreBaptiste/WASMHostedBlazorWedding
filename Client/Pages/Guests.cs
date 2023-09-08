using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Guests : ComponentBase
    {
        [Inject]
        public IGuestService? guestsService { get; set; }

        [Inject]
        public ILogger<Guests>? Logger { get; set; }

        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();

        protected override async Task OnInitializedAsync()
        {
            if (guestsService != null)
            {
                var guests = await guestsService.Getguests();
                GuestList = guests != null && guests.Any() ? guests : new List<Guest>();
            }
        }
    }
}