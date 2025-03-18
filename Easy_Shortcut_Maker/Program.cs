using System;
using System.Windows.Forms;

namespace Easy_Shortcut_Maker
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Deve corrispondere al namespace
        }
    }
}