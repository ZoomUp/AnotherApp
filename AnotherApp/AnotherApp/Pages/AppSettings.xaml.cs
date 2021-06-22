using System;
using System.Collections.Generic;

using System.Net.Http;

using AnotherApp.Models;
using AnotherApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url1 = "https://reqres.in/api/unknown";
        

        AllColorsList allColorsList = new AllColorsList();
        List<string> colorNames = new List<string>();
        List<string> colorValue = new List<string>();
        Dictionary<string, string> keyValuePairs;
        AppStyles appStyles = AppStyles.GetAppStyles();

        public AppSettings()
        {
            InitializeComponent();
            UpdatePage();
            keyValuePairs = new Dictionary<string, string>();
            GetColorsList(url1);
            
        }

        private async void GetColorsList(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string listColorsJson = await response.Content.ReadAsStringAsync();
            allColorsList = JsonConvert.DeserializeObject<AllColorsList>(listColorsJson);

            foreach (DatumColor datumColor in allColorsList.Data)
            {
                keyValuePairs.Add(datumColor.Name, datumColor.Color);
            }

            foreach (KeyValuePair<string, string> keyValue in keyValuePairs)
            {
                colorNames.Add(keyValue.Key);
                colorValue.Add(keyValue.Value);
            }
            appStyles.ColorDic = keyValuePairs;
            ColorPicker.ItemsSource = colorNames;
        }

        private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColorPicker.SelectedIndex != -1)
            {
                appStyles.SelectedBackColor = (string)ColorPicker.SelectedItem;
                VisualColor.Background = appStyles.setBrush();
            }
        }

        private void ChangeAppColor_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<AppSettings, string>(this, "Updates", "Styles update");
            Resources["backColor"] = appStyles.setBrush();
            OnDisplayAlertQuestionButtonClicked(sender, e);
        }

        private void UpdatePage()
        {            
            if (appStyles.selectedBackColor != null)
            {
                Resources["backColor"] = appStyles.setBrush();
            }
        }

        async void OnDisplayAlertQuestionButtonClicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Preferences", "Set this color as default?", "Yes", "No");
            if (response)
            {
                Preferences.Set("backColor", appStyles.ColorDic[appStyles.SelectedBackColor]);
            }
        }
    }
}