using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Alarm
{
    internal struct Lessons
    {
        public DateTime time;
        public string name;

        public Lessons(DateTime time, string name)
        {
            this.time = time;
            this.name = name;
        }

    }
    internal class Parser
    {
        private readonly string url;

        public Parser(string groupID_Value, string faculties)
        {
            this.url = $"https://timetable.tusur.ru/faculties/{faculties}/groups/{groupID_Value}";
        }
        static private string __normalize_text(string text)
        {
            if (text != null)
            {
                string striped_text = text.Trim();
                string replaced_text = striped_text.Replace(" ", "");
                return replaced_text.Replace("\n", " ");
            }
            return null;
        }

        public List<Dictionary<string, object>> ParseTimetable()
        {
            List<Dictionary<string, object>> timetable = new List<Dictionary<string, object>>();
            var doc = new HtmlWeb().Load(url);

            var table = doc.DocumentNode.SelectSingleNode("//table[@class='table']");
            var thead = table.SelectSingleNode("thead");
            var tbody = table.SelectSingleNode("tbody");
            var days = thead.SelectNodes("tr/th").Skip(1).Select(d => __normalize_text(d.InnerText)).ToList();
            var rows = tbody.SelectNodes("tr");

            foreach (var day in days)
            {
                List<Dictionary<string, object>> lessons = new List<Dictionary<string, object>>();
                for (int i = 0; i < rows.Count; i++)
                {
                    var time = rows[i].SelectSingleNode("th[@class='time']");
                    var lesson = rows[i].SelectNodes("td")[days.IndexOf(day)];
                    var discipline = lesson.SelectSingleNode("span[@class='discipline']");
                    var kind = lesson.SelectSingleNode("span[@class='kind']");
                    var teacher = lesson.SelectSingleNode("span[@class='group']");

                    string normalized_discipline = discipline != null ? __normalize_text(discipline.InnerText) : null;
                    string normalized_kind = kind != null ? __normalize_text(kind.InnerText) : null;
                    string normalized_teacher = teacher != null ? __normalize_text(teacher.InnerText) : null;
                    string normalized_time = time != null ? __normalize_text(time.InnerText) : null;

                    lessons.Add(new Dictionary<string, object>
                {
                    { "time", normalized_time },
                    { "discipline", normalized_discipline },
                    { "kind", normalized_kind },
                    { "teacher", normalized_teacher }
                });
                }
                timetable.Add(new Dictionary<string, object>
            {
                { "day", day },
                { "lessons", lessons }
            });
            }

            return timetable;
        }
    }
}
