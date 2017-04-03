namespace BasicIRC
{
    partial class FormClient
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButtonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(810, 447);
            this.tabControl.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 478);
            this.textBox1.MaxLength = 200;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(716, 49);
            this.textBox1.TabIndex = 1;
            // 
            // ButtonSend
            // 
            this.ButtonSend.Location = new System.Drawing.Point(726, 491);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(75, 23);
            this.ButtonSend.TabIndex = 2;
            this.ButtonSend.Text = "Send";
            this.ButtonSend.UseVisualStyleBackColor = true;
            // 
            // FormClient
            // 
            this.AcceptButton = this.ButtonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 539);
            this.Controls.Add(this.ButtonSend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClient_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ButtonSend;
    }
}