using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Form : ComponentBase
    {
        [Inject]
        public IguestsService? guestsService { get; set; }

        [Inject]
        public ILogger<Guests>? Logger { get; set; }

        public List<Guest> guestList { get; set; } = new List<Guest>();

        public Guest? guest { get; set; }

        public string LastSubmitResult = string.Empty;

        public EditContext? EditContext { get; set; }  

        protected override async Task OnInitializedAsync()
        {
            if (guestsService != null)
            {
                var guests = await guestsService.Getguests();
                guestList = guests != null && guests.Any() ? guests.ToList() : new List<Guest>();
            }

            if(guestList != null)
            {
                guest = guestList.FirstOrDefault();
                EditContext = new EditContext(guestList);
            }
        }

        public void ValidFormSubmitted(EditContext editContext)
        {
            LastSubmitResult = editContext.Validate() ? "Success - form was valid" : "Failure - form was invalid";
        }
    
    }
}