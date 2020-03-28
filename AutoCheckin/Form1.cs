using System;
using System.Reflection;
using System.Windows.Forms;
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

        Student student;

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
                button_Start.Text = "...";
                student = new Student(textBox_Cookie.Text);
                if (student.Schedule != null)
                {
                    student.Checkin(notifyIcon1);
                    timer1.Start();
                    button_Start.Enabled = true;
                    button_Start.Text = "Стоп";
                }
                else { }
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

        #region Tray
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Visible = false;
        }
        #endregion Tray

        private void timer1_Tick(object sender, EventArgs e)
        {
            student.CheckinAsync(notifyIcon1);
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
}
