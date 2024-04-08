using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smart_Alarm
{
    public partial class MainPage : ContentPage
    {
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
            await DisplayAlert("Спасибо", "Ваша заявка принята", "OK");
        }
    }
}
