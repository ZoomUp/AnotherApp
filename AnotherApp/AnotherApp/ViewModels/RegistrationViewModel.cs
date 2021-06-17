using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AnotherApp.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string regEmail;

        public string RegEmail
        {
            get { return regEmail; }
            set
            {
                regEmail = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RegEmail"));
            }
        }
        private string regPassword;
        public string RegPassword
        {
            get { return regPassword; }
            set
            {
                regPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RegPassword"));
            }
        }
        public ICommand SubmitRegCommand { protected set; get; }
        public RegistrationViewModel()
        {
            SubmitRegCommand = new Command(OnSubmit);
        }
        // В методе OnSubmit() можно прописать валидацию на входные данные
        public void OnSubmit()
        {
            if (regEmail == "" || regPassword == "")
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
