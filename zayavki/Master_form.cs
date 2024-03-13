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
    public partial class Master_form : Form
    {
        private const int MaxProductsPerPage = 5;
        private int currentPage = 1;
        private List<Requests> All_requests = new List<Requests>();
        private List<Requests> Displayed_requests = new List<Requests>();
        string login;
        int id;
        public Master_form(string login_)
        {
            InitializeComponent();
            login = login_;
        }

        public void Requests_load()
        {
            DB dB = new DB();
            dB.Open_conn();
            using (var command = new NpgsqlCommand("SELECT * FROM request WHERE master_id = @master_id ORDER BY id ASC", dB.Get_Conn()))
            {
                command.Parameters.Add("@master_id", NpgsqlDbType.Integer).Value = id;
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
        private void Master_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Master_form_Load(object sender, EventArgs e)
        {
            DB dB = new DB();
            int user_id = dB.Get_user_id(login);

            DB dB1 = new DB();
            id = dB1.Get_master_id(user_id);
            Requests_load();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            Displayed_requests = All_requests.Where(product =>
                product.Id.ToString().StartsWith(searchText) ||
                product.Equipment.ToString().StartsWith(searchText)).ToList();

            currentPage = 1;

            DisplayCurrentPage();
        }

        private void выполнитьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем выбранную панель
            Panel selectedPanel = (Panel)contextMenuStrip1.SourceControl;

            // Получаем индекс выбранной панели
            int panelIndex = int.Parse(selectedPanel.Name.Replace("panel", ""));

            // Получаем соответствующий продукт
            Requests selectedProduct = Displayed_requests[(currentPage - 1) * MaxProductsPerPage + panelIndex];

            if(selectedProduct.Satus_id == 3)
            {
                MessageBox.Show("Данная заявка уже выполнена!");
                return;
            }

            Master_form2 form = new Master_form2(login, selectedProduct);
            this.Hide();
            form.Show();
        }

        private void Stat_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
