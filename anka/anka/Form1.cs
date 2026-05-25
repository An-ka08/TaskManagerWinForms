using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        private List<MyTask> tasks = new List<MyTask>();

        public Form1()
        {
            InitializeComponent();
            cmbFilter.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "" || cmbPriority.SelectedIndex < 0)
            {
                MessageBox.Show("Нужно заполнить название и выбрать приоритет");
                return;
            }

            MyTask t = new MyTask();
            t.Title = txtTitle.Text;
            t.Description = rtbDesc.Text;
            t.DueDate = dtpDate.Value;
            t.Priority = cmbPriority.Text;
            t.IsCompleted = chkIsDone.Checked;

            tasks.Add(t);
            RefreshList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedIndex < 0 || cmbPriority.SelectedIndex < 0) return;

            MyTask t = (MyTask)lbTasks.SelectedItem;

            t.Title = txtTitle.Text;
            t.Description = rtbDesc.Text;
            t.DueDate = dtpDate.Value;
            t.Priority = cmbPriority.Text;
            t.IsCompleted = chkIsDone.Checked;

            RefreshList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedItems.Count == 0) return;

            List<MyTask> toDelete = new List<MyTask>();

            foreach (MyTask selectedTask in lbTasks.SelectedItems)
            {
                toDelete.Add(selectedTask);
            }

            foreach (MyTask task in toDelete)
            {
                tasks.Remove(task);
            }

            RefreshList();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            tasks.Sort((x, y) => x.DueDate.CompareTo(y.DueDate));
            RefreshList();
        }

        private void lbTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTasks.SelectedItem == null) return;

            MyTask t = (MyTask)lbTasks.SelectedItem;
            txtTitle.Text = t.Title;
            rtbDesc.Text = t.Description;
            dtpDate.Value = t.DueDate;
            cmbPriority.Text = t.Priority;
            chkIsDone.Checked = t.IsCompleted;
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            lbTasks.Items.Clear();
            string f = cmbFilter.Text;

            for (int i = 0; i < tasks.Count; i++)
            {
                MyTask t = tasks[i];

                if (f == "Все") lbTasks.Items.Add(t);
                else if (f == "Низкий" && t.Priority == "Низкий") lbTasks.Items.Add(t);
                else if (f == "Средний" && t.Priority == "Средний") lbTasks.Items.Add(t);
                else if (f == "Высокий" && t.Priority == "Высокий") lbTasks.Items.Add(t);
                else if (f == "Готовые" && t.IsCompleted) lbTasks.Items.Add(t);
                else if (f == "Не готовые" && !t.IsCompleted) lbTasks.Items.Add(t);
                else if (f == "Просроченные" && t.DueDate.Date < DateTime.Today && !t.IsCompleted) lbTasks.Items.Add(t);
            }

            txtTitle.Text = "";
            rtbDesc.Text = "";
            cmbPriority.SelectedIndex = -1;
            chkIsDone.Checked = false;
            dtpDate.Value = DateTime.Today;
        }

        private void lbTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            MyTask t = (MyTask)lbTasks.Items[e.Index];
            e.DrawBackground();

            Brush b = Brushes.Black;
            if (t.IsCompleted) b = Brushes.Green;
            else if (t.DueDate.Date < DateTime.Today) b = Brushes.Red;

            e.Graphics.DrawString(t.ToString(), e.Font, b, e.Bounds);
            e.DrawFocusRectangle();
        }
    }

    public class MyTask
    {
        public string Title;
        public string Description;
        public DateTime DueDate;
        public string Priority;
        public bool IsCompleted;

        public override string ToString()
        {
            return (IsCompleted ? "[Готово] " : "[ ] ") + Title + " (" + DueDate.ToShortDateString() + ", " + Priority + ")";
        }
    }
}