using Microsoft.AspNetCore.Components;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Guests : ComponentBase
    {
        #region Inject
        [Inject]
        public IGuestService? GuestsService { get; set; }

        [Inject]
        public ILogger<Guests>? Logger { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// List of guests loaded from API
        /// </summary>
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();

        /// <summary>
        /// List of guests display on screen
        /// </summary>
        public IEnumerable<Guest> SelectedGuestList { get; set; } = new List<Guest>();

        /// <summary>
        /// Dictionnary of guests used for pagination
        /// Key: page index
        /// Value: list of guests 
        /// </summary>
        public IDictionary<int,List<Guest>> PaginatedGuests = new Dictionary<int, List<Guest>>();

        /// <summary>
        /// Previous tab enabled/disabled
        /// </summary>
        private string? DisablePrevious
        {
            get { return SelectedIndex <= 0 ? "disabled" : null; }
            set { }
        }

        /// <summary>
        /// Next tab enabled/disabled
        /// </summary>
        private string? DisableNext
        {
            get { return SelectedIndex >= PaginatedGuests.Count - 1 ? "disabled" : null; }
            set { }
        }

        /// <summary>
        /// Current page 
        /// </summary>
        private int SelectedIndex { get; set; }

        private bool LastnameOrdered { get; set; }
        private bool FirstnameOrdered { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (GuestsService != null)
                {
                    var guests = await GuestsService.Getguests();
                    GuestList = guests != null && guests.Any() ? guests : new List<Guest>();

                    // In a larger projet I would do that server side, passing page number/size to API
                    // and loading desired chunks from DB
                    if (GuestList.Any())
                    {
                        PaginatedGuests = Partition(GuestList.ToList(), 10);
                        SelectedGuestList = PaginatedGuests[0];
                        SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Logger != null && Logger.IsEnabled(LogLevel.Debug))
                {
                    Logger.LogTrace(string.Format("Erreur lors de OnInitializedAsync : {0}", ex.Message));
                }   
            }            
        }

        /// <summary>
        /// Select badge class
        /// </summary>
        /// <param name="pStatut">Guest.Status</param>
        /// <returns></returns>
        private static string Badge(string? pStatut)
        {
            return pStatut switch
            {
                "Accepté" => "badge-success",
                "En attente" => "badge-primary",
                "Refusé" => "badge-danger",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Event for next page
        /// </summary>
        /// <param name="page">next page to display</param>
        private void OnSelectedPage(int page)
        {
            if(page >= 0 && page <= PaginatedGuests.Count)
            {
                SelectedGuestList = PaginatedGuests[page];
                SelectedIndex = page;
            }                                  
        }

        /// <summary>
        /// Order by lastname current page
        /// </summary>
        private void OrderLastname()
        {
            if (LastnameOrdered)
            {
                SelectedGuestList = SelectedGuestList.OrderBy(x => x.Name);
            }
            else
            {
                SelectedGuestList = SelectedGuestList.OrderByDescending(x => x.Name);
            }
            LastnameOrdered = !LastnameOrdered;
        }

        /// <summary>
        /// Order by firstname current page
        /// </summary>
        private void OrderFirstname()
        {
            if (FirstnameOrdered)
            {
                SelectedGuestList = SelectedGuestList.OrderBy(x => x.Firstname);
            }
            else
            {
                SelectedGuestList = SelectedGuestList.OrderByDescending(x => x.Firstname);
            }
            FirstnameOrdered = !FirstnameOrdered;
        }

        /// <summary>
        /// Split a list by a given param:chunkSize
        /// </summary>
        /// <param name="values">initial guests list</param>
        /// <param name="chunkSize">size of desired chunk</param>
        /// <returns>Dictionnary of page/list</returns>
        private static IDictionary<int, List<Guest>> Partition(List<Guest> values, int chunkSize)
        {
            var partitions = new Dictionary<int, List<Guest>>();
            int index = 0;
            for (int i = 0; i < values.Count; i += chunkSize)
            {
                partitions.Add(index, values.GetRange(i, Math.Min(chunkSize, values.Count - i)));
                index++;
            }
            return partitions;
        }
    }
}