using System;
using AnotherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
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
    }
}