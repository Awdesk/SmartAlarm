using Android.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Alarm.Alarm
{
    public class Alarm
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public MediaPlayer Ringtone { get; set; }
        public bool IsToggled { get; set; } = true;
    }
}
