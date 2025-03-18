using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Easy_Shortcut_Maker
{
    public partial class Form1 : Form
    {
        private TextBox txtGamePath;
        private Button btnGenerate;

        public Form1()
        {
            InitializeComponent(); // Questo metodo è definito in Form1.Designer.cs
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string gamePath = txtGamePath.Text;
            if (string.IsNullOrEmpty(gamePath))
            {
                MessageBox.Show("Inserisci il percorso del gioco.");
                return;
            }

            string shortcutExePath = Path.Combine(Application.StartupPath, "GameLauncher.exe");
            if (File.Exists(shortcutExePath))
            {
                File.Delete(shortcutExePath); // Sovrascrive se esiste già
            }

            string code = $@"
using System;
using System.Diagnostics;

class Program
{{
    static void Main()
    {{
        Process.Start(@""{gamePath.Replace(@"\", @"\\")}"");
    }}
}}";

            CompileExecutable(code, shortcutExePath);
            MessageBox.Show("Scorciatoia creata con successo!");
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
                    string errors = "Errori di compilazione:\n";
                    foreach (CompilerError error in results.Errors)
                    {
                        errors += $"- Linea {error.Line}: {error.ErrorText}\n";
                    }
                    MessageBox.Show(errors);
                }
            }
        }
    }
}