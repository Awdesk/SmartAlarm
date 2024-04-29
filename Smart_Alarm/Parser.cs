using HtmlAgilityPack;
using Smart_Alarm.FilesJSON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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

        public Parser(SettingsJSON settings)
        {
            url = $"https://timetable.tusur.ru/faculties/{settings.Faculty}/groups/{settings.GroupID}";
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

                lessons.Add(new LessonJSON { Discipline = disciplina, Auditorums = auditoriya, Date = data_provedeniya, Time = time });
            }
            return lessons;
        }
    }
}