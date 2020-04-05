using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace AutoCheckin
{
    public class Student
    {
        public Schedule Schedule { get; set; }
        public UserInfo UserInfo { get; set; }
        public string Cookie { get; set; }
        private WebClient webClient;
        public WebClient WebClient
        {
            get
            {
                if(webClient == null)
                {
                    webClient = new WebClient();
                    webClient.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + Cookie);
                }
                return webClient;
            }
        }
        public Dictionary<string, string> MessagesRules { get; set; }
        public bool SendingMessagesEnabled { get; set; }

        public string Name => UserInfo?.Name;

        public Student() { }

        public void GetSchedule()
        {
            int week = Convert.ToInt32(WebClient.UploadString("https://stud.kubsau.ru/Home/GetCurrentWeekNumber", "").Substring(8, 1));
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string resp = WebClient.UploadString("https://stud.kubsau.ru/Home/GetSchedule", "week=" + week);
            Schedule = Schedule.FromJson(resp.Decode());
        }

        public void Checkin(NotifyIcon notifyIcon)
        {
            Pair current = Schedule.Today?.GetCurrentPair();
            if (current == null || current.CheckedAtLesson == true) return;
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string resp = WebClient.UploadString("https://stud.kubsau.ru/Home/Checkin", $"disciplineName={HttpUtility.UrlEncode(current.DisciplineName)}&classNumber=" + current.LessonNumber);
            string name = resp.FindSubstring("<p class=\"navbar-nav ml-auto\">", "</p>").Decode();
            if (name != "-1") notifyIcon.ShowBalloonTip(1000, "Autocheckin", $"{name} посетил пару №{current.LessonNumber}: \"{current.DisciplineName}\" в {DateTime.Now}", ToolTipIcon.None);
            else throw new Exception();
        }

        public async void GetScheduleAsync()
        {
            int week = Convert.ToInt32((await WebClient.UploadStringTaskAsync("https://stud.kubsau.ru/Home/GetCurrentWeekNumber", "")).Substring(8, 1));
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string resp = await WebClient.UploadStringTaskAsync("https://stud.kubsau.ru/Home/GetSchedule", "week=" + week);
            Schedule = Schedule.FromJson(resp.Decode());
        }

        public async void CheckinAsync(NotifyIcon notifyIcon)
        {
            Pair current = Schedule.Days[0].GetCurrentPair();
            if (current == null || current.CheckedAtLesson == true) return;
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string resp = await WebClient.UploadStringTaskAsync("https://stud.kubsau.ru/Home/Checkin", $"disciplineName={HttpUtility.UrlEncode(current.DisciplineName)}&classNumber=" + current.LessonNumber);
            string name = resp.FindSubstring("<p class=\"navbar-nav ml-auto\">", "</p>").Decode();
            if (name != "-1") notifyIcon.ShowBalloonTip(1000, "Autocheckin", $"{name} посетил пару №{current.LessonNumber}: \"{current.DisciplineName}\" в {DateTime.Now}", ToolTipIcon.None);
            else throw new Exception();
        }

        public void GetUserInfo()
        {
            string resp = WebClient.DownloadString("https://stud.kubsau.ru/Home/GetUserInfo");
            UserInfo = UserInfo.FromJson(resp.Decode());
        }

        public Schedule GetFullSchedule()
        {
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string resp = WebClient.UploadString("https://stud.kubsau.ru/Schedule/GetSchedule", "week=" + 1);
            Schedule schedule = Schedule.FromJson(resp.Decode());
            WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            resp = WebClient.UploadString("https://stud.kubsau.ru/Schedule/GetSchedule", "week=" + 2);
            schedule.Days.AddRange(Schedule.FromJson(resp.Decode()).Days);
            return schedule;
        }
    }
}
