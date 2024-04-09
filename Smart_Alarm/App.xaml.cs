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
            if(Preferences.Get("groupID", "") == "")
            MainPage = new NavigationPage(new StartPage());
            // Багулина! Исправить. Если данные сохранены, то приложение не запускается
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
