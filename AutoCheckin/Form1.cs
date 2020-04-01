﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Onova;
using Onova.Services;
using System.Linq;
using System.IO;

namespace AutoCheckin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label_ver.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        List<Student> students = new List<Student>();

        enum Status
        {
            Пусто,
            Посещение,
            Загрузка,
            Ошибка
        }

        #region Tray & Save/load
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdate();
            if (File.Exists("cookies.txt"))
            {
                using (var sr = File.OpenText("cookies.txt"))
                    while (sr.EndOfStream == false)
                    {
                        dgv.Rows.Add(sr.ReadLine());
                    }
            }
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Visible = false;
            using (var sw = File.CreateText("cookies.txt"))
                for(int i = 0; i < dgv.RowCount; i++)
                {
                    sw.WriteLine(dgv["cookie", i].Value);
                }
        }
        #endregion Tray & Save/load

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(var student in students)
            {
                CheckinAsync(student);
            }
        }

        async void CheckinAsync(Student student) // TODO: Сообщения преподавателям
        {
            var rowIndex = dgv.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["cookie"].Value as string == student.Cookie).FirstOrDefault().Index;
            SetStatus(rowIndex, Status.Загрузка);
            try
            {
                await Task.Run(() => student.Checkin(notifyIcon1));
                SetStatus(rowIndex, Status.Посещение);
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                SetStatus(rowIndex, Status.Ошибка, resp.StatusDescription);
            }
            catch (Exception)
            {
                dgv["button", rowIndex].Value = "Старт";
                students.Remove(student);
                SetStatus(rowIndex, Status.Ошибка, "Неправильно введены или истёк срок действия куки");
            }
        }

        async void AutoUpdate()
        {
            using (var manager = new UpdateManager(
                new GithubPackageResolver("Nero-X", "KubSAUAutoCheckin", "AutoCheckin*.zip"),
                new ZipPackageExtractor()))
            {
                await manager.CheckPerformUpdateAsync();
            }
        }

        void SetStatus(int rowIndex, Status status, string description = null)
        {
            var statusCell = dgv["status", rowIndex];
            if (description != null) description = description.Insert(0, ". ");
            switch (status)
            {
                case Status.Посещение:
                    statusCell.Value = "Посещение" + description;
                    statusCell.Style.BackColor = Color.PaleGreen;
                    break;
                case Status.Загрузка:
                    statusCell.Value = "Загрузка" + description;
                    statusCell.Style.BackColor = Color.Gray;
                    break;
                case Status.Ошибка:
                    statusCell.Value = "Ошибка" + description;
                    statusCell.Style.BackColor = Color.LightCoral;
                    break;
                case Status.Пусто:
                    statusCell.Value = null;
                    statusCell.Style.BackColor = Color.White;
                    break;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e) // TODO: Лог
        {
            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var buttonCell = dgv[e.ColumnIndex, e.RowIndex];
                if (buttonCell.Value as string == "Старт") StartAsync(e.RowIndex);
                else Stop(e.RowIndex);
            }
        }

        private void button_startAll_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgv.Rows)
            {
                if(row.Cells["button"].Value as string == "Старт") StartAsync(row.Index);
            }
        }

        private void button_stopAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["button"].Value as string == "Стоп") Stop(row.Index);
            }
        }

        async void StartAsync(int rowIndex) // TODO: Получать ФИО
        {
            var button = dgv["button", rowIndex];
            button.Value = "Стоп";
            SetStatus(rowIndex, Status.Загрузка);
            Student student = new Student(dgv[0, rowIndex].Value as string);
            try
            {
                await Task.Run(() =>
                {
                    student.GetSchedule();
                    student.Checkin(notifyIcon1);
                });
                students.Add(student);
                SetStatus(rowIndex, Status.Посещение);
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                SetStatus(rowIndex, Status.Ошибка, resp.StatusDescription);
                students.Add(student);
            }
            catch (Exception)
            {
                SetStatus(rowIndex, Status.Ошибка, "Неправильно введены или истёк срок действия куки");
                button.Value = "Старт";
            }
        }

        void Stop(int rowIndex)
        {
            dgv["button", rowIndex].Value = "Старт";
            students.RemoveAll(x => x.Cookie == dgv["cookie", rowIndex].Value as string);
            SetStatus(rowIndex, Status.Пусто);
        }

        private void dgv_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            students.RemoveAll(x => x.Cookie == dgv["cookie", e.Row.Index].Value as string);
        }

        private void dgv_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgv["button", e.Row.Index - 1].Value = "Старт";
        }
    }
}
