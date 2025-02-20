namespace Desktop_Warranty_TSJ
{
    partial class TemplateDataPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateDataPrint));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SumberPrint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintDefault = new System.Windows.Forms.Button();
            this.Hapus = new System.Windows.Forms.Button();
            this.Upload = new System.Windows.Forms.Button();
            this.Cari = new System.Windows.Forms.Button();
            this.TemplateName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstViewTemplatePrint = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(356, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data Template Print";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SumberPrint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.PrintDefault);
            this.groupBox1.Controls.Add(this.Hapus);
            this.groupBox1.Controls.Add(this.Upload);
            this.groupBox1.Controls.Add(this.Cari);
            this.groupBox1.Controls.Add(this.TemplateName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1383, 186);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cari Data";
            // 
            // SumberPrint
            // 
            this.SumberPrint.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SumberPrint.Location = new System.Drawing.Point(172, 82);
            this.SumberPrint.Name = "SumberPrint";
            this.SumberPrint.Size = new System.Drawing.Size(236, 29);
            this.SumberPrint.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Sumber Print";
            // 
            // PrintDefault
            // 
            this.PrintDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PrintDefault.BackgroundImage")));
            this.PrintDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PrintDefault.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintDefault.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PrintDefault.Location = new System.Drawing.Point(802, 39);
            this.PrintDefault.Name = "PrintDefault";
            this.PrintDefault.Size = new System.Drawing.Size(116, 119);
            this.PrintDefault.TabIndex = 17;
            this.PrintDefault.Text = "Default";
            this.PrintDefault.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.PrintDefault.UseVisualStyleBackColor = true;
            this.PrintDefault.Click += new System.EventHandler(this.PrintDefault_Click);
            // 
            // Hapus
            // 
            this.Hapus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Hapus.BackgroundImage")));
            this.Hapus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hapus.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hapus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Hapus.Location = new System.Drawing.Point(558, 39);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(116, 119);
            this.Hapus.TabIndex = 16;
            this.Hapus.Text = "Hapus";
            this.Hapus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // Upload
            // 
            this.Upload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Upload.BackgroundImage")));
            this.Upload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Upload.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Upload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Upload.Location = new System.Drawing.Point(680, 39);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(116, 119);
            this.Upload.TabIndex = 15;
            this.Upload.Text = "Upload";
            this.Upload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // Cari
            // 
            this.Cari.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cari.Image = ((System.Drawing.Image)(resources.GetObject("Cari.Image")));
            this.Cari.Location = new System.Drawing.Point(436, 39);
            this.Cari.Name = "Cari";
            this.Cari.Size = new System.Drawing.Size(116, 119);
            this.Cari.TabIndex = 8;
            this.Cari.Text = "Cari Data";
            this.Cari.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cari.UseVisualStyleBackColor = true;
            this.Cari.Click += new System.EventHandler(this.Cari_Click);
            // 
            // TemplateName
            // 
            this.TemplateName.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateName.Location = new System.Drawing.Point(172, 39);
            this.TemplateName.Name = "TemplateName";
            this.TemplateName.Size = new System.Drawing.Size(236, 29);
            this.TemplateName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nama Template";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstViewTemplatePrint);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(27, 290);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(967, 446);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List Data";
            // 
            // lstViewTemplatePrint
            // 
            this.lstViewTemplatePrint.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstViewTemplatePrint.FullRowSelect = true;
            this.lstViewTemplatePrint.GridLines = true;
            this.lstViewTemplatePrint.HideSelection = false;
            this.lstViewTemplatePrint.Location = new System.Drawing.Point(16, 28);
            this.lstViewTemplatePrint.Name = "lstViewTemplatePrint";
            this.lstViewTemplatePrint.Size = new System.Drawing.Size(933, 401);
            this.lstViewTemplatePrint.TabIndex = 9;
            this.lstViewTemplatePrint.UseCompatibleStateImageBehavior = false;
            this.lstViewTemplatePrint.SelectedIndexChanged += new System.EventHandler(this.lstViewTemplatePrint_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1013, 303);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(397, 433);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // TemplateDataPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1433, 757);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "TemplateDataPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TemplatePrint";
            this.Load += new System.EventHandler(this.TemplatePrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.Button Cari;
        private System.Windows.Forms.TextBox TemplateName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstViewTemplatePrint;
        private System.Windows.Forms.Button PrintDefault;
        private System.Windows.Forms.TextBox SumberPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}