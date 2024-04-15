using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm
{
    public partial class App : Application
    {   
        //Хранить здесь все константные значения
        public static string localPath = FileSystem.AppDataDirectory;
        public static string settingsPath = Path.Combine(localPath, "localSettings.txt");
        public static string htmlPath = Path.Combine(App.localPath, $"tusurSite.html");
        public App()
        {
            InitializeComponent();
            // Проверка на наличие сохраненных данных
            if (!File.Exists(settingsPath))
            {
                MainPage = new NavigationPage(new StartPage());
            } 
            else
                MainPage = new NavigationPage(new MainPage());
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
