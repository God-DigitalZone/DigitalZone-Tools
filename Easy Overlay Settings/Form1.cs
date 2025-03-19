using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Easy_Overlay_Settings
{
    public partial class Form1 : Form
    {
        private string overlayConfigFilePath = @".\DigitalZone\steam_settings\configs.overlay.ini";
        private CheckBox overlayCheckBox;
        private Label messageLabel;
        private Label poweredByLabel;
        private PictureBox githubPictureBox;
        private Label githubLabel;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Easy Overlay Settings"; // Change the title of the form
            ApplyDarkMode();
            CreateUI();

            // Call to Resize method to force centering of elements
            this.OnResize(EventArgs.Empty);

            // Load the current value of the overlay config.
            this.Load += (sender, e) => LoadOverlayConfig();
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(45, 45, 48); // Dark background
            this.ForeColor = Color.White; // White background

            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = Color.FromArgb(45, 45, 48); // Dark background for controls
                ctrl.ForeColor = Color.White; // White text for controls
            }
        }

        private void UpdateOverlayConfig()
        {
            if (!File.Exists(overlayConfigFilePath))
            {
                MessageBox.Show($"Config file not found: {overlayConfigFilePath}");
                return;
            }

            var lines = new List<string>(File.ReadAllLines(overlayConfigFilePath));
            bool overlayUpdated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("enable_experimental_overlay="))
                {
                    lines[i] = $"enable_experimental_overlay={(overlayCheckBox.Checked ? 1 : 0)}";
                    overlayUpdated = true;
                    break;
                }
            }

            if (overlayUpdated)
            {
                File.WriteAllLines(overlayConfigFilePath, lines);
                messageLabel.Text = overlayCheckBox.Checked ? "Overlay enabled!" : "Overlay disabled!";
                messageLabel.ForeColor = Color.LightGreen;
            }
            else
            {
                messageLabel.Text = "Overlay setting not found in config file.";
                messageLabel.ForeColor = Color.Red;
            }

            // Center the message
            messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
            poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;
            githubPictureBox.Left = (this.ClientSize.Width - githubPictureBox.Width) / 2;
            githubLabel.Left = (this.ClientSize.Width - githubLabel.Width) / 2;
        }

        private void LoadOverlayConfig()
        {
            if (!File.Exists(overlayConfigFilePath))
            {
                return;
            }

            var lines = File.ReadAllLines(overlayConfigFilePath);

            foreach (var line in lines)
            {
                if (line.StartsWith("enable_experimental_overlay="))
                {
                    var value = line.Split('=')[1].Trim();
                    overlayCheckBox.Checked = value == "1";
                    break;
                }
            }
        }

        private void CreateUI()
        {
            overlayCheckBox = new CheckBox { Text = "Enable Overlay", AutoSize = true, ForeColor = Color.White };
            messageLabel = new Label { Text = "", Font = new Font("Arial", 10, FontStyle.Italic), AutoSize = true };

            // Add the text "Powered by DigitalZone" below the message
            poweredByLabel = new Label
            {
                Text = "Powered by DigitalZone",
                Font = new Font("Arial", 14, FontStyle.Bold), // Same Font of "Enable Overlay"
                ForeColor = Color.White,
                AutoSize = true
            };

            // Upload GitHub image from embedded resources
            githubPictureBox = new PictureBox
            {
                Image = Easy_Overlay_Settings.Properties.Resources.github, // Use the full namespace name
                SizeMode = PictureBoxSizeMode.AutoSize,
                Cursor = Cursors.Hand // Sets the hand cursor when it passes over the icon
            };
            githubPictureBox.Click += (sender, e) => Process.Start("https://github.com/God-DigitalZone/DigitalZone-Tools");

            githubLabel = new Label
            {
                Text = "GitHub",
                Font = new Font("Arial", 14, FontStyle.Bold), // Same Font of "Powered by DigitalZone"
                ForeColor = Color.White,
                AutoSize = true
            };

            // Centrare gli elementi
            overlayCheckBox.Left = (this.ClientSize.Width - overlayCheckBox.Width) / 2;
            messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
            poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;

            overlayCheckBox.Top = 50;
            messageLabel.Top = 100;
            poweredByLabel.Top = 120;

            // Place the GitHub icon and label under the "Powered by DigitalZone"
            githubPictureBox.Top = poweredByLabel.Bottom + 10;
            githubPictureBox.Left = (this.ClientSize.Width - githubPictureBox.Width) / 2;
            githubLabel.Top = githubPictureBox.Bottom + 5;
            githubLabel.Left = (this.ClientSize.Width - githubLabel.Width) / 2;

            overlayCheckBox.CheckedChanged += (sender, e) => UpdateOverlayConfig();

            this.Controls.Add(overlayCheckBox);
            this.Controls.Add(messageLabel);
            this.Controls.Add(poweredByLabel);
            this.Controls.Add(githubPictureBox); // Add GitHub icon
            this.Controls.Add(githubLabel); // Add GitHub label

            // Centering elements at resizing
            this.Resize += (sender, e) => {
                overlayCheckBox.Left = (this.ClientSize.Width - overlayCheckBox.Width) / 2;
                messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
                poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;
                githubPictureBox.Left = (this.ClientSize.Width - githubPictureBox.Width) / 2;
                githubLabel.Left = (this.ClientSize.Width - githubLabel.Width) / 2;
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add the code that should be executed when the form is loaded
        }
    }
}
