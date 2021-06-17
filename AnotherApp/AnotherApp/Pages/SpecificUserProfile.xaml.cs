using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnotherApp.Pages;
using System.Net.Http;
using AnotherApp.Models;
using Newtonsoft.Json;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecificUserProfile : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url1 = "https://reqres.in/api/users?page=1";
        private readonly string url2 = "https://reqres.in/api/users?page=2";
        ListUsers listUsers1;
        public SpecificUserProfile()
        {
            InitializeComponent();
        }

        public SpecificUserProfile(string email)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            GetUsersLists(url1, email);
            GetUsersLists(url2, email);
        }

        private async void ChangeUserInfoButton_Clicked(object sender, System.EventArgs e)
        {
            
        }
        private void AppSettingsButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AppSettings());
        }

        private async void GetUsersLists(string url, string email)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string listUsersJson = await response.Content.ReadAsStringAsync();
            listUsers1 = JsonConvert.DeserializeObject<ListUsers>(listUsersJson);

            foreach (Datum datum in listUsers1.Data)
            {
                if (datum.Email == email)
                {
                    UserNameLabel.Text += datum.First_Name;
                    UserLastNameLabel.Text += datum.Last_Name;
                    UserImage.Source = datum.Avatar;
                    UserEmailLabel.Text += datum.Email;
                }
            }
        }

        
    }
}