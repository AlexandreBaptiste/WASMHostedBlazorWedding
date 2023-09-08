using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public IAccountService? AccountService { get; set; }

        [Inject]
        public ILogger<Guests>? Logger { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public Blazored.SessionStorage.ISessionStorageService? SessionStorageService { get; set; }

        public Account? Account { get; set; } = new Account();

        public List<Account>? AccountList { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            if(SessionStorageService != null)
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

        public async void ValidFormSubmitted(EditContext editContext)
        {
            if(editContext.Validate())
            {
                if (Account != null && AccountList != null)
                {
                    if (AccountList.Any(a => a.Equals(Account)))
                    {                       
                        if (SessionStorageService != null)
                        {
                            await SessionStorageService.SetItemAsStringAsync("connected", "true");
                            await SessionStorageService.SetItemAsStringAsync("formtype", Account.Id);
                            NavigationManager?.NavigateTo("form");
                            
                        }                        
                    }
                }
            }
        }    
    }
}