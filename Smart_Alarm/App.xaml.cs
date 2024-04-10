using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm
{
    public partial class App : Application
    {
        const string localFileName = "Settings.txt";
        public App()
        {
            InitializeComponent();
            // Проверка на наличие сохраненных данных
            if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory,localFileName)))
            {
                MainPage = new NavigationPage(new StartPage());
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
