using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace AutoCheckin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                int week;
                try
                {
                    week = Convert.ToInt32(client.UploadString("https://stud.kubsau.ru/Home/GetCurrentWeekNumber", "").Substring(8, 1));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проверьте куки\n" + ex.Message, "Ошибка");
                    button_Start.Enabled = true;
                    return;
                }
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string resp = client.UploadString("https://stud.kubsau.ru/Home/GetSchedule", "week=" + week);
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
            /////
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
                Pair current = schedule.model[0].GetCurrentPair();
                if (current == null) return;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string resp = client.UploadString("https://stud.kubsau.ru/Home/Checkin", $"disciplineName={HttpUtility.UrlEncode(current.DisciplineName)}&classNumber=" + current.LessonNumber);
                string name = Decode(FindSubstring(resp, "<p class=\"navbar-nav ml-auto\">", "</p>"));
                if (name != "-1") notifyIcon1.ShowBalloonTip(1000, this.Text, $"{name} посетил пару №{ current.LessonNumber}: \"{current.DisciplineName}\" в {DateTime.Now}", ToolTipIcon.None);
                else MessageBox.Show("Проверьте куки", "Ошибка");
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                MessageBox.Show(Convert.ToString((int)resp.StatusCode), resp.StatusDescription);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
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
    }

    #region JSON Parser classes
    public class Pair
    {
        public string DisciplineName { get; set; }
        public int LessonNumber { get; set; }
        private DateTime start;
        private DateTime end;
        public DateTime CheckinDateStart { get => start; set => start = value.ToLocalTime(); }
        public DateTime CheckinDateEnd { get => end; set => end = value.ToLocalTime(); }
    }

    public class Model
    {
        public List<Pair> Pairs { get; set; }

        public Pair GetCurrentPair()
        {
            foreach (Pair pair in Pairs)
            {
                if (pair.CheckinDateStart < DateTime.Now && pair.CheckinDateEnd > DateTime.Now) return pair;
            }
            return null;
        }
    }

    public class Schedule
    {
        public List<Model> model { get; set; }
    }
    #endregion
}
