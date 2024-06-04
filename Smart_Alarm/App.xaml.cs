using Smart_Alarm.Pages;
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
        public static string LOCAL_PATH = FileSystem.AppDataDirectory;
        public static string SETTINGS_PATH = Path.Combine(LOCAL_PATH, "localSettings.json");
        public static string SAVED_TIMETABLE_PATH = Path.Combine(LOCAL_PATH, "timetable.json");
        public App()
        {
            InitializeComponent();
            // Проверка на наличие сохраненных данных
            if (!File.Exists(SETTINGS_PATH))
            {
                MainPage = new NavigationPage(new StartPage());
            }
            else
            MainPage = new FlyoutPage1();
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
