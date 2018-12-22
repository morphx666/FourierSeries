namespace FourierSeries {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.LabelTerms = new System.Windows.Forms.Label();
            this.TextBoxTerms = new System.Windows.Forms.TextBox();
            this.ButtonApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxMultiplier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxFactor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LabelTerms
            // 
            this.LabelTerms.AutoSize = true;
            this.LabelTerms.Location = new System.Drawing.Point(12, 13);
            this.LabelTerms.Name = "LabelTerms";
            this.LabelTerms.Size = new System.Drawing.Size(48, 20);
            this.LabelTerms.TabIndex = 0;
            this.LabelTerms.Text = "Terms";
            // 
            // TextBoxTerms
            // 
            this.TextBoxTerms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.TextBoxTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxTerms.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxTerms.ForeColor = System.Drawing.Color.Gainsboro;
            this.TextBoxTerms.Location = new System.Drawing.Point(91, 12);
            this.TextBoxTerms.Name = "TextBoxTerms";
            this.TextBoxTerms.Size = new System.Drawing.Size(62, 26);
            this.TextBoxTerms.TabIndex = 1;
            this.TextBoxTerms.Text = "7";
            this.TextBoxTerms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ButtonApply
            // 
            this.ButtonApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ButtonApply.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ButtonApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.ButtonApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.ButtonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonApply.Location = new System.Drawing.Point(91, 108);
            this.ButtonApply.Name = "ButtonApply";
            this.ButtonApply.Size = new System.Drawing.Size(75, 31);
            this.ButtonApply.TabIndex = 2;
            this.ButtonApply.Text = "Apply";
            this.ButtonApply.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Multiplier";
            // 
            // TextBoxMultiplier
            // 
            this.TextBoxMultiplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.TextBoxMultiplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxMultiplier.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.TextBoxMultiplier.Location = new System.Drawing.Point(91, 44);
            this.TextBoxMultiplier.Name = "TextBoxMultiplier";
            this.TextBoxMultiplier.Size = new System.Drawing.Size(964, 26);
            this.TextBoxMultiplier.TabIndex = 1;
            this.TextBoxMultiplier.Text = "2 * i + 1";
            this.TextBoxMultiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Factor";
            // 
            // TextBoxFactor
            // 
            this.TextBoxFactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.TextBoxFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxFactor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxFactor.ForeColor = System.Drawing.Color.Gainsboro;
            this.TextBoxFactor.Location = new System.Drawing.Point(91, 76);
            this.TextBoxFactor.Name = "TextBoxFactor";
            this.TextBoxFactor.Size = new System.Drawing.Size(964, 26);
            this.TextBoxFactor.TabIndex = 1;
            this.TextBoxFactor.Text = "4 * (Diameter / 2) / (Multiplier * Pi)";
            this.TextBoxFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1067, 692);
            this.Controls.Add(this.ButtonApply);
            this.Controls.Add(this.TextBoxFactor);
            this.Controls.Add(this.TextBoxMultiplier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxTerms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelTerms);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fourier Series";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelTerms;
        private System.Windows.Forms.TextBox TextBoxTerms;
        private System.Windows.Forms.Button ButtonApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxMultiplier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxFactor;
    }
}

