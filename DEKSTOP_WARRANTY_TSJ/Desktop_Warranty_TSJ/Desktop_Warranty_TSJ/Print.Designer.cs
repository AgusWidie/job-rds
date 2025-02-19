namespace Desktop_Warranty_TSJ
{
    partial class Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Print));
            this.label4 = new System.Windows.Forms.Label();
            this.TotalPrint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProsesData = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PrintSerialNumber = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.TextBox();
            this.SourcePrinter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Total Print";
            // 
            // TotalPrint
            // 
            this.TotalPrint.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPrint.Location = new System.Drawing.Point(182, 41);
            this.TotalPrint.Name = "TotalPrint";
            this.TotalPrint.Size = new System.Drawing.Size(262, 29);
            this.TotalPrint.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "Proses Data";
            // 
            // ProsesData
            // 
            this.ProsesData.Enabled = false;
            this.ProsesData.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProsesData.Location = new System.Drawing.Point(182, 79);
            this.ProsesData.Name = "ProsesData";
            this.ProsesData.Size = new System.Drawing.Size(262, 29);
            this.ProsesData.TabIndex = 15;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(31, 168);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(739, 42);
            this.progressBar1.TabIndex = 16;
            // 
            // PrintSerialNumber
            // 
            this.PrintSerialNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PrintSerialNumber.BackgroundImage")));
            this.PrintSerialNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PrintSerialNumber.Location = new System.Drawing.Point(476, 38);
            this.PrintSerialNumber.Name = "PrintSerialNumber";
            this.PrintSerialNumber.Size = new System.Drawing.Size(89, 80);
            this.PrintSerialNumber.TabIndex = 17;
            this.PrintSerialNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.PrintSerialNumber.UseVisualStyleBackColor = true;
            this.PrintSerialNumber.Click += new System.EventHandler(this.PrintSerialNumber_Click);
            // 
            // Message
            // 
            this.Message.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Message.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.Location = new System.Drawing.Point(31, 221);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(739, 31);
            this.Message.TabIndex = 19;
            // 
            // SourcePrinter
            // 
            this.SourcePrinter.Enabled = false;
            this.SourcePrinter.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourcePrinter.Location = new System.Drawing.Point(182, 118);
            this.SourcePrinter.Name = "SourcePrinter";
            this.SourcePrinter.Size = new System.Drawing.Size(262, 29);
            this.SourcePrinter.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 22);
            this.label2.TabIndex = 20;
            this.label2.Text = "Source Printer";
            // 
            // Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 266);
            this.Controls.Add(this.SourcePrinter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.PrintSerialNumber);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ProsesData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TotalPrint);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.Name = "Print";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print";
            this.Load += new System.EventHandler(this.Print_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TotalPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProsesData;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button PrintSerialNumber;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.TextBox SourcePrinter;
        private System.Windows.Forms.Label label2;
    }
}