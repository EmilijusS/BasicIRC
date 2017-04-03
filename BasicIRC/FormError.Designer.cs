namespace BasicIRC
{
    partial class FormError
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
            this.LabelError = new System.Windows.Forms.Label();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelError
            // 
            this.LabelError.BackColor = System.Drawing.SystemColors.Control;
            this.LabelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelError.ForeColor = System.Drawing.Color.Red;
            this.LabelError.Location = new System.Drawing.Point(12, 9);
            this.LabelError.Name = "LabelError";
            this.LabelError.Size = new System.Drawing.Size(188, 68);
            this.LabelError.TabIndex = 0;
            this.LabelError.Text = "Error!";
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(52, 80);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(96, 38);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "Whatever";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // FormError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(212, 130);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.LabelError);
            this.Name = "FormError";
            this.Text = "Error";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelError;
        private System.Windows.Forms.Button ButtonClose;
    }
}