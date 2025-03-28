﻿namespace Easy_Shortcut_Maker
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtGamePath = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnToggleDarkMode = new System.Windows.Forms.Button();
            this.btnBrowseGame = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtGamePath
            this.txtGamePath.Location = new System.Drawing.Point(12, 25);
            this.txtGamePath.Name = "txtGamePath";
            this.txtGamePath.Size = new System.Drawing.Size(200, 20);
            this.txtGamePath.TabIndex = 0;

            // btnGenerate
            this.btnGenerate.Location = new System.Drawing.Point(12, 116);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(260, 30);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate Shortcut";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);

            // txtArguments
            this.txtArguments.Location = new System.Drawing.Point(12, 69);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(260, 20);
            this.txtArguments.TabIndex = 1;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game executable:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Arguments (Optional):";

            // btnToggleDarkMode
            this.btnToggleDarkMode.Location = new System.Drawing.Point(147, 152);
            this.btnToggleDarkMode.Name = "btnToggleDarkMode";
            this.btnToggleDarkMode.Size = new System.Drawing.Size(125, 30);
            this.btnToggleDarkMode.TabIndex = 4;
            this.btnToggleDarkMode.Text = "Toggle Dark Mode";
            this.btnToggleDarkMode.UseVisualStyleBackColor = true;
            this.btnToggleDarkMode.Click += new System.EventHandler(this.BtnToggleDarkMode_Click);

            // btnBrowseGame
            this.btnBrowseGame.Location = new System.Drawing.Point(218, 25);
            this.btnBrowseGame.Name = "btnBrowseGame";
            this.btnBrowseGame.Size = new System.Drawing.Size(54, 20);
            this.btnBrowseGame.TabIndex = 3;
            this.btnBrowseGame.Text = "...";
            this.btnBrowseGame.UseVisualStyleBackColor = true;
            this.btnBrowseGame.Click += new System.EventHandler(this.BtnBrowseGame_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 194);
            this.Controls.Add(this.btnBrowseGame);
            this.Controls.Add(this.btnToggleDarkMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtGamePath);
            this.MinimumSize = new System.Drawing.Size(300, 230);
            this.Name = "Form1";
            this.Text = "Easy Shortcut Maker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtGamePath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnToggleDarkMode;
        private System.Windows.Forms.Button btnBrowseGame;
    }
}