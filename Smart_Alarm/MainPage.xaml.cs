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
namespace Smart_Alarm
{
    public partial class MainPage : ContentPage
    {
        // Ошибка при парсинге(привести данные невозможно)
        Parser parser = new Parser(Preferences.Get("faculties", "fb"), Preferences.Get("groupID", "723-2"));
        public MainPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            // Возвращаем true, чтобы предотвратить переход на предыдущую страницу
            return true;
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine(parser.ParseTimetable());
        }
    }
}
