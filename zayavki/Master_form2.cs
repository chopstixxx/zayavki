using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace zayavki
{
    public partial class Master_form2 : Form
    {
        string login;
        Requests request;
        private readonly List<ListViewItem> allItems = new List<ListViewItem>();
        public Master_form2(string login_, Requests request_)
        {
            InitializeComponent();
            login = login_;
            request = request_;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Master_form form = new Master_form(login);
            this.Hide();
            form.Show();
        }

        private void Request_create_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1)
            {
                MessageBox.Show("Добавьте ресурсы!");
                return;
            }


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dest = saveFileDialog.FileName;

                using (var writer = new PdfWriter(dest))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Установка шрифта
                        PdfFont font = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\Arial.ttf", PdfEncodings.IDENTITY_H);

                        Paragraph title = new Paragraph("Отчёт по заявке №" + request.Id).SetTextAlignment(TextAlignment.CENTER).SetFont(font);
                        document.Add(title);

                        // Создание таблицы
                        Table table = new Table(3);
                        Cell[] headerCells = new Cell[] {
                    new Cell().Add(new Paragraph("Название").SetFont(font)),
                    new Cell().Add(new Paragraph("Кол-во").SetFont(font)),
                    new Cell().Add(new Paragraph("Цена").SetFont(font)),
                };

                        foreach (Cell cell in headerCells)
                        {
                            table.AddHeaderCell(cell);
                        }

                        // Заполнение таблицы данными из ListView
                        foreach (ListViewItem item in listView1.Items)
                        {
                            table.AddCell(new Cell().Add(new Paragraph(item.SubItems[0].Text).SetFont(font)));
                            table.AddCell(new Cell().Add(new Paragraph(item.SubItems[1].Text).SetFont(font)));
                            table.AddCell(new Cell().Add(new Paragraph(item.SubItems[2].Text).SetFont(font)));

                        }

                        // Добавление таблицы и итоговой суммы
                        document.Add(table);

                        // Рассчитываем итоговую сумму

                        document.Add(new Paragraph("Комментарий исполнителя: " + textBox1.Text).SetFont(font));
                    }
                }

                MessageBox.Show("Заявка успешно выполнена!");
            }
            DB db = new DB();
            db.Request_finsh_status_upd(request.Id);


        }




        private void Master_form2_Load(object sender, EventArgs e)
        {

            name_.Text = request.Equipment;
            type_.Text = request.Malf_type;
            desc_.Text = request.Malf_description;
        }

        private void Master_form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ListViewItem newItem = new ListViewItem(new string[]
   {
        textBox2.Text,
        textBox3.Text,
        textBox4.Text
   });

            listView1.Items.Add(newItem);
            allItems.Add(newItem);
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
