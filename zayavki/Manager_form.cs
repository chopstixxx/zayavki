using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zayavki
{
    public partial class Manager_form : Form
    {
        private const int MaxProductsPerPage = 5;
        private int currentPage = 1;
        private List<Requests> All_requests = new List<Requests>();
        private List<Requests> Displayed_requests = new List<Requests>();

        public Manager_form()
        {
            InitializeComponent();
        }
        public void Reqauests_load()
        {
            DB dB = new DB();
            dB.Open_conn();
            using (var command = new NpgsqlCommand("SELECT * FROM request ORDER BY id ASC", dB.Get_Conn()))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Requests request = new Requests
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Client_id = Convert.ToInt32(reader["client_id"]),
                            Satus_id = Convert.ToInt32(reader["status_id"]),
                            Master_id = Convert.ToInt32(reader["master_id"]),
                            Equipment = reader["equipment"].ToString(),
                            Malf_type = reader["malf_type"].ToString(),
                            Malf_description = reader["malf_description"].ToString(),
                            Add_date = DateTime.Parse(reader["add_date"].ToString())



                        };
                        All_requests.Add(request);
                    }
                }
            }
        }

        private void DisplayCurrentPage()
        {
            string status = "";
            int startIndex = (currentPage - 1) * MaxProductsPerPage;
            int endIndex = Math.Min(startIndex + MaxProductsPerPage, Displayed_requests.Count);

            for (int i = 0; i < MaxProductsPerPage; i++)
            {
                Panel currentPanel = (Panel)Controls.Find("panel" + i, true)[0];

                if (i < endIndex - startIndex)
                {
                    currentPanel.Visible = true;


                    Label labelnum = (Label)currentPanel.Controls["num" + i];
                    labelnum.Text = Displayed_requests[startIndex + i].Id.ToString();

                    Label equip_name = (Label)currentPanel.Controls["equip" + i];
                    equip_name.Text = Displayed_requests[startIndex + i].Equipment.ToString();

                    Label labeldate = (Label)currentPanel.Controls["date" + i];
                    labeldate.Text = Displayed_requests[startIndex + i].Add_date.ToString("g");

                    Label labelstatus = (Label)currentPanel.Controls["status" + i];
                    int status_id = Displayed_requests[startIndex + i].Satus_id;
                    switch (status_id)
                    {
                        case 1:
                            status = "Создана";
                            break;
                        case 2:
                            status = "В работе";
                            break;
                        case 3:
                            status = "Выполнен";
                            break;

                    }
                    labelstatus.Text = status;
                }
                else
                {
                    currentPanel.Visible = false;
                }
            }



        }
        private void Manager_form_Load(object sender, EventArgs e)
        {
            Reqauests_load();
            Displayed_requests = All_requests;
            DisplayCurrentPage();



            for (int i = 0; i < MaxProductsPerPage; i++)
            {
                Panel currentPanel = (Panel)Controls.Find("panel" + i, true)[0];

                // Добавляем обработчик события для щелчка правой кнопкой мыши на каждой панели
                currentPanel.ContextMenuStrip = contextMenuStrip1;


            }

            filter.Items.Add("Все статусы");
            filter.Items.Add("Создана");
            filter.Items.Add("В работе");
            filter.Items.Add("Выполнена");

            sort.Items.Add("Более новые");
            sort.Items.Add("Более старые");

        }

        private void подробнееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем выбранную панель
            Panel selectedPanel = (Panel)contextMenuStrip1.SourceControl;

            // Получаем индекс выбранной панели
            int panelIndex = int.Parse(selectedPanel.Name.Replace("panel", ""));

            // Получаем соответствующий продукт
            Requests selectedProduct = Displayed_requests[(currentPage - 1) * MaxProductsPerPage + panelIndex];


            One_request_form form = new One_request_form(selectedProduct);
            this.Hide();
            form.Show();

        }

        private void prev_page_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void next_page_Click(object sender, EventArgs e)
        {
            if ((currentPage * MaxProductsPerPage) < Displayed_requests.Count)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }

        private void Manager_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            Displayed_requests = All_requests.Where(product =>
                product.Id.ToString().StartsWith(searchText) ||
                product.Equipment.ToString().StartsWith(searchText)).ToList();

            currentPage = 1;

            DisplayCurrentPage();
        }

        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedSort = filter.SelectedItem.ToString();
            switch (selectedSort)
            {
                case "Все статусы":
                    Displayed_requests = All_requests;
                    break;
                case "Создана":
                    Displayed_requests = All_requests.Where(product => product.Satus_id == 1).ToList();
                    break;
                case "В работе":
                    Displayed_requests = All_requests.Where(product => product.Satus_id == 2).ToList();
                    break;
                case "Выполнена":
                    Displayed_requests = All_requests.Where(product => product.Satus_id == 3).ToList();
                    break;


            }

            DisplayCurrentPage();
        }

        private void sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSort = sort.SelectedItem.ToString();
            switch (selectedSort)
            {
                case "Более старые":
                    Displayed_requests = All_requests.OrderBy(product => product.Add_date).ToList();
                    break;
                case "Более новые":
                    Displayed_requests = All_requests.OrderByDescending(product => product.Add_date).ToList();
                    break;
            }

            DisplayCurrentPage();
        }

        private void Stat_btn_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.Open_conn();

            // Создаем переменные для подсчета количества заказов и общего времени выполнения для каждого статуса
            int createdCount = 0;
            int inProgressCount = 0;
            int completedCount = 0;
            TimeSpan totalCompletionTime = TimeSpan.Zero;

            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT status_id, add_date, finish_date FROM request", db.Get_Conn()))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int status_id = reader.GetInt32(reader.GetOrdinal("status_id"));
                        DateTime add_date = reader.GetDateTime(reader.GetOrdinal("add_date"));
                        DateTime? finish_date = reader.IsDBNull(reader.GetOrdinal("finish_date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("finish_date"));

                        // Увеличиваем счетчик заказов в зависимости от статуса
                        switch (status_id)
                        {
                            case 1:
                                createdCount++;
                                break;
                            case 2:
                                inProgressCount++;
                                break;
                            case 3:
                                completedCount++;

                                // Рассчитываем время выполнения и добавляем его к общему времени выполнения
                                if (finish_date.HasValue)
                                {
                                    TimeSpan completionTime = finish_date.Value - add_date;
                                    totalCompletionTime += completionTime;
                                }
                                break;
                            default:
                                // Можно добавить обработку для неизвестных статусов, если необходимо
                                break;
                        }
                    }
                }
            }

          
            TimeSpan averageCompletionTime = completedCount > 0 ? TimeSpan.FromTicks(totalCompletionTime.Ticks / completedCount) : TimeSpan.Zero;

            // Форматируем среднее время выполнения в формате "часы:минуты"
            string averageCompletionTimeString = (completedCount > 0 ? $"{averageCompletionTime.Hours:D2}:{averageCompletionTime.Minutes:D2}" : "Нет выполненных заказов с определенным временем завершения");

            // Формируем сообщение для отображения в MessageBox
            string message = $"Статистика по статусам заявок:\n" +
                             $"Создан: {createdCount} шт.\n" +
                             $"В работе: {inProgressCount} шт.\n" +
                             $"Выполнен: {completedCount} шт.\n" +
                             $"Среднее время выполнения: {averageCompletionTimeString}";

            // Отображаем сообщение в MessageBox
            MessageBox.Show(message, "Статистика по статусам заказов", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
