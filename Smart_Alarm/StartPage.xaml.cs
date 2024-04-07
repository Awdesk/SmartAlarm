using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        /// <summary>
        ///  Здесь будет ввод данных пользователем.
        /// </summary
        public StartPage()
        {
            InitializeComponent();
        }
        private async void ButtonCommit_Click(object sender, EventArgs e)
        {
            int timeGK_Value = 0, timeULK_Value = 0, timeFAT_RK_Value = 0;
            // Проверка на корректность введенных данных P.S. Добавьте больше проверок
            if(int.TryParse(timeGK.Text, out timeGK_Value) && int.TryParse(timeULK.Text, out timeULK_Value) 
                && int.TryParse(timeFAT_RK.Text, out timeFAT_RK_Value))
            {
                await Navigation.PushModalAsync(new MainPage());
            }
            else
                await DisplayAlert("Ошибка", "Проверьте введенные данные", "OK");

        }
    }
}