using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Проверка на наличие сохраненных данных
            if (Preferences.ContainsKey("groupID") && Preferences.ContainsKey("faculties"))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else MainPage = new MainPage();
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
