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
        // Если констант будет много, нужно создать отдельный файл с константами, чтобы избежать дублирование кода
        const string localFileName = "Settings.txt";
        readonly string localPath;
        public StartPage()
        {
            InitializeComponent();
            localPath = Path.Combine(FileSystem.AppDataDirectory, localFileName);
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
                File.WriteAllText(localPath, $"{groupID.Text}\n{pickerFaculties.Items[pickerFaculties.SelectedIndex]}\n{timeULK.Text}\n{timeGK.Text}\n{timeFAT_RK.Text}");
                await Navigation.PushAsync(new MainPage());
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