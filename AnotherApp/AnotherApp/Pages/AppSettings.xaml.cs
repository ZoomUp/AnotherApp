using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnotherApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string url1 = "https://reqres.in/api/unknown?page=1";
        private readonly string url2 = "https://reqres.in/api/unknown?page=2";

        AllColorsList allColorsList;
        Dictionary<string, string> colorDic = new Dictionary<string, string>();

        public AppSettings()
        {
            InitializeComponent();
            GetColorsList(url1);
            GetColorsList(url2);

            //var colorNames = new List<string>();
            //foreach (KeyValuePair<string, string> keyValue in colorDic)
            //{
            //    colorNames.Add(keyValue.Value);
            //}

            //ColorPicker.ItemsSource = colorNames;
        }

        private async void GetColorsList(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string listColorsJson = await response.Content.ReadAsStringAsync();
            allColorsList = JsonConvert.DeserializeObject<AllColorsList>(listColorsJson);

            foreach (DatumColor datumColor in allColorsList.Data)
            {
                colorDic.Add(datumColor.Name, datumColor.Color);
            }
        }
    }
}