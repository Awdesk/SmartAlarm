using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using HtmlAgilityPack;
using System.IO;
namespace Smart_Alarm
{
    public partial class MainPage : ContentPage
    {
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
                settings = File.ReadAllLines(App.settingsPath);
            }
            catch (Exception)
            {
                await DisplayAlert("Ошибка", "Похоже файл с настройками отстутствует?", "ОК");
                Navigation.InsertPageBefore(new StartPage(), this);
                await Navigation.PopAsync();
                return;
            }
            //Сделайте таймер перед повторным использованием парсера, чтобы не досить сайт, иначе по IP вычислят
            //Предполагается сохранять структуру на телефон, если структура есть и файл со структурой создан меньше 30 минут, то блокировать попытку парсинга
            parser = new Parser(settings[0], settings[1]);
            parser.ParseTimetable();
        }
    }
}
