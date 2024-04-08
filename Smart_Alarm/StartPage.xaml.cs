using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        string selectedPickerItem = "";
        int timeGK_Value = 0, timeULK_Value = 0, timeFAT_RK_Value = 0, groupID_Value = 0;
        /// <summary>
        ///  Здесь будет ввод данных пользователем.
        /// </summary
        public StartPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            pickerFaculties.SelectedIndex = 10;
//#if DEBUG
//            Label debugLabel = new Label
//            {
//                Text = selectedPickerItem
//            };
//            Content = debugLabel;
//#endif
        }
        private async void ButtonCommit_Click(object sender, EventArgs e)
        {
            // Проверка на корректность введенных данных P.S. Добавьте больше проверок
            if(int.TryParse(timeGK.Text, out timeGK_Value) && int.TryParse(timeULK.Text, out timeULK_Value) 
                && int.TryParse(timeFAT_RK.Text, out timeFAT_RK_Value) && int.TryParse(groupID.Text, out groupID_Value))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
                await DisplayAlert("Ошибка", "Проверьте введенные данные", "OK");

        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPickerItem = pickerFaculties.Items[pickerFaculties.SelectedIndex];
        }
    }
}