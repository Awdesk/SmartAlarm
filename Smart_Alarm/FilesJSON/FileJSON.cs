﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Smart_Alarm.FilesJSON
{
    public class SettingsJSON
    {
        public string GroupID { get; set; }
        public string Faculty { get; set; }
        public string TimeULK { get; set; }
        public string TimeGK { get; set; }
        public string TimeFAT_RK { get; set; }
    }
    public class LessonJSON
    /// <param name = time" > Время начало пары, например: 10:40 </param>
    /// <param name = date" > Дата начало пары, например: 08.04.2024  </param>
    {
        public string Discipline { get; set; }
        public string Auditorums { get; set; }
        public string Time {  get; set; }

        public string Date { get; set; }

        public DateTime DateTime
        {
            get => DateTime.ParseExact($"{Date} {Time}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
