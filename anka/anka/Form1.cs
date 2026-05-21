using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TaskManager
{
    /// <summary>
    /// Главная форма приложения.
    /// </summary>
    public partial class Form1 : Form
    {
        // Список задач
        private List<MyTask> tasks = new List<MyTask>();

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            cmbFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Добавляет новую задачу в список.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы кнопки.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверка названия
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            // Проверка приоритета
            if (cmbPriority.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите приоритет задачи!");
                return;
            }

            // Создание задачи
            MyTask task = new MyTask();

            task.Title = txtTitle.Text;
            task.Description = rtbDesc.Text;
            task.DueDate = datepicker.Value;
            task.Priority = cmbPriority.Text;
            task.IsCompleted = DoneChecker.Checked;

            // Добавление
            tasks.Add(task);

            // Обновление списка
            cmbFilter_SelectedIndexChanged(null, null);

            // Очистка полей
            ClearFields();
        }

        /// <summary>
        /// Редактирование задачи.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Проверка выбора
            if (Tasks.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбрана задача для выполнения действия");
                return;
            }

            // Проверка приоритета
            if (cmbPriority.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите приоритет задачи!");
                return;
            }

            MyTask task = (MyTask)Tasks.SelectedItem;

            // Изменение данных
            task.Title = txtTitle.Text;
            task.Description = rtbDesc.Text;
            task.DueDate = datepicker.Value;
            task.Priority = cmbPriority.Text;
            task.IsCompleted = DoneChecker.Checked;

            cmbFilter_SelectedIndexChanged(null, null);
            ClearFields();
        }

        /// <summary>
        /// Удаление выбранных задач.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Проверка выбора
            if (Tasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Не выбрана задача для выполнения действия");
                return;
            }

            // Удаление нескольких задач
            for (int i = Tasks.SelectedItems.Count - 1; i >= 0; i--)
            {
                MyTask task = (MyTask)Tasks.SelectedItems[i];

                tasks.Remove(task);
            }

            cmbFilter_SelectedIndexChanged(null, null);
            ClearFields();
        }

        /// <summary>
        /// Сортировка задач по дате.
        /// </summary>
        private void btnSort_Click(object sender, EventArgs e)
        {
            tasks.Sort((a, b) => a.DueDate.CompareTo(b.DueDate));

            cmbFilter_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Выбор задачи из списка.
        /// </summary>
        private void Tasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверка выбора
            if (Tasks.SelectedIndex == -1)
                return;

            MyTask task = (MyTask)Tasks.SelectedItem;

            // Заполнение полей
            txtTitle.Text = task.Title;
            rtbDesc.Text = task.Description;
            datepicker.Value = task.DueDate;
            cmbPriority.Text = task.Priority;
            DoneChecker.Checked = task.IsCompleted;
        }

        /// <summary>
        /// Отрисовка задач.
        /// </summary>
        private void Tasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            MyTask task = (MyTask)Tasks.Items[e.Index];

            e.DrawBackground();

            // Цвет текста
            Brush color = Brushes.Black;

            // Просроченная задача
            if (task.DueDate.Date < DateTime.Today &&
                task.IsCompleted == false)
            {
                color = Brushes.Red;
            }

            if (task.IsCompleted)
            {
                color = Brushes.Green;
            }

            // Рисуем текст
            e.Graphics.DrawString(
                task.ToString(),
                e.Font,
                color,
                e.Bounds
            );

            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Очистка полей формы.
        /// </summary>
        private void ClearFields()
        {
            txtTitle.Clear();

            rtbDesc.Clear();

            datepicker.Value = DateTime.Today;

            // Убираем выбранный приоритет
            cmbPriority.SelectedIndex = -1;

            DoneChecker.Checked = false;
        }

        /// <summary>
        /// Фильтр задач.
        /// </summary>
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tasks.Items.Clear();

            if (cmbFilter.Text == "Все")
            {
                foreach (MyTask task in tasks)
                {
                    Tasks.Items.Add(task);
                }
            }

            else if (cmbFilter.Text == "Низкий")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.Priority == "Низкий")
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }

            else if (cmbFilter.Text == "Средний")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.Priority == "Средний")
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }

            else if (cmbFilter.Text == "Высокий")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.Priority == "Высокий")
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }

            else if (cmbFilter.Text == "Просроченные")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.DueDate.Date < DateTime.Today &&
                        task.IsCompleted == false)
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }

            else if (cmbFilter.Text == "Готовые")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.IsCompleted == true)
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }

            else if (cmbFilter.Text == "Не готовые")
            {
                foreach (MyTask task in tasks)
                {
                    if (task.IsCompleted == false)
                    {
                        Tasks.Items.Add(task);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Класс задачи.
    /// </summary>
    public class MyTask
    {
        /// <summary>
        /// Название задачи.
        /// </summary>
        public string Title;

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description;

        /// <summary>
        /// Дата выполнения.
        /// </summary>
        public DateTime DueDate;

        /// <summary>
        /// Приоритет задачи.
        /// </summary>
        public string Priority;

        /// <summary>
        /// Статус выполнения.
        /// </summary>
        public bool IsCompleted;

        /// <summary>
        /// Текст задачи для ListBox.
        /// </summary>
        public override string ToString()
        {
            string status = "[ ]";

            if (IsCompleted)
            {
                status = "[Готово]";
            }

            return status + " " +
                   Title + " (" +
                   DueDate.ToShortDateString() +
                   ", " + Priority + ")";
        }
    }
}