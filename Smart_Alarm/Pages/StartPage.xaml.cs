
using Newtonsoft.Json;
using Smart_Alarm.FilesJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        Dictionary<string, string> faculties = new Dictionary<string, string>{
            {"Аспирантура", "aspirantura"},
            {"Радиотехнический факультет", "rtf"},
            {"Факультет вычислительных систем", "fvs"},
            {"Факультет систем управления", "fsu"},
            {"Радиоконструкторский факультет", "rkf"},
            {"Факультет инновационных технологий", "fit"},
            {"Экономический факульт", "ef"},
            {"Гуманитарный факультет", "gf"},
            {"Юридический факультет", "yuf"},
            {"Факультет безопасности", "fb"},
            {"Заочный и вечерний факультет", "zivf"},
            {"Факультет электронной техники", "fet" }
        };
        public StartPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            pickerFaculties.SelectedIndex = 10;
        }
        private async void ButtonCommit_Click(object sender, EventArgs e)
        {
            // Проверка на корректность введенных данных P.S. Добавьте больше проверок
            if (IsNumericString(timeGK.Text) && IsNumericString(timeULK.Text) 
                && IsNumericString(timeFAT_RK.Text) && IsNumericString(groupID.Text))
            {
                SettingsJSON data = new SettingsJSON
                {
                    GroupID = groupID.Text,
                    Faculty = faculties[pickerFaculties.Items[pickerFaculties.SelectedIndex]],
                    TimeULK = timeULK.Text,
                    TimeGK = timeGK.Text,
                    TimeFAT_RK = timeFAT_RK.Text
                };

                // Преобразование данных в JSON
                string json = JsonConvert.SerializeObject(data);

                // Запись JSON в файл
                File.WriteAllText(App.SETTINGS_PATH, json);
                if (Application.Current.MainPage is NavigationPage)
                {
                    Application.Current.MainPage = new FlyoutPage1();
                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Успешно", "Настройки обновлены", "OK");
            }
            else
                await DisplayAlert("Ошибка", "Проверьте введенные данные", "OK");
        }
        private bool IsNumericString(string str)
        {
            if (str == null)
                return false;
            return str.All(c => char.IsDigit(c) || c == '-');
        }
    }
}