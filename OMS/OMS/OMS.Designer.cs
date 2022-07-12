namespace OMS
{
    partial class OMS
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerateOutput = new System.Windows.Forms.Button();
            this.txtExcelFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGenerateOutput
            // 
            this.btnGenerateOutput.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenerateOutput.Location = new System.Drawing.Point(304, 134);
            this.btnGenerateOutput.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnGenerateOutput.Name = "btnGenerateOutput";
            this.btnGenerateOutput.Size = new System.Drawing.Size(632, 46);
            this.btnGenerateOutput.TabIndex = 0;
            this.btnGenerateOutput.Text = "Generate OMS Output";
            this.btnGenerateOutput.UseVisualStyleBackColor = true;
            this.btnGenerateOutput.Click += new System.EventHandler(this.btnGenerateOutput_Click);
            // 
            // txtExcelFilePath
            // 
            this.txtExcelFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelFilePath.Location = new System.Drawing.Point(128, 50);
            this.txtExcelFilePath.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtExcelFilePath.Name = "txtExcelFilePath";
            this.txtExcelFilePath.Size = new System.Drawing.Size(1036, 39);
            this.txtExcelFilePath.TabIndex = 1;
            this.txtExcelFilePath.Text = "C:\\Users\\spa27\\Downloads\\dotNet Technical Interview Homework";
            // 
            // OMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 237);
            this.Controls.Add(this.txtExcelFilePath);
            this.Controls.Add(this.btnGenerateOutput);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "OMS";
            this.Text = "OMS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnGenerateOutput;
        private TextBox txtExcelFilePath;
    }
}