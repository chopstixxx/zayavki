namespace zayavki
{
    partial class Master_form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            Request_create = new Button();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            desc_ = new TextBox();
            type_ = new TextBox();
            name_ = new TextBox();
            textBox1 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 431);
            button1.Name = "button1";
            button1.Size = new Size(178, 35);
            button1.TabIndex = 28;
            button1.Text = "Назад";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Request_create
            // 
            Request_create.Location = new Point(397, 428);
            Request_create.Name = "Request_create";
            Request_create.Size = new Size(444, 38);
            Request_create.TabIndex = 27;
            Request_create.Text = "Выполнить заявку и создать отчёт";
            Request_create.UseVisualStyleBackColor = true;
            Request_create.Click += Request_create_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(310, 9);
            label4.Name = "label4";
            label4.Size = new Size(232, 26);
            label4.TabIndex = 24;
            label4.Text = "Выполнение заявки";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(12, 135);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 23;
            label3.Text = "Описание";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(12, 91);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 22;
            label1.Text = "Тип неисправности";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(148, 15);
            label2.TabIndex = 21;
            label2.Text = "Название оборудования";
            // 
            // desc_
            // 
            desc_.Location = new Point(12, 153);
            desc_.Multiline = true;
            desc_.Name = "desc_";
            desc_.ReadOnly = true;
            desc_.Size = new Size(289, 88);
            desc_.TabIndex = 20;
            // 
            // type_
            // 
            type_.Location = new Point(12, 104);
            type_.Name = "type_";
            type_.ReadOnly = true;
            type_.Size = new Size(289, 23);
            type_.TabIndex = 19;
            // 
            // name_
            // 
            name_.Location = new Point(12, 65);
            name_.Name = "name_";
            name_.ReadOnly = true;
            name_.Size = new Size(289, 23);
            name_.TabIndex = 18;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(397, 366);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(444, 56);
            textBox1.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(397, 348);
            label5.Name = "label5";
            label5.Size = new Size(167, 15);
            label5.TabIndex = 30;
            label5.Text = "Комментарий исполнителя";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(397, 47);
            label6.Name = "label6";
            label6.Size = new Size(129, 15);
            label6.TabIndex = 33;
            label6.Text = "Затраченные ресуры";
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listView1.Location = new Point(397, 65);
            listView1.Name = "listView1";
            listView1.Size = new Size(444, 196);
            listView1.TabIndex = 34;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Название";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Количество";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Цена";
            columnHeader3.Width = 150;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(397, 281);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(190, 23);
            textBox2.TabIndex = 35;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(622, 281);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(95, 23);
            textBox3.TabIndex = 36;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(734, 281);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(107, 23);
            textBox4.TabIndex = 37;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(397, 264);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 38;
            label7.Text = "Название";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(622, 264);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 39;
            label8.Text = "Количество";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(734, 263);
            label9.Name = "label9";
            label9.Size = new Size(37, 15);
            label9.TabIndex = 40;
            label9.Text = "Цена";
            // 
            // button2
            // 
            button2.Location = new Point(397, 310);
            button2.Name = "button2";
            button2.Size = new Size(444, 22);
            button2.TabIndex = 41;
            button2.Text = "Добавить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // Master_form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 192, 192);
            ClientSize = new Size(853, 478);
            Controls.Add(button2);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(listView1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(Request_create);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(desc_);
            Controls.Add(type_);
            Controls.Add(name_);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Master_form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выполнение заявки";
            FormClosing += Master_form2_FormClosing;
            Load += Master_form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button Request_create;
        private Label label4;
        private Label label3;
        private Label label1;
        private Label label2;
        private TextBox desc_;
        private TextBox type_;
        private TextBox name_;
        private TextBox textBox1;
        private Label label5;
        private Label label6;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button button2;
    }
}