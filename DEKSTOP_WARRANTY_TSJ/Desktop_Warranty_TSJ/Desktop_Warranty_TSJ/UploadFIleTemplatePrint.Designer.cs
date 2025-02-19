namespace Desktop_Warranty_TSJ
{
    partial class UploadFileTemplatePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadFileTemplatePrint));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SelectFile = new System.Windows.Forms.Button();
            this.SaveFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TemplateName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(26, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(445, 27);
            this.textBox1.TabIndex = 0;
            // 
            // SelectFile
            // 
            this.SelectFile.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectFile.Location = new System.Drawing.Point(486, 52);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(101, 35);
            this.SelectFile.TabIndex = 1;
            this.SelectFile.Text = "Pilih File";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveFile.BackgroundImage")));
            this.SaveFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SaveFile.Font = new System.Drawing.Font("Rockwell", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveFile.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SaveFile.Location = new System.Drawing.Point(482, 130);
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(105, 107);
            this.SaveFile.TabIndex = 2;
            this.SaveFile.Text = "Simpan";
            this.SaveFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SaveFile.UseVisualStyleBackColor = true;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Upload File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nama Template";
            // 
            // TemplateName
            // 
            this.TemplateName.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateName.Location = new System.Drawing.Point(26, 130);
            this.TemplateName.Name = "TemplateName";
            this.TemplateName.Size = new System.Drawing.Size(445, 27);
            this.TemplateName.TabIndex = 5;
            // 
            // UploadFileTemplatePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(617, 249);
            this.Controls.Add(this.TemplateName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveFile);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.Name = "UploadFileTemplatePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload File TemplatePrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button SaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TemplateName;
    }
}