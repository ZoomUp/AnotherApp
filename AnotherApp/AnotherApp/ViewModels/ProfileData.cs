using System.ComponentModel;

namespace AnotherApp.ViewModels
{
    public class ProfileData : INotifyPropertyChanged
    {
        public string userFirstName;
        public string userLastName;
        public string userEmail;
        public string userAvatar;

        private static ProfileData profileData;

        private ProfileData()
        {
            
        }

        public static ProfileData GetProfileData()
        {
            if(profileData is null)
            {
                profileData = new ProfileData();
            }
            return profileData;
        }



        public string UserFirstName
        {
            get { return userFirstName; }
            set
            {
                if(userFirstName != value)
                {
                    userFirstName = value;
                    OnPropertyChanged("UserFirstName");
                }
            }            
        }

        public string UserLastName
        {
            get { return userLastName; }
            set
            {
                if (userLastName != value)
                {
                    userLastName = value;
                    OnPropertyChanged("UserLastName");
                }
            }
        }

        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                if (userEmail != value)
                {
                    userEmail = value;
                    OnPropertyChanged("UserEmail");
                }
            }
        }

        public string UserAvatar
        {
            get { return userAvatar; }
            set
            {
                if (userAvatar != value)
                {
                    userAvatar = value;
                    OnPropertyChanged("UserAvatar");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}