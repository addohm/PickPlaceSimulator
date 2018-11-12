using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PickPackSim
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();
        }
        public string FormSerialNumber
        {
            set
            {
                lbResults.Items.Add(value);
            }
        }
        public string FormLineNumber
        {
            set
            {
                lbResults.Items.Add(value);
            }
        }
        public string FormOpticPosition
        {
            set
            {
                lbResults.Items.Add(value);
            }
        }
        public string FormClamshellPosition
        {
            set
            {
                lbResults.Items.Add(value);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message =
                "Are you sure that you would like to close the form?";
            const string caption = "Form Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            }
        }

        public event EventHandler CloseEvent;
        public event EventHandler RunEvent;
        public event EventHandler ResetEvent;

        protected virtual void OnClose(EventArgs e)
        {
            EventHandler handler = CloseEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRun(EventArgs e)
        {
            EventHandler handler = RunEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnReset(EventArgs e)
        {
            EventHandler handler = ResetEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BtnRun_Click(object sender, EventArgs e)
        {

        }

        public void BtnReset_Click(object sender, EventArgs e)
        {
            // rt_sp_testAOFreset
        }
    }
}
