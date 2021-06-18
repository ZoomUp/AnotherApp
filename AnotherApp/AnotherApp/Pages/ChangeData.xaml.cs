using System;
using AnotherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeData : ContentPage
    {
        ProfileData profileData = ProfileData.GetProfileData();
        AppStyles appStyles = AppStyles.GetAppStyles();
        public ChangeData()
        {
            InitializeComponent();
            UpdatePage();
        }
        
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            profileData.UserFirstName = FirstNameChange.Text;
            profileData.UserLastName = LastNameChange.Text;
            profileData.UserEmail = EmailChange.Text;
            MessagingCenter.Send<ChangeData, string>(this, "Updates", "User data changed");
            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void UpdatePage()
        {
            FirstNameChange.Text = profileData.UserFirstName;
            LastNameChange.Text = profileData.UserLastName;
            EmailChange.Text = profileData.UserEmail;
            UserImage.Source = profileData.UserAvatar;

            if (appStyles.selectedBackColor != null)
            {
                Resources["backColor"] = appStyles.setBrush();
            }
        }
    }
}