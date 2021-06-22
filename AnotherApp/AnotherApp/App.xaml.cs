using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using AnotherApp.ViewModels;
using System.Collections.Generic;

namespace AnotherApp
{
    public partial class App : Application
    {
        AppStyles appStyles = AppStyles.GetAppStyles();
        public App()
        {
            InitializeComponent();
            if (Preferences.ContainsKey("backColor"))
            {
                Dictionary<string, string> tmp = new Dictionary<string, string>();
                string backColor = Preferences.Get("backColor", "#FFFFFF");
                tmp.Add("backColor", backColor);
                appStyles.ColorDic = tmp;
                
                appStyles.SelectedBackColor = "backColor";
            }      
            MainPage = new NavigationPage(new Pages.LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
