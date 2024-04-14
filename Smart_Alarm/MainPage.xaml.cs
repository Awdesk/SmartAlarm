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
        public string[] settings;
        Parser parser;
        public MainPage()
        {
            InitializeComponent();
            try
            {
                settings = File.ReadAllLines(App.settingsPath);
            }
            catch(Exception ex){
                //Если произошла ошибка, сделайте возможность перейти на страницу с настройками, 
                //чтобы сохранить файл повторно, хотя возникновение такой ошибки маловероятно.
                DisplayAlert("Ошибка", $"Ошибка чтения файла настроек: {ex.Message}", "ok");
            }
        }
        protected override bool OnBackButtonPressed()
        {
            // Возвращаем true, чтобы предотвратить переход на предыдущую страницу
            return true;
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            //Сделайте таймер перед повторным использованием парсера, чтобы не досить сайт, иначе по IP вычислят
            //Предполагается сохранять структуру на телефон, если структура есть и файл со структурой создан меньше 30 минут, то блокировать попытку парсинга
            parser = new Parser(settings[0], settings[1]);
            parser.ParseTimetable();
        }
    }
}
