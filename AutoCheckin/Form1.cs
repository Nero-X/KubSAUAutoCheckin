using System;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;
using System.Web;
using Onova;
using Onova.Services;

namespace AutoCheckin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label_ver.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        WebClient client = new WebClient();
        Schedule schedule;

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_Cookie.Clear();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if(!timer1.Enabled)
            {
                if (textBox_Cookie.Text == "")
                {
                    MessageBox.Show("Введите cookie");
                    return;
                }

                button_Start.Enabled = false;
                client.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + textBox_Cookie.Text);
                string resp;
                Info info;
                try
                {
                    resp = client.UploadString("https://stud.kubsau.ru/Home/GetScheduleCommonInfo", "");
                    info = JsonConvert.DeserializeObject<Info>(resp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проверьте куки\n" + ex.Message, "Ошибка");
                    button_Start.Enabled = true;
                    return;
                }
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                resp = client.DownloadString($"https://stud.kubsau.ru/Home/GetSchedule?week={info.Week}&groupId={info.PersonGroups[0].Key}");
                schedule = JsonConvert.DeserializeObject<Schedule>(Decode(resp));

                tryCheck();
                timer1.Start();
                button_Start.Enabled = true;
                button_Start.Text = "Стоп";
            }
            else
            {
                timer1.Stop();
                button_Start.Text = "Старт";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tryCheck();
        }

        void tryCheck()
        {
            try
            {
                Lesson current = schedule.Today?.GetCurrentLesson();
                if (current == null) return;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string resp = client.UploadString("https://stud.kubsau.ru/Home/Checkin", "lessonGuid=" + current.LessonGuid);
                //string name = Decode(FindSubstring(resp, "<p class=\"navbar-nav ml-auto\">", "</p>"));
                if (resp == "\"OK\"") notifyIcon1.ShowBalloonTip(1000, this.Text, $"Студент успешно посетил пару в {DateTime.Now}", ToolTipIcon.None);
                else MessageBox.Show("Проверьте куки\n" + DateTime.Now, "Ошибка");
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                MessageBox.Show(Convert.ToString((int)resp.StatusCode) + "\n" + DateTime.Now, resp.StatusDescription);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + DateTime.Now, ex.Source);
            }
        }

        string Decode(string str)
        {
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            byte[] utf8Bytes = win1251.GetBytes(str);
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        string FindSubstring(string text, string start, string end)
        {
            int startIndex = text.IndexOf(start);
            int endIndex = text.IndexOf(end, startIndex);
            if (startIndex >= 0 && endIndex >= 0) return text.Substring(startIndex + start.Length, endIndex - startIndex - start.Length);
            else return "-1";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        async void AutoUpdate()
        {
            using (var manager = new UpdateManager(
                new GithubPackageResolver("Nero-X", "KubSAUAutoCheckin", "AutoCheckin*.zip"),
                new ZipPackageExtractor()))
            {
                // Check for new version and, if available, perform full update and restart
                await manager.CheckPerformUpdateAsync();
            }
        }
    }

    #region JSON Parser classes
    public partial class Info
    {
        public int Week { get; set; }
        public PersonGroup[] PersonGroups { get; set; }
    }

    public partial class PersonGroup
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public partial class Schedule
    {
        public List<Day> Days { get; set; }

        public Day Today => Days.Find(x => x.IsToday);
    }

    public partial class Day
    {
        public bool IsToday { get; set; }
        public Lesson[] Lessons { get; set; }

        public Lesson GetCurrentLesson()
        {
            foreach (Lesson lesson in Lessons)
            {
                if (lesson.StartTime.TimeOfDay < DateTime.Now.TimeOfDay && lesson.EndTime.TimeOfDay > DateTime.Now.TimeOfDay 
                    && !lesson.LessonGuid.Equals(Guid.Empty)) return lesson;
            }
            return null;
        }
    }

    public partial class Lesson
    {
        public Guid LessonGuid { get; set; }
        private DateTime start;
        private DateTime end;
        public DateTime StartTime { get => start; set => start = value.ToLocalTime(); }
        public DateTime EndTime { get => end; set => end = value.ToLocalTime(); }
        public bool IsCheckedIn { get; set; }
    }
    #endregion
}
