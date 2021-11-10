namespace Table_Start_Hands
{
    partial class Table_Start_Hands
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar_Start_Hands = new System.Windows.Forms.TrackBar();
            this.textBox_Start_Hands = new System.Windows.Forms.TextBox();
            this.label_Percent_Start_Hands = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_SAVE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Start_Hands)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_Start_Hands
            // 
            this.trackBar_Start_Hands.Location = new System.Drawing.Point(400, -5);
            this.trackBar_Start_Hands.Maximum = 0;
            this.trackBar_Start_Hands.Minimum = -169;
            this.trackBar_Start_Hands.Name = "trackBar_Start_Hands";
            this.trackBar_Start_Hands.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_Start_Hands.Size = new System.Drawing.Size(45, 415);
            this.trackBar_Start_Hands.TabIndex = 1;
            this.trackBar_Start_Hands.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Start_Hands.Scroll += new System.EventHandler(this.trackBar_Start_Hands_Scroll);
            // 
            // textBox_Start_Hands
            // 
            this.textBox_Start_Hands.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_Start_Hands.Location = new System.Drawing.Point(407, 400);
            this.textBox_Start_Hands.Name = "textBox_Start_Hands";
            this.textBox_Start_Hands.Size = new System.Drawing.Size(30, 20);
            this.textBox_Start_Hands.TabIndex = 2;
            this.textBox_Start_Hands.Text = "0";
            this.textBox_Start_Hands.TextChanged += new System.EventHandler(this.textBox_Start_Hands_TextChanged);
            // 
            // label_Percent_Start_Hands
            // 
            this.label_Percent_Start_Hands.AutoSize = true;
            this.label_Percent_Start_Hands.Location = new System.Drawing.Point(412, 420);
            this.label_Percent_Start_Hands.Margin = new System.Windows.Forms.Padding(0);
            this.label_Percent_Start_Hands.Name = "label_Percent_Start_Hands";
            this.label_Percent_Start_Hands.Size = new System.Drawing.Size(21, 13);
            this.label_Percent_Start_Hands.TabIndex = 3;
            this.label_Percent_Start_Hands.Text = "0%";
            this.label_Percent_Start_Hands.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Clear
            // 
            this.button_Clear.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Clear.Location = new System.Drawing.Point(90, 402);
            this.button_Clear.Margin = new System.Windows.Forms.Padding(0);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(60, 30);
            this.button_Clear.TabIndex = 7;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_SAVE
            // 
            this.button_SAVE.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SAVE.Location = new System.Drawing.Point(160, 402);
            this.button_SAVE.Margin = new System.Windows.Forms.Padding(0);
            this.button_SAVE.Name = "button_SAVE";
            this.button_SAVE.Size = new System.Drawing.Size(60, 30);
            this.button_SAVE.TabIndex = 8;
            this.button_SAVE.Text = "Save";
            this.button_SAVE.UseVisualStyleBackColor = true;
            this.button_SAVE.Click += new System.EventHandler(this.button_SAVE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "suited cards";
            // 
            // Table_Start_Hands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_SAVE);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.label_Percent_Start_Hands);
            this.Controls.Add(this.textBox_Start_Hands);
            this.Controls.Add(this.trackBar_Start_Hands);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Table_Start_Hands";
            this.Size = new System.Drawing.Size(443, 436);
            this.Load += new System.EventHandler(this.Table_Start_Hands_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Start_Hands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TrackBar trackBar_Start_Hands;
        private System.Windows.Forms.TextBox textBox_Start_Hands;
        private System.Windows.Forms.Label label_Percent_Start_Hands;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_SAVE;
        private System.Windows.Forms.Label label1;
    }
}
