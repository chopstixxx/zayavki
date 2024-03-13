using Microsoft.VisualBasic.Logging;
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
using System.Xml.Linq;
using zayavki;

namespace zayavki
{
    public partial class One_request_form : Form
    {
        Requests request;

        public One_request_form(Requests request_)
        {
            InitializeComponent();
            request = request_;
        }

        private void One_request_form_Load(object sender, EventArgs e)
        {
            name_.Text = request.Equipment;
            type_.Text = request.Malf_type;
            desc_.Text = request.Malf_description;
            Get_all_masters();

            comboBox1.SelectedItem = Is_master_exist();


        }
        public void Get_all_masters()
        {
            DB db = new DB();
            db.Open_conn();
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT full_name FROM master", db.Get_Conn()))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(reader.GetOrdinal("full_name")));

                    }

                }

            }
        }
        public string Is_master_exist()
        {
            DB db = new DB();
            db.Open_conn();
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT full_name FROM master WHERE id=@id", db.Get_Conn()))
            {
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = request.Master_id;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string master = reader.GetString(reader.GetOrdinal("full_name"));
                        return master;
                    }
                    return "";
                }

            }
        }
        private void Request_create_Click(object sender, EventArgs e)
        {
            string _name = name_.Text;
            string _type = type_.Text;
            string _desc = desc_.Text;
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("Введите оборудование!");
                return;
            }

            if (string.IsNullOrEmpty(_type))
            {
                MessageBox.Show("Введите тип!");
                return;
            }
            if (string.IsNullOrEmpty(_desc))
            {
                MessageBox.Show("Введите описание!");
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Назначьте исполнителя!");
                return;
            }
            if (request.Satus_id == 3)
            {
                MessageBox.Show("Заявка уже выполнена!");
                return;
            }


            DB dB = new DB();
            int master_id = Get_master_id(comboBox1.SelectedItem.ToString());
            dB.Request_update(request.Id, master_id, _name, _type, _desc);
        }

        public int Get_master_id(string fullname)
        {
            DB db = new DB();
            db.Open_conn();
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT id FROM master WHERE full_name=@full_name", db.Get_Conn()))
            {
                cmd.Parameters.Add("@full_name", NpgsqlDbType.Varchar).Value = fullname;
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        return id;
                    }
                    return -1;
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Manager_form form = new Manager_form();
            this.Hide();
            form.Show();
        }

        private void One_request_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
