using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutDetailAlarm : ContentPage
    {
        private bool isFirstRun = true;
        Parser parser;
        public FlyoutDetailAlarm()
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