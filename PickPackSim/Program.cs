using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PickPackSim
{
    internal static class Program
    {
        /// <summary>
        /// Gets the connection string
        /// </summary>
        public static string ConnStr
        {
            get
            {
                Properties.Settings.Default.ConnStr = $"Data Source={ Properties.Settings.Default.sqlServer}" +
                    $";Initial Catalog={ Properties.Settings.Default.sqlDBName}" +
                    $";user id={ Properties.Settings.Default.sqlUsername}" +
                    $";password={ Properties.Settings.Default.sqlPassword}";
                Properties.Settings.Default.ConnStr.Replace(" ", "");
                Properties.Settings.Default.Save();
                return Properties.Settings.Default.ConnStr;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Simulate());
            TableForm sim = new TableForm();
            Application.Run(sim);
            sim = null;
        }
    }
}