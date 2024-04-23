using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPage1 : FlyoutPage
    {
        FlyoutPage1Flyout flyoutPage;
        public FlyoutPage1()
        {
            flyoutPage = new FlyoutPage1Flyout();
            Flyout = flyoutPage;
            Detail = new NavigationPage(new FlyoutDetailAlarm());

            flyoutPage.MenuItemsListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPage1FlyoutMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                flyoutPage.MenuItemsListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}