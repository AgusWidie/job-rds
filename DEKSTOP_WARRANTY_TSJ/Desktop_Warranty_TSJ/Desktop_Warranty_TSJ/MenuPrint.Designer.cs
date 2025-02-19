namespace Desktop_Warranty_TSJ
{
    partial class MenuPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrint));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Hapus = new System.Windows.Forms.Button();
            this.PrintData = new System.Windows.Forms.Button();
            this.SelectDate = new System.Windows.Forms.CheckBox();
            this.Cari = new System.Windows.Forms.Button();
            this.SerialCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ViewPrint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstViewSerialCode = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(323, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data Print Serial Number";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.Hapus);
            this.groupBox1.Controls.Add(this.PrintData);
            this.groupBox1.Controls.Add(this.SelectDate);
            this.groupBox1.Controls.Add(this.Cari);
            this.groupBox1.Controls.Add(this.SerialCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1001, 196);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cari Data";
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(865, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 121);
            this.button2.TabIndex = 17;
            this.button2.Text = "Template";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Hapus
            // 
            this.Hapus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Hapus.BackgroundImage")));
            this.Hapus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hapus.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hapus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Hapus.Location = new System.Drawing.Point(743, 66);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(116, 122);
            this.Hapus.TabIndex = 16;
            this.Hapus.Text = "Hapus";
            this.Hapus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // PrintData
            // 
            this.PrintData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PrintData.BackgroundImage")));
            this.PrintData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PrintData.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintData.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PrintData.Location = new System.Drawing.Point(621, 68);
            this.PrintData.Name = "PrintData";
            this.PrintData.Size = new System.Drawing.Size(116, 121);
            this.PrintData.TabIndex = 15;
            this.PrintData.Text = "Print";
            this.PrintData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.PrintData.UseVisualStyleBackColor = true;
            this.PrintData.Click += new System.EventHandler(this.PrintData_Click);
            // 
            // SelectDate
            // 
            this.SelectDate.AutoSize = true;
            this.SelectDate.Location = new System.Drawing.Point(14, 34);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.Size = new System.Drawing.Size(138, 24);
            this.SelectDate.TabIndex = 14;
            this.SelectDate.Text = "Pilih Tanggal";
            this.SelectDate.UseVisualStyleBackColor = true;
            this.SelectDate.Click += new System.EventHandler(this.SelectDate_Click);
            // 
            // Cari
            // 
            this.Cari.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cari.Image = ((System.Drawing.Image)(resources.GetObject("Cari.Image")));
            this.Cari.Location = new System.Drawing.Point(499, 67);
            this.Cari.Name = "Cari";
            this.Cari.Size = new System.Drawing.Size(116, 122);
            this.Cari.TabIndex = 8;
            this.Cari.Text = "Cari Data";
            this.Cari.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cari.UseVisualStyleBackColor = true;
            this.Cari.Click += new System.EventHandler(this.Cari_Click);
            // 
            // SerialCode
            // 
            this.SerialCode.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerialCode.Location = new System.Drawing.Point(172, 68);
            this.SerialCode.Name = "SerialCode";
            this.SerialCode.Size = new System.Drawing.Size(306, 29);
            this.SerialCode.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Serial Number";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(172, 142);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(306, 27);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sampai Tanggal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dari Tanggal";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(172, 105);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(306, 27);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // ViewPrint
            // 
            this.ViewPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ViewPrint.BackgroundImage")));
            this.ViewPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ViewPrint.Location = new System.Drawing.Point(754, 17);
            this.ViewPrint.Name = "ViewPrint";
            this.ViewPrint.Size = new System.Drawing.Size(96, 97);
            this.ViewPrint.TabIndex = 13;
            this.ViewPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ViewPrint.UseVisualStyleBackColor = true;
            this.ViewPrint.Visible = false;
            this.ViewPrint.Click += new System.EventHandler(this.ViewPrint_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstViewSerialCode);
            this.groupBox2.Controls.Add(this.ViewPrint);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 295);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1000, 467);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List Data";
            // 
            // lstViewSerialCode
            // 
            this.lstViewSerialCode.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstViewSerialCode.FullRowSelect = true;
            this.lstViewSerialCode.GridLines = true;
            this.lstViewSerialCode.HideSelection = false;
            this.lstViewSerialCode.Location = new System.Drawing.Point(13, 28);
            this.lstViewSerialCode.Name = "lstViewSerialCode";
            this.lstViewSerialCode.Size = new System.Drawing.Size(967, 419);
            this.lstViewSerialCode.TabIndex = 9;
            this.lstViewSerialCode.UseCompatibleStateImageBehavior = false;
            this.lstViewSerialCode.SelectedIndexChanged += new System.EventHandler(this.lstViewSerialCode_SelectedIndexChanged);
            // 
            // MenuPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1037, 778);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MenuPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrint";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuPrint_FormClosed);
            this.Load += new System.EventHandler(this.MenuPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SerialCode;
        private System.Windows.Forms.Button Cari;
        private System.Windows.Forms.Button ViewPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstViewSerialCode;
        private System.Windows.Forms.CheckBox SelectDate;
        private System.Windows.Forms.Button PrintData;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Button button2;
    }
}