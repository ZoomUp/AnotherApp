using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AnotherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url = "https://reqres.in/api/register";
        public RegistrationPage()
        {
            var vm = new RegistrationViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Email, try again", "OK");
            InitializeComponent();

            RegEmail.Completed += (object sender, EventArgs e) =>
            {
                RegPassword.Focus();
            };

            RegPassword.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitRegCommand.Execute(null);
            };
        }



        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"email", RegEmail.Text},{"password", RegPassword.Text }
            };

            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

            HttpResponseMessage response = await client.PostAsync(url, form);
            string responseCode = Convert.ToString(response.StatusCode);
            string result = await response.Content.ReadAsStringAsync();

            if (result == "OK")
            {
                DisplayAlert("Notification", "Registration successful", "ОK");
                await Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Notification", "Registration unsuccessful", "ОK");
            }
        }
    }
}