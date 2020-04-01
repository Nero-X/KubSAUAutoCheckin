﻿using System;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace AutoCheckin
{
    public class Student
    {
        public Schedule Schedule { get; set; }
        public string Cookie { get; set; }
        public WebClient WebClient { get; private set; }

        public Student(string cookie)
        {
            Cookie = cookie;
            WebClient = new WebClient();
            WebClient.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + Cookie);
        }

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
    }
}
