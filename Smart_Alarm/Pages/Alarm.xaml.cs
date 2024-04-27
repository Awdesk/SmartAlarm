using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Alarm.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alarm : ContentPage
    {
        public Alarm()
        {
            InitializeComponent();
            if (File.Exists(App.savedTimetablePath))
            {
                File.ReadAllText(App.savedTimetablePath);
            }
        }
    }
}