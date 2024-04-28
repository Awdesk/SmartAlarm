using Newtonsoft.Json;
using Smart_Alarm.FilesJSON;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutDetailAlarm : ContentPage
    {
        public ObservableCollection<Alarm.Alarm> time_of_alarm { get; set; }
        List<LessonJSON> lessons;
        public FlyoutDetailAlarm()
        {
            InitializeComponent();
            if (File.Exists(App.savedTimetablePath))
            {
                string json = File.ReadAllText(App.savedTimetablePath);
                List<LessonJSON> lessons = JsonConvert.DeserializeObject<List<LessonJSON>>(json);
                lstView.ItemsSource = GetAlarmData(lessons);
                button.Text = "Обновить расписание";
            }
        }
        public ObservableCollection<Alarm.Alarm> GetAlarmData(List<LessonJSON> lessons)
        {
            ObservableCollection<Alarm.Alarm> alarms = new ObservableCollection<Alarm.Alarm>();
            foreach (var item in lessons)
            {
                alarms.Add(new Alarm.Alarm { DateTime = item.DateTime, Name = item.Time, Description = $"{item.Date} " + '\n' + $"{item.Discipline} {item.Auditorums} " });
            }
            return alarms;
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            SettingsJSON settings;
            try
            {
                if (!File.Exists(App.savedTimetablePath) || DateTime.Now > File.GetLastAccessTime(App.settingsPath).AddMinutes(1))
                {
                    string json = File.ReadAllText(App.settingsPath);
                    settings = JsonConvert.DeserializeObject<SettingsJSON>(json);
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
                Parser parser = new Parser(settings.GroupID, settings.Faculty);
                lessons = parser.ParseTimetable();
                Debug.WriteLine(lessons);
                string json = JsonConvert.SerializeObject(lessons);
                File.WriteAllText(App.savedTimetablePath, json);
            });
            activityIndicator1.IsRunning = false;
            lstView.ItemsSource = GetAlarmData(lessons);
        }
    }
}