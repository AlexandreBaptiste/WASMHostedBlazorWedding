using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Login : ComponentBase
    {
        #region Inject
        [Inject]
        public IAccountService? AccountService { get; set; }

        [Inject]
        public ILogger<Guests>? Logger { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public Blazored.SessionStorage.ISessionStorageService? SessionStorageService { get; set; }
        #endregion

        #region Properties
        public Account? Account { get; set; } = new Account();

        public List<Account>? AccountList { get; set; }

        // Show or hide error box
        private string? Hide = "hidden";
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (SessionStorageService != null)
                {
                    string? isConnected = await SessionStorageService.GetItemAsync<string>("connected");
                    if (isConnected != null)
                    {
                        if (bool.Parse(isConnected))
                        {
                            string formType = await SessionStorageService.GetItemAsync<string>("formtype");
                            if (!string.IsNullOrEmpty(formType))
                            {
                                NavigationManager?.NavigateTo("form");
                            }
                            else
                            {
                                await SessionStorageService.ClearAsync();
                            }
                        }
                    }
                }

                if (AccountService != null)
                {
                    var accounts = await AccountService.GetAccounts();
                    AccountList = accounts != null && accounts.Any() ? accounts.ToList() : new List<Account>();
                }
            }            
            catch(Exception ex)
            {
                if (Logger != null && Logger.IsEnabled(LogLevel.Debug))
                {
                    Logger.LogTrace(string.Format("Erreur lors de OnInitializedAsync : {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Form validation 
        /// </summary>
        /// <param name="editContext"></param>
        public async void ValidFormSubmitted(EditContext editContext)
        {
            try
            {
                if (editContext.Validate())
                {
                    if (Account != null && AccountList != null)
                    {
                        if (AccountList.Any(a => a.Equals(Account)))
                        {
                            Hide = "hidden";
                            if (SessionStorageService != null)
                            {
                                await SessionStorageService.SetItemAsStringAsync("connected", "true");
                                await SessionStorageService.SetItemAsStringAsync("formtype", Account.Id);
                                NavigationManager?.NavigateTo("form");
                            }
                        }
                        else
                        {
                            Hide = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Logger != null && Logger.IsEnabled(LogLevel.Debug))
                {
                    Logger.LogTrace(string.Format("Erreur lors de ValidFormSubmitted : {0}", ex.Message));
                }
            }
        }    
    }
}