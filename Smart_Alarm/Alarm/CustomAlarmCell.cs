using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using Xamarin.Forms;

namespace Smart_Alarm.Alarm
{
    class CustomAlarmCell : ViewCell
    {
        public CustomAlarmCell() {
            var timeLabel = new Label();
            var descriptionLabel = new Label();
            var on_off_switch = new Switch();
            var verticaLayout = new StackLayout();
            var horizontalLayout = new StackLayout() { BackgroundColor = Color.Olive };
            //set bindings
            timeLabel.SetBinding(Label.TextProperty, new Binding("Time"));
            descriptionLabel.SetBinding(Label.TextProperty, new Binding("Where"));
            on_off_switch.SetBinding(Switch.IsToggledProperty, new Binding("Turn")
            {
                FallbackValue = true, // Устанавливаем значение по умолчанию true
                Mode = BindingMode.TwoWay // Устанавливаем режим привязки на двустороннюю
            });
            //Set properties for desired design
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            on_off_switch.HorizontalOptions = LayoutOptions.End;
            on_off_switch.VerticalOptions = LayoutOptions.Center;
            timeLabel.FontSize = 24;
            //add views to the view hierarchy
            verticaLayout.Children.Add(timeLabel);
            verticaLayout.Children.Add(descriptionLabel);
            horizontalLayout.Children.Add(verticaLayout);
            horizontalLayout.Children.Add(on_off_switch);

            // add to parent view
            View = horizontalLayout;
        }

    }
}
