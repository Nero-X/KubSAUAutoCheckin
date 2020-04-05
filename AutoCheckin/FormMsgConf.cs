using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckin
{
    public partial class FormMsgConf : Form
    {
        Student Student;
        Dictionary<string, List<string>> Dictionary;

        public FormMsgConf(Student student)
        {
            Student = student;
            InitializeComponent();
            try
            {
                Dictionary = student.GetFullSchedule().Dictionary;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удаётся загрузить полное расписание. Список возможных предметов и преподавателей может быть неполным", ex.Message);
                Dictionary = student.Schedule.Dictionary;
            }
            finally
            {
                Column1.Items.AddRange(Dictionary.Keys.ToArray());
                var allValues = new List<string>();
                foreach(var value in Dictionary.Values)
                {
                    allValues.AddRange(value);
                }
                Column2.Items.AddRange(allValues.ToArray());
            }
        }

        private void radioButton_Off_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                dgv.Enabled = false;
                Student.SendingMessagesEnabled = false;
            }
        }

        private void radioButton_On_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                dgv.Enabled = true;
                Student.SendingMessagesEnabled = true;
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) //////////////////////
        {
            var cell = dgv.SelectedCells[0] as DataGridViewComboBoxCell;
            if (cell.ColumnIndex == 1)
            {
                ComboBox cb = e.Control as ComboBox;
                string discipline = cell.OwningRow.Cells[0].Value as string;
                if (cb != null && discipline != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.Items.Clear();
                    cb.Items.AddRange(Dictionary[discipline].ToArray());
                    /*Column2.Items.Clear();
                    Column2.Items.AddRange(Dictionary[discipline].ToArray());*/
                    //cell.Items.Clear();
                    //cell.Items.AddRange(Dictionary[discipline].ToArray());

                }
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) //////////////////
        {
            string discipline = dgv[0, e.RowIndex].EditedFormattedValue as string;
            if (e.ColumnIndex == 1 && discipline != "")
            {
                //var cell = dgv[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell;
                //cell.Items.Clear();
                //cell.Items.Add(cell.EditedFormattedValue);
                //cell.Items.AddRange(Dictionary[discipline].ToArray());
                if (!Dictionary[discipline].Contains(e.FormattedValue as string))
                {
                    Column2.Items.Add(e.FormattedValue as string);
                    Dictionary[discipline].Add(e.FormattedValue as string);
                }
                //cell.Value = e.FormattedValue;
            }
        }

        /*private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == Column2.DisplayIndex)
            {
                if (!Column2.Items.Contains(e.FormattedValue))
                {
                    Column2.Items.Add(e.FormattedValue);
                }
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv.CurrentCellAddress.X == Column2.DisplayIndex)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }*/

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
