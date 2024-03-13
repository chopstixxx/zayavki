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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace zayavki
{
    public partial class Client_form : Form
    {
        string login;
        int id;
        private List<Requests> All_requests = new List<Requests>();
        private const int MaxProductsPerPage = 5;
        private int currentPage = 1;

        public Client_form(string login_)
        {
            InitializeComponent();
            login = login_;
        }

        public void Client_requests_load()
        {
            DB dB = new DB();
            dB.Open_conn();
            using (var command = new NpgsqlCommand("SELECT * FROM request WHERE client_id = @client_id ORDER BY id ASC", dB.Get_Conn()))
            {
                command.Parameters.Add("@client_id", NpgsqlDbType.Integer).Value = id;
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
        private void button1_Click(object sender, EventArgs e)
        {
            Request_form form = new Request_form(login, id);
            form.Show();
            this.Hide();
        }

        private void Client_form_Load(object sender, EventArgs e)
        {
            DB dB = new DB();
            int user_id = dB.Get_user_id(login);
            DB dB1 = new DB();
            id = dB1.Get_client_id(user_id);
            Client_requests_load();
            DisplayCurrentPage();
            if (All_requests.Count < 1)
            {
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label1.Visible = true;

                return;
            }
        }
        private void DisplayCurrentPage()
        {
            string status = "";
            int startIndex = (currentPage - 1) * MaxProductsPerPage;
            int endIndex = Math.Min(startIndex + MaxProductsPerPage, All_requests.Count);

            for (int i = 0; i < MaxProductsPerPage; i++)
            {
                Panel currentPanel = (Panel)Controls.Find("panel" + i, true)[0];

                if (i < endIndex - startIndex)
                {
                    currentPanel.Visible = true;


                    Label labelnum = (Label)currentPanel.Controls["num" + i];
                    labelnum.Text = All_requests[startIndex + i].Id.ToString();

                    Label equip_name = (Label)currentPanel.Controls["equip" + i];
                    equip_name.Text = All_requests[startIndex + i].Equipment.ToString();

                    Label labeldate = (Label)currentPanel.Controls["date" + i];
                    labeldate.Text = All_requests[startIndex + i].Add_date.ToString("g");

                    Label labelstatus = (Label)currentPanel.Controls["status" + i];
                    int status_id = All_requests[startIndex + i].Satus_id;
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

        private void Client_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
