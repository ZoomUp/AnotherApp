using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnotherApp.ViewModels;
using System.Net.Http;
using System.Collections.Generic;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url = "https://reqres.in/api/login";
        public LoginPage()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Email, try again", "OK");
            InitializeComponent();

            LogEmail.Completed += (object sender, EventArgs e) =>
            {
                LogPassword.Focus();
            };

            LogPassword.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        private void RegistartionButton_Clicked(object sender, EventArgs e)
        {            
            Navigation.PushAsync(new RegistrationPage());
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"email", LogEmail.Text},{"password", LogPassword.Text }
            };

            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

            HttpResponseMessage response = await client.PostAsync(url, form);
            string responseCode = Convert.ToString(response.StatusCode);
            string result = await response.Content.ReadAsStringAsync();

            if (responseCode == "OK")
            {
                await Navigation.PushAsync(new SpecificUserProfile(LogEmail.Text));
            }
        }
    }
}