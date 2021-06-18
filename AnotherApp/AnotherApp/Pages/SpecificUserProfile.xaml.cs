using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using AnotherApp.Models;
using Newtonsoft.Json;
using AnotherApp.ViewModels;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecificUserProfile : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url1 = "https://reqres.in/api/users?page=1";
        private readonly string url2 = "https://reqres.in/api/users?page=2";
        ListUsers listUsers1;

        AppStyles appStyles = AppStyles.GetAppStyles();
        ProfileData profileData = ProfileData.GetProfileData();
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

            MessagingCenter.Subscribe<ChangeData, string>(this, "Updates", (sender, arg) =>
            {
                UpdatePage();
                DisplayAlert("Notification", arg, "OK");
            });

            MessagingCenter.Subscribe<AppSettings, string>(this, "Updates", (sender, arg) =>
            {
                UpdatePage();
            });
        }

        

        private void ChangeUserInfoButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new ChangeData());
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
                    profileData.UserAvatar = datum.Avatar;
                    profileData.UserFirstName = datum.First_Name;
                    profileData.UserLastName = datum.Last_Name;
                    profileData.UserEmail = datum.Email;

                    UpdatePage();
                }
            }
        }

        private void UpdatePage()
        {
            UserImage.Source = profileData.UserAvatar;
            UserNameLabel.Text = profileData.UserFirstName;
            UserLastNameLabel.Text = profileData.UserLastName;
            UserEmailLabel.Text = profileData.UserEmail;
            if (appStyles.selectedBackColor != null)
            {
                Resources["backColor"] = appStyles.setBrush();
            }            
        }
    }
}