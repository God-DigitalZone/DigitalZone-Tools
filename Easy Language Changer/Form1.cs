﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Easy_Language_Changer
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> languageMap = new Dictionary<string, string>
        {
            {"Arabic", "arabic"}, {"Bulgarian", "bulgarian"}, {"Chinese (Simplified)", "schinese"},
            {"Chinese (Traditional)", "tchinese"}, {"Czech", "czech"}, {"Danish", "danish"},
            {"Dutch", "dutch"}, {"English", "english"}, {"Finnish", "finnish"}, {"French", "french"},
            {"German", "german"}, {"Greek", "greek"}, {"Hungarian", "hungarian"}, {"Indonesian", "indonesian"},
            {"Italian", "italian"}, {"Japanese", "japanese"}, {"Korean", "koreana"}, {"Norwegian", "norwegian"},
            {"Polish", "polish"}, {"Portuguese", "portuguese"}, {"Portuguese-Brazil", "brazilian"},
            {"Romanian", "romanian"}, {"Russian", "russian"}, {"Spanish-Spain", "spanish"},
            {"Spanish-Latin America", "latam"}, {"Swedish", "swedish"}, {"Thai", "thai"},
            {"Turkish", "turkish"}, {"Ukrainian", "ukrainian"}, {"Vietnamese", "vietnamese"}
        };

        private string iniFilePath = @".\DigitalZone\steam_settings\configs.user.ini";
        private string languagesFilePath = @".\DigitalZone\steam_settings\supported_languages.txt";
        private ComboBox comboBox;
        private Label messageLabel;
        private Label poweredByLabel;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Easy Language Changer"; // Change the title of the form
            ApplyDarkMode();
            LoadSupportedLanguages();
            CreateUI();

            // Call to Resize method to force centering of elements
            this.OnResize(EventArgs.Empty);

            // Set the default language after all controls have been added
            this.Load += (sender, e) => SetDefaultLanguage();
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

        private void LoadSupportedLanguages()
        {
            if (File.Exists(languagesFilePath))
            {
                var availableLanguages = new HashSet<string>(File.ReadAllLines(languagesFilePath));
                var keysToRemove = new List<string>();

                foreach (var key in languageMap.Keys)
                {
                    if (!availableLanguages.Contains(languageMap[key]))
                    {
                        keysToRemove.Add(key);
                    }
                }

                foreach (var key in keysToRemove)
                {
                    languageMap.Remove(key);
                }
            }
        }

        private void UpdateLanguage(string selectedLanguage)
        {
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show($"Config file not found: {iniFilePath}");
                return;
            }

            var selectedCode = languageMap[selectedLanguage];
            var lines = new List<string>(File.ReadAllLines(iniFilePath));
            bool languageUpdated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("language="))
                {
                    lines[i] = $"language={selectedCode}";
                    languageUpdated = true;
                    break;
                }
            }

            if (languageUpdated)
            {
                File.WriteAllLines(iniFilePath, lines);
                messageLabel.Text = $"Language changed to '{selectedLanguage}'!";
                messageLabel.ForeColor = Color.LightGreen;
            }
            else
            {
                messageLabel.Text = "Language setting not found in config file.";
                messageLabel.ForeColor = Color.Red;
            }

            // Centra il messaggio
            messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
            poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;
        }

        private void SetDefaultLanguage()
        {
            if (File.Exists(iniFilePath))
            {
                var lines = File.ReadAllLines(iniFilePath);
                foreach (var line in lines)
                {
                    if (line.StartsWith("language="))
                    {
                        var currentLanguageCode = line.Split('=')[1].Trim();
                        foreach (var pair in languageMap)
                        {
                            if (pair.Value == currentLanguageCode)
                            {
                                comboBox.SelectedItem = pair.Key;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void CreateUI()
        {
            var label = new Label { Text = "Select Game Language", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true };
            comboBox = new ComboBox { DataSource = new BindingSource(languageMap.Keys, null), DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
            var applyButton = new Button { Text = "Apply Language" };
            messageLabel = new Label { Text = "", Font = new Font("Arial", 10, FontStyle.Italic), AutoSize = true };

            // Add the words “Powered by DigitalZone” below the message
            poweredByLabel = new Label
            {
                Text = "Powered by DigitalZone",
                Font = new Font("Arial", 14, FontStyle.Bold), // Same Font of "Select Game Language"
                ForeColor = Color.White,
                AutoSize = true
            };

            // Add a clickable GitHub icon
            var githubIcon = new PictureBox
            {
                Image = Properties.Resources.github, // Use the embedded image
                Width = 32,
                Height = 32,
                Left = (this.ClientSize.Width - 32) / 2,
                Top = 300,
                Cursor = Cursors.Hand
            };
            githubIcon.Click += (sender, e) => System.Diagnostics.Process.Start("https://github.com/God-DigitalZone/DigitalZone-Tools");

            // Add a label for “GitHub”
            var githubLabel = new Label
            {
                Text = "GitHub",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Left = (this.ClientSize.Width - 50) / 2,
                Top = 340
            };

            // Centrare gli elementi
            label.Left = (this.ClientSize.Width - label.Width) / 2;
            comboBox.Left = (this.ClientSize.Width - comboBox.Width) / 2;
            applyButton.Left = (this.ClientSize.Width - applyButton.Width) / 2;
            messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
            poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;

            label.Top = 50;
            comboBox.Top = 100;
            applyButton.Top = 150;
            messageLabel.Top = 200;
            poweredByLabel.Top = 220; // Place the inscription below the message

            applyButton.Click += (sender, e) => UpdateLanguage(comboBox.SelectedItem.ToString());

            this.Controls.Add(label);
            this.Controls.Add(comboBox);
            this.Controls.Add(applyButton);
            this.Controls.Add(messageLabel);
            this.Controls.Add(poweredByLabel);
            this.Controls.Add(githubIcon);
            this.Controls.Add(githubLabel);

            // Centering elements at resizing
            this.Resize += (sender, e) =>
            {
                label.Left = (this.ClientSize.Width - label.Width) / 2;
                comboBox.Left = (this.ClientSize.Width - comboBox.Width) / 2;
                applyButton.Left = (this.ClientSize.Width - applyButton.Width) / 2;
                messageLabel.Left = (this.ClientSize.Width - messageLabel.Width) / 2;
                poweredByLabel.Left = (this.ClientSize.Width - poweredByLabel.Width) / 2;
                githubIcon.Left = (this.ClientSize.Width - githubIcon.Width) / 2;
                githubLabel.Left = (this.ClientSize.Width - githubLabel.Width) / 2;
            };
        }
    }
}
