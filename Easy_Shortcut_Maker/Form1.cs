using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Easy_Shortcut_Maker
{
    public partial class Form1 : Form
    {
        private bool isDarkMode = true; // Dark Mode on by default

        public Form1()
        {
            InitializeComponent();
            ApplyDarkMode(isDarkMode); // Applies Dark Mode at Startup
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string gamePath = txtGamePath.Text;
            if (string.IsNullOrEmpty(gamePath))
            {
                MessageBox.Show("Enter the path of the game.");
                return;
            }

            string shortcutExePath = Path.Combine(Application.StartupPath, "GameLauncher.exe");
            if (File.Exists(shortcutExePath))
            {
                File.Delete(shortcutExePath); // Overrides if it already exists
            }

            string arguments = txtArguments.Text;

            string code = $@"
using System;
using System.Diagnostics;

class Program
{{
    static void Main()
    {{
        Process.Start(@""{gamePath.Replace(@"\", @"\\")}"", @""{arguments.Replace("\"", "\"\"")}"");
    }}
}}";

            CompileExecutable(code, shortcutExePath);
            MessageBox.Show("Shortcut successfully created!");
        }

        private void CompileExecutable(string code, string outputPath)
        {
            using (CSharpCodeProvider provider = new CSharpCodeProvider())
            {
                CompilerParameters parameters = new CompilerParameters
                {
                    GenerateExecutable = true,
                    OutputAssembly = outputPath,
                    ReferencedAssemblies = { "System.dll" }
                };

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
                if (results.Errors.HasErrors)
                {
                    string errors = "Compilation errors:\n";
                    foreach (CompilerError error in results.Errors)
                    {
                        errors += $"- Line {error.Line}: {error.ErrorText}\n";
                    }
                    MessageBox.Show(errors);
                }
            }
        }

        private void ToggleDarkMode()
        {
            isDarkMode = !isDarkMode; // Reverse the state of Dark Mode
            ApplyDarkMode(isDarkMode); // Apply Theme
        }

        private void ApplyDarkMode(bool darkMode)
        {
            if (darkMode)
            {
                // Colors for Dark Mode
                this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                this.ForeColor = System.Drawing.Color.White;

                txtGamePath.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
                txtGamePath.ForeColor = System.Drawing.Color.White;

                txtArguments.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
                txtArguments.ForeColor = System.Drawing.Color.White;

                btnGenerate.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
                btnGenerate.ForeColor = System.Drawing.Color.White;

                label1.ForeColor = System.Drawing.Color.White;
                label2.ForeColor = System.Drawing.Color.White;

                btnToggleDarkMode.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
                btnToggleDarkMode.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                // Colors for Light Mode
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ForeColor = System.Drawing.SystemColors.ControlText;

                txtGamePath.BackColor = System.Drawing.SystemColors.Window;
                txtGamePath.ForeColor = System.Drawing.SystemColors.WindowText;

                txtArguments.BackColor = System.Drawing.SystemColors.Window;
                txtArguments.ForeColor = System.Drawing.SystemColors.WindowText;

                btnGenerate.BackColor = System.Drawing.SystemColors.Control;
                btnGenerate.ForeColor = System.Drawing.SystemColors.ControlText;

                label1.ForeColor = System.Drawing.SystemColors.ControlText;
                label2.ForeColor = System.Drawing.SystemColors.ControlText;

                btnToggleDarkMode.BackColor = System.Drawing.SystemColors.Control;
                btnToggleDarkMode.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void BtnToggleDarkMode_Click(object sender, EventArgs e)
        {
            ToggleDarkMode();
        }
    }
}