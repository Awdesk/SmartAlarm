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

        public Parser(string groupID_Value, string faculties)
        {
            this.url = $"https://timetable.tusur.ru/faculties/{faculties}/groups/{groupID_Value}";
        }
        /// <summary>
        /// Парсит расписание за текущую неделю
        /// </summary>
        public List<LessonJSON> ParseTimetable()
        {
            List<LessonJSON> lessons = new List<LessonJSON>();
            var doc = new HtmlWeb().Load(url);
            var screen_reader_element = doc.DocumentNode.SelectSingleNode("//div[@class='timetable_wrapper']//table[@class='screen-reader-element']");
            var days = screen_reader_element.SelectNodes("//tr").Where((num, index) => index % 2 == 0).ToList();

            int count = 0;
            foreach (var day in days)
            {
                if (count < 6)
                {
                    var discipline = day.SelectSingleNode("//div[@class='hidden for_print']//span[@class='discipline'][text()]").InnerText.Trim();
                    var auditoriums = day.SelectSingleNode("//span[@class='auditoriums'][text()]").InnerText.Trim();
                    string[] data = new string[] { day.SelectSingleNode("//div[@class='modal-body']/p[2][text()]").InnerText.Split('\n')[2].Trim(),
                    day.SelectSingleNode("//div[@class='modal-body']/p[3][text()]").InnerText.Split('\n')[2].Trim() };
                    lessons.Add(new LessonJSON { Discipline = discipline, Auditorums = auditoriums, Date = data[0], Time = data[1] });
                    day.RemoveAll();
                }
                else
                    break;
                ++count;
            }
            return lessons;
        }
    }
}