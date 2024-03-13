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
    public partial class Request_form : Form
    {
        string login;
        int client_id;
        public Request_form(string login_, int client_id_)
        {
            InitializeComponent();
            login = login_;
            client_id = client_id_;
        }

        private void Request_form_Load(object sender, EventArgs e)
        {
           
        }

        private void Request_create_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите название!");
                return;
            }
            string type = textBox2.Text;
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Введите тип!");
                return;
            }
            string desc = textBox3.Text;
            if (string.IsNullOrEmpty(desc))
            {
                MessageBox.Show("Введите описание!");
                return;
            }
            DB dB = new DB();
            dB.Request_create(client_id, name, type, desc);
        }

        private void Request_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Client_form form = new Client_form(login);
            this.Hide();
            form.Show();
        }
    }
}
