﻿namespace AutoPatcher_csharkpy
{
    partial class GTAPATCHERSPEEDRUN
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
            this.GTAPATH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.RestoreBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GTAPATH
            // 
            this.GTAPATH.Location = new System.Drawing.Point(12, 175);
            this.GTAPATH.Name = "GTAPATH";
            this.GTAPATH.Size = new System.Drawing.Size(282, 20);
            this.GTAPATH.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "GTA Path: Please Don\'t change it unless its wrong";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start Autopatcher";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RestoreBackup
            // 
            this.RestoreBackup.Location = new System.Drawing.Point(188, 12);
            this.RestoreBackup.Name = "RestoreBackup";
            this.RestoreBackup.Size = new System.Drawing.Size(107, 23);
            this.RestoreBackup.TabIndex = 3;
            this.RestoreBackup.Text = "Restore Backup";
            this.RestoreBackup.UseVisualStyleBackColor = true;
            this.RestoreBackup.Click += new System.EventHandler(this.RestoreBackup_Click);
            // 
            // GTAPATCHERSPEEDRUN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 202);
            this.Controls.Add(this.RestoreBackup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GTAPATH);
            this.Name = "GTAPATCHERSPEEDRUN";
            this.Text = "GTA-AutoPatcher-Speedrun";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GTAPATH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button RestoreBackup;
    }
}

