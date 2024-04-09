using System;
using System.Collections.Generic;
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
        string selectedPickerItem = "";
        uint timeGK_Value = 0, timeULK_Value = 0, timeFAT_RK_Value = 0, groupID_Value = 0;
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
            if(uint.TryParse(timeGK.Text, out timeGK_Value) && uint.TryParse(timeULK.Text, out timeULK_Value) 
                && uint.TryParse(timeFAT_RK.Text, out timeFAT_RK_Value) && uint.TryParse(groupID.Text, out groupID_Value))
            {
                // Сохраняем значения в память телефона
                //Application.Current.Properties["timeGK"] = timeGK_Value;
                //Application.Current.Properties["timeULK"] = timeULK_Value;
                //Application.Current.Properties["groupID"] = groupID_Value;
                //Application.Current.Properties["faculties"] = selectedPickerItem;
                Preferences.Set("timeULK", timeULK_Value);
                Preferences.Set("timeFAT_RK", timeFAT_RK_Value);
                Preferences.Set("groupID", groupID_Value);
                Preferences.Set("faculties", selectedPickerItem);
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