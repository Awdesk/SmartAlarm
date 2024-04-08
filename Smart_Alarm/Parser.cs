using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
        private string url;
        public Parser(string groupID_Value, string faculties) { 
            this.url = $"https://timetable.tusur.ru/{faculties}/{groupID_Value}";
        } 
    }
}
