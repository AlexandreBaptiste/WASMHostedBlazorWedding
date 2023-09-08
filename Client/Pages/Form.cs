using Microsoft.AspNetCore.Components;

namespace Wedding.Client.Pages
{
    public partial class Form : ComponentBase
    {
        [Inject]
        public ILogger<Guests>? Logger { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public Blazored.SessionStorage.ISessionStorageService? SessionStorageService { get; set; }

        internal const string URL_FORM_HONNEUR = "https://form.jotform.com/232484125864359";
        internal const string URL_FORM_LOVER   = "https://docs.google.com/forms/d/e/1FAIpQLSedqYG1DoTea-1NFZgt506gceOK8pI2yGifyuEj7r7OoGdlAA/viewform?embedded=true";

        public string URL { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
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
                            switch (formType)
                            {
                                case "Honneur":
                                    URL = URL_FORM_HONNEUR;
                                    break;
                                case "Lover":
                                    URL = URL_FORM_LOVER;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            NavigationManager?.NavigateTo("login");
                        }
                    }
                    else
                    {
                        NavigationManager?.NavigateTo("login");
                    }
                }
                else
                {
                    NavigationManager?.NavigateTo("login");
                }
            }         
        }
    }
}