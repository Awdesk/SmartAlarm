using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public FlyoutDetailAlarm()
        {
            InitializeComponent();
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            string[] settings;
            try
            {
                if (!File.Exists(App.savedTimetablePath) || DateTime.Now > File.GetLastAccessTime(App.settingsPath).AddMinutes(1))
                {
                    settings = File.ReadAllLines(App.settingsPath);
                    File.SetLastAccessTime(App.settingsPath, DateTime.Now);
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
                Parser parser = new Parser(settings[0], settings[1]);
                List<Lesson> lessons = parser.ParseTimetable();
                string[] lines = lessons.Select(lesson => $"{lesson.discipline};{lesson.auditoriums};{lesson.dateTime:dd.MM.yyyy HH:mm}").ToArray();
                File.WriteAllText(App.savedTimetablePath, string.Join(Environment.NewLine, lines));
            }).ConfigureAwait(false);
            activityIndicator1.IsRunning = false;
        }
    }
}