using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public Parser(string groupID_Value, string faculties)
        {
            this.url = $"https://timetable.tusur.ru/{faculties}/{groupID_Value}";
        }
        /// <summary>
        /// Создаёт запрос по url к странице
        /// </summary>
        /// <returns> Возвращает html страницу в виде строки </returns>
        // Код еще не проверен. Я устал писать код :(
        public async Task<string> SendGetRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
