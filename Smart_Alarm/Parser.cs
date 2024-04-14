using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Alarm
{
    public struct Lesson
    {
        /// <param name = time" > Время начало пары c лишними символами, например: "\n            Время проведения:\n            10:40-12:15\n          " </param>
        /// <param name = date" > Дата начало пары с лишними символами, например: Дата проведения:\n          08.04.2024\n  </param>
        // В конструкторе необходимо убрать лишние символы. Привести к типу DataTime 
        public Lesson(string discipline, string auditoriums, string date, string time)
        {
            this.auditoriums = auditoriums;
            this.time = time;
            this.discipline = discipline;
            this.date = date;
        }
        public string discipline;
        public string auditoriums;
        public string date;
        public string time;
    }

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
        public List<Lesson> ParseTimetable()
        {
            List<Lesson> lessons = new List<Lesson>();
            var doc = new HtmlWeb().Load(url);
            var screen_reader_element = doc.DocumentNode.SelectSingleNode("//div[@class='timetable_wrapper']//table[@class='screen-reader-element']");
            var days = screen_reader_element.SelectNodes("//tr").Where((num, index) => index % 2 == 0).ToList();

            int count = 0;
            foreach (var day in days)
            {
                if (count < 6)
                {
                    var discipline = day.SelectSingleNode("/span[@class='discipline'][text()]").InnerText;
                    var auditoriums = day.SelectSingleNode("/span[@class='auditoriums'][text()]").InnerText;
                    string[] data = new string[] { day.SelectSingleNode("/div[@class='modal-body']/p[2][text()]").InnerText,
                    day.SelectSingleNode("/div[@class='modal-body']/p[3][text()]").InnerText };
                    Lesson newLesson = new Lesson(discipline, auditoriums, data[0], data[1]);
                    lessons.Add(newLesson);
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