namespace Tanks
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainDrawContext = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.playerHPBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerPointsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainDrawContext)).BeginInit();
            this.SuspendLayout();
            // 
            // mainDrawContext
            // 
            this.mainDrawContext.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mainDrawContext.Location = new System.Drawing.Point(172, 12);
            this.mainDrawContext.Name = "mainDrawContext";
            this.mainDrawContext.Size = new System.Drawing.Size(460, 374);
            this.mainDrawContext.TabIndex = 0;
            this.mainDrawContext.TabStop = false;
            this.mainDrawContext.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // playerHPBar
            // 
            this.playerHPBar.ForeColor = System.Drawing.Color.Red;
            this.playerHPBar.Location = new System.Drawing.Point(66, 12);
            this.playerHPBar.Name = "playerHPBar";
            this.playerHPBar.Size = new System.Drawing.Size(100, 13);
            this.playerHPBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.playerHPBar.TabIndex = 1;
            this.playerHPBar.Value = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "HP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Points";
            // 
            // playerPointsLabel
            // 
            this.playerPointsLabel.AutoSize = true;
            this.playerPointsLabel.Location = new System.Drawing.Point(63, 46);
            this.playerPointsLabel.Name = "playerPointsLabel";
            this.playerPointsLabel.Size = new System.Drawing.Size(31, 13);
            this.playerPointsLabel.TabIndex = 4;
            this.playerPointsLabel.Text = "9999";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.playerPointsLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playerHPBar);
            this.Controls.Add(this.mainDrawContext);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mainDrawContext)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainDrawContext;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar playerHPBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label playerPointsLabel;
    }
}

