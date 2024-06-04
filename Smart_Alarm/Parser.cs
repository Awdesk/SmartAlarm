using HtmlAgilityPack;
using Smart_Alarm.FilesJSON;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Alarm
{

    /// <summary>
    /// Парсер сайта ТУСУР. В конструторе требует указать номер группы 
    /// и название факультета. Чтобы воспользоваться, вызовите метод
    /// ParseTimetable()
    /// </summary>
    internal class Parser
    {
        private readonly string url;
        private SettingsJSON settings;
        /// <param name="flag">Если параметр указан явно, то парсим следующую неделю </param>
        public Parser(SettingsJSON settings, bool flag = false)
        {
            url = $"https://timetable.tusur.ru/faculties/{settings.Faculty}/groups/{settings.GroupID}?week_id={CheckWeekID()}";
            this.settings = settings;
        }
        public Parser(SettingsJSON settings)
        {
            url = $"https://timetable.tusur.ru/faculties/{settings.Faculty}/groups/{settings.GroupID}";
            this.settings = settings;
        }
        private int CheckWeekID()
        {
            // ID недели на 13.05.2024
            int weekID = 718;
            int weekElapsed = (int)(DateTime.Now - new DateTime(2024, 5, 13)).TotalDays / 7;
            //костыль до тех пор, пока не пройдут две недели :)
            //return (weekID + weekElapsed) == 718 ? 719 : weekID + weekElapsed;
            return weekID + weekElapsed;
        }
        /// <summary>
        /// Парсит расписание за текущую неделю
        /// </summary>
        public List<LessonJSON> ParseTimetable()
        {
            List<LessonJSON> lessons = new List<LessonJSON>();
            var doc = new HtmlWeb().Load(url);
            var days = doc.DocumentNode.SelectNodes("/html/body/div[1]/div[7]/div[2]/div[3]/div/div[1]/table[1]/tr").Where(trNode => trNode.InnerLength > 2000).ToList();
            foreach (var day in days)
            {
                string disciplina = day.SelectSingleNode(".//div[1]/div/div[2]/span[1]").InnerText;
                string auditoriya = day.SelectSingleNode(".//div[1]/div/div[2]/span[3]").InnerText;
                string data_provedeniya = day.SelectSingleNode(".//div[2]/noindex/div/div/div/div[2]/p[2]").InnerText.Substring(30).Trim();
                string time = day.SelectSingleNode(".//div[2]/noindex/div/div/div/div[2]/p[3]").InnerText.Substring(34).Trim();
                time = time.Substring(0, time.IndexOf('-'));
                string auditoriyaLower = auditoriya.ToLower();
                int timeToSubtract = 0;

                if (auditoriyaLower.Contains("рк") || auditoriyaLower.Contains("ф") || auditoriyaLower.Contains("фэт"))
                {
                    timeToSubtract = int.Parse(settings.TimeFAT_RK);
                }
                else if (auditoriyaLower.Contains("улк"))
                {
                    timeToSubtract = int.Parse(settings.TimeULK);
                }
                else if (auditoriyaLower.Contains("гк"))
                {
                    timeToSubtract = int.Parse(settings.TimeGK);
                }
                time = (TimeSpan.Parse(time).Subtract(TimeSpan.FromMinutes(timeToSubtract))).ToString(@"hh\:mm");
                lessons.Add(new LessonJSON { Discipline = disciplina, Auditorums = auditoriya, Date = data_provedeniya, Time = time });
            }
            return lessons;
        }
    }
}