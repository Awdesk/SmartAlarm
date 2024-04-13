using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Smart_Alarm
{
    public struct Lesson
    {
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

    internal class Parser
    {
        private readonly string url;

        public Parser(string groupID_Value, string faculties)
        {
            this.url = $"https://timetable.tusur.ru/faculties/{faculties}/groups/{groupID_Value}";
        }
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