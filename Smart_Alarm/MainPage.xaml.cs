using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using HtmlAgilityPack;
using System.Diagnostics;
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
                DisplayAlert("Ошибка", ex.Message, "ok");
            }
        }
        protected override bool OnBackButtonPressed()
        {
            // Возвращаем true, чтобы предотвратить переход на предыдущую страницу
            return true;
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            parser = new Parser(settings[0], settings[1]);
            parser.ParseTimetable();
        }
    }
}
