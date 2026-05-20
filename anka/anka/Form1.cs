using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }





        public enum TaskPriority { Low, Medium, High }
        public class MyTask
        {   public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public TaskPriority Priority { get; set; }
            public bool IsCompleted { get; set; }

            public override string ToString()
            {
                string status = IsCompleted ? "[Готово]" : "[ ]";
                return $"{status} {Title} {DueDate.ToShortDateString()},{Priority})";
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void Tasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tasks_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
    }
}
