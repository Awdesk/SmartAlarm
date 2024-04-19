using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Shapes;
namespace Smart_Alarm
{
    public partial class MainPage : ContentPage
    {
        private bool isFirstRun = true;
        Parser parser;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            string[] settings;
            try
            {
                // Костыль с isFirstRun мне не нравится, но по другому не могу придумать.
                if (isFirstRun || DateTime.Now > File.GetLastAccessTime(App.settingsPath).AddMinutes(5))
                {
                    settings = File.ReadAllLines(App.settingsPath);
                    File.SetLastAccessTime(App.settingsPath, DateTime.Now);
                    isFirstRun = false;
                }
                else
                {
                    await DisplayAlert("Внимание", "Не так быстро, ковбой. Подождите минутку", "ОК");
                    return;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Ошибка", "Похоже, что файл с настройками отстутствует?", "ОК");
                Navigation.InsertPageBefore(new StartPage(), this);
                await Navigation.PopAsync();
                return;
            }
            activityIndicator1.IsRunning = true;
            await Task.Run(() =>
            {
                parser = new Parser(settings[0], settings[1]);
                parser.ParseTimetable();
            });
            activityIndicator1.IsRunning = false;

        }
    }
}
