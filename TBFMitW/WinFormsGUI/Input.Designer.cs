namespace WinFormsGUI
{
    partial class Input
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
            this.ResText = new System.Windows.Forms.TextBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.MsgText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ResText
            // 
            this.ResText.Location = new System.Drawing.Point(104, 145);
            this.ResText.Name = "ResText";
            this.ResText.Size = new System.Drawing.Size(547, 22);
            this.ResText.TabIndex = 1;
            // 
            // OKBtn
            // 
            this.OKBtn.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.OKBtn.Location = new System.Drawing.Point(325, 236);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(88, 53);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // MsgText
            // 
            this.MsgText.AutoSize = true;
            this.MsgText.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.MsgText.Location = new System.Drawing.Point(176, 64);
            this.MsgText.Name = "MsgText";
            this.MsgText.Size = new System.Drawing.Size(0, 29);
            this.MsgText.TabIndex = 3;
            this.MsgText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 353);
            this.Controls.Add(this.MsgText);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.ResText);
            this.Name = "Input";
            this.Text = "Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ResText;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Label MsgText;
    }
}