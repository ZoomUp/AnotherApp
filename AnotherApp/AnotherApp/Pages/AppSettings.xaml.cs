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
        private readonly string url1 = "https://reqres.in/api/unknown";
        

        AllColorsList allColorsList;
        Dictionary<string, string> colorDic;
        List<string> colorNames;
        List<string> colorValue;

        public AppSettings()
        {
            InitializeComponent();
            colorDic = new Dictionary<string, string>();
            colorNames = new List<string>();
            colorValue = new List<string>();
            GetColorsList(url1);

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

            foreach (KeyValuePair<string, string> keyValue in colorDic)
            {
                colorNames.Add(keyValue.Key);
                colorValue.Add(keyValue.Value);
            }

            ColorPicker.ItemsSource = colorNames;
        }

        private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColorPicker.SelectedIndex != -1)
            {
                Color color = Color.FromHex(colorDic[Convert.ToString(ColorPicker.SelectedItem)]);
                Brush brush = new SolidColorBrush(color);

                VisualColor.Background = brush;
            }
        }
    }
}