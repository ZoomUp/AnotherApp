using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace AnotherApp.ViewModels
{
    public class AppStyles
    {
        private Dictionary<string, string> colorDic;
        private string selectedBackColor;
        private static AppStyles appStyles;
        private AppStyles()
        {
            
        }

        public static AppStyles GetAppStyles()
        {
            if (appStyles is null)
            {
                appStyles = new AppStyles();
            }
            return appStyles;
        }
        
        public Dictionary<string, string> ColorDic
        {
            get { return colorDic; }
            set
            {
                colorDic = value;
            }
        }

        public string SelectedBackColor
        {
            get { return selectedBackColor; }
            set
            {
                if (selectedBackColor != value)
                {
                    selectedBackColor = value;
                    OnPropertyChanged("SelectedBackColor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Brush setBrush()
        {
            Color color = Color.FromHex(appStyles.ColorDic[selectedBackColor]);
            Brush brush = new SolidColorBrush(color);

            return brush;
        }
    }
}
