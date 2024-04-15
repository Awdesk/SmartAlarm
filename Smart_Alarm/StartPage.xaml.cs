using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm
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
                // Записываем данные в файл, если его нет, то он создаётся
                File.WriteAllText(App.settingsPath, $"{groupID.Text}\n{faculties[pickerFaculties.Items[pickerFaculties.SelectedIndex]]}\n{timeULK.Text}\n{timeGK.Text}\n{timeFAT_RK.Text}");
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Ошибка", "Проверьте введенные данные", "OK");
        }
        private bool IsNumericString(string str)
        {
            return str.All(c => char.IsDigit(c) || c == '-');
        }
    }
}