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
        public System.Windows.Input.ICommand ButtonClickedCommand { private set; get; }
        public FlyoutDetailAlarm()
        {
            InitializeComponent();
            if (File.Exists(App.SAVED_TIMETABLE_PATH))
            {
                string json = File.ReadAllText(App.SAVED_TIMETABLE_PATH);
                List<LessonJSON> lessons = JsonConvert.DeserializeObject<List<LessonJSON>>(json);
                lstView.ItemsSource = GetAlarmData(lessons);
                button.Text = "Обновить расписание";
            }
            BindingContext = this;
        }
        public ObservableCollection<Alarm.Alarm> GetAlarmData(List<LessonJSON> lessons)
        {
            ObservableCollection<Alarm.Alarm> alarms = new ObservableCollection<Alarm.Alarm>();
            foreach (var item in lessons)
            {
                alarms.Add(new Alarm.Alarm { DateTime = item.DateTime, Name = item.Time, Description = $"{item.Date} " + '\n' + $"{item.Discipline} {item.Auditorums}" });
            }
            return alarms;
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            string isSecondButton = (string)((Button)sender).CommandParameter;
            SettingsJSON settings;
            try
            {
                if (!File.Exists(App.SAVED_TIMETABLE_PATH) || DateTime.Now > File.GetLastAccessTime(App.SETTINGS_PATH).AddMinutes(1))
                {
                    string json = File.ReadAllText(App.SETTINGS_PATH);
                    settings = JsonConvert.DeserializeObject<SettingsJSON>(json);
                    File.SetLastAccessTime(App.SETTINGS_PATH, DateTime.Now);
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
                Parser parser;
                if (isSecondButton == "special_flag")
                {
                    parser = new Parser(settings, flag: true);
                }
                else
                {
                    parser = new Parser(settings);
                }
                lessons = parser.ParseTimetable();
                string json = JsonConvert.SerializeObject(lessons);
                File.WriteAllText(App.SAVED_TIMETABLE_PATH, json);
            });
            activityIndicator1.IsRunning = false;
            lstView.ItemsSource = GetAlarmData(lessons);
            button.Text = "Обновить расписание";
        }
    }
}