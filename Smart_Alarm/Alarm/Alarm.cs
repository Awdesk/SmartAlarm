using System;
using System.Collections.Generic;
using System.Text;
using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace Smart_Alarm.Alarm
{
    [BroadcastReceiver]
    public class Alarm: BroadcastReceiver
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public MediaPlayer Ringtone { get; set; }
        public bool IsToggled { get; set; } = true;
        public override void OnReceive(Context context, Intent intent)
        {
            
        }
        public class AlarmService
        {
            public void SetRepeatingAlarm(Context context)
            {
                AlarmManager alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
                Intent alarmIntent = new Intent(context, typeof(Alarm));
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

                // Устанавливаем будильник на повторение каждые 10 минут
                alarmManager.SetRepeating(AlarmType.RtcWakeup, DateTime.Now.Millisecond, 10 * 60 * 1000, pendingIntent);
            }
        }

    }

}
