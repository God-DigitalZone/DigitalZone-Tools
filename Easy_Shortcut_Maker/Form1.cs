using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Drawing;

namespace Easy_Shortcut_Maker
{
    public partial class Form1 : Form
    {
        private bool isDarkMode = true; // Dark Mode attivo di default

        public Form1()
        {
            InitializeComponent();
            ApplyDarkMode(isDarkMode);
        }

        private string GetRelativePath(string fullPath, string basePath)
        {
            if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                basePath += Path.DirectorySeparatorChar;

            Uri fullUri = new Uri(fullPath);
            Uri baseUri = new Uri(basePath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString())
                .Replace('/', Path.DirectorySeparatorChar);

            if (!relativePath.StartsWith("."))
                relativePath = ".\\" + relativePath;

            return relativePath;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string gamePath = txtGamePath.Text.Trim();
            if (string.IsNullOrEmpty(gamePath))
            {
                MessageBox.Show("Please enter the game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(gamePath))
            {
                MessageBox.Show("Game executable not found at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string shortcutExePath = Path.Combine(Application.StartupPath, "GameLauncher.exe");

            try
            {
                if (File.Exists(shortcutExePath))
                    File.Delete(shortcutExePath);

                string arguments = txtArguments.Text;
                string relativePath = GetRelativePath(gamePath, Application.StartupPath);

                // Codice generato con escaping corretto
                string code = @"using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        try
        {
            string exePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), " +
                        $"@\"{relativePath.Replace("\\", "\\\\")}\");" + @"
            
            if (!File.Exists(exePath))
            {
                MessageBox.Show(@""Game not found at: "" + exePath + ""\nPlease place the shortcut in the correct folder."", 
                              ""Error"", 
                              MessageBoxButtons.OK, 
                              MessageBoxIcon.Error);
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = " + $"@\"{arguments.Replace("\"", "\"\"")}\"," + @"
                WorkingDirectory = Path.GetDirectoryName(exePath),
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(@""Error launching game: "" + ex.Message + ""\nMake sure the game files are in the correct location."", 
                          ""Error"", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Error);
        }
    }
}";

                CompileExecutable(code, shortcutExePath);
                MessageBox.Show($"Shortcut created successfully!\n\nIt will look for the game at:\n{relativePath}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompileExecutable(string code, string outputPath)
        {
            using (CSharpCodeProvider provider = new CSharpCodeProvider())
            {
                CompilerParameters parameters = new CompilerParameters
                {
                    GenerateExecutable = true,
                    OutputAssembly = outputPath,
                    ReferencedAssemblies = {
                        "System.dll",
                        "System.Windows.Forms.dll"
                    },
                    CompilerOptions = "/optimize"
                };

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    string errorMessage = "Compilation failed:\n";
                    foreach (CompilerError error in results.Errors)
                        errorMessage += $"- Line {error.Line}: {error.ErrorText}\n";

                    MessageBox.Show(errorMessage, "Compilation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ApplyDarkMode(bool darkMode)
        {
            Color backColor = darkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color foreColor = darkMode ? Color.White : SystemColors.ControlText;
            Color controlBackColor = darkMode ? Color.FromArgb(50, 50, 50) : SystemColors.Window;
            Color buttonBackColor = darkMode ? Color.FromArgb(70, 70, 70) : SystemColors.Control;

            this.BackColor = backColor;
            this.ForeColor = foreColor;

            foreach (Control control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = controlBackColor;
                    textBox.ForeColor = foreColor;
                    textBox.BorderStyle = darkMode ? BorderStyle.FixedSingle : BorderStyle.Fixed3D;
                }
                else if (control is Button button)
                {
                    button.BackColor = buttonBackColor;
                    button.ForeColor = foreColor;
                    button.FlatStyle = darkMode ? FlatStyle.Flat : FlatStyle.Standard;
                }
                else if (control is Label label)
                {
                    label.ForeColor = foreColor;
                }
            }
        }

        private void BtnToggleDarkMode_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ApplyDarkMode(isDarkMode);
        }

        private void BtnBrowseGame_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Game Executable";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    txtGamePath.Text = openFileDialog.FileName;
            }
        }
    }
}