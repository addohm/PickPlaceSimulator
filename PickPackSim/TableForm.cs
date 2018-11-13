using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public List<string> FormData = new List<string>();
        string btnText = string.Empty;
        private object result;
        private int quantity = -1;
        public List<string> SerialList { get; set; }
        List<string> rejects = new List<string>() {
                "EOPJ7L150092",
                "EOSJ8V260015",
                "EOSJ61180059",
                "EOSJ61170040",
                "EOSJ5N230030",
                "EOSJ61130107"
        };
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateValues();
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

        public void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void BtnReset_Click(object sender, EventArgs e)
        {

            dbReset();
            // progress.Visible = true;
            // BtnReset.Enabled = false;
            // BtnReset.Text = "0 Percent Complete";
            // SpCaller.RunWorkerAsync();
        }

        private void SpCaller_DoWork(object sender, DoWorkEventArgs e)
        {
            var self = (BackgroundWorker)sender;

            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                conn.FireInfoMessageEventOnUserErrors = true;
                conn.Open();
                conn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "rt_sp_testAOFreset";
                    result = cmd.ExecuteNonQuery();
                }
            }
        }

        private void SpCaller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnReset.Text = btnText;
            UpdateValues();
            progress.Visible = false;
            BtnReset.Enabled = true;
        }

        private void SpCaller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var message = Convert.ToString(e.UserState);
            Debug.WriteLine(message);
            BtnReset.Text = message;

            if (message.EndsWith(" Percent Complete"))
            {
                int percent;
                if (int.TryParse(message.Split(' ')[0], out percent))
                    progress.Value = percent;
            }
        }

        public void BtnRun_Click(object sender, EventArgs e)
        {
            int inserted = 0;
            int results = 0;
            // object[,,,] results;
            string serial = string.Empty;
            string board = string.Empty;
            // Create a list of serials from the database
            List<string> serials = GetSerialList(Program.ConnStr);
            // Run the process for each serial
            Random rnd = new Random();
            SqlConnection conn = new SqlConnection(Program.ConnStr);
            SqlCommand cmd;
            conn.Open();

            if (quantity != 0)
            {
                cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                quantity = PackedQuantity(Program.ConnStr);
                inserted = InsertedQuantity(Program.ConnStr);
                results = ResultQuantity(Program.ConnStr);
                if (inserted > rnd.Next(1, 6))
                {
                    // Theres enough optics in the boards, create and process results
                    // First, select a serial to be picked from the boards
                    serial = SelectSerialToPick(Program.ConnStr);
                    // And select a board to insert the serial into
                    board = SelectBoardResult(Program.ConnStr);
                    // Then insert the serial into the board
                    InsertResult(Program.ConnStr, serial, board);
                    // Now select a serial to pack
                    serial = SelectSerialToPack(Program.ConnStr);
                    // Check to see if the object is contained within the rejected serials list
                    if (RejectList( Program.ConnStr, rejects, serial) != true)
                    {
                        SqlParameter param = new SqlParameter();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "rt_sp_aof_packOptic";
                        cmd.Parameters.AddWithValue("@serial", serial);
                        //cmd.Parameters.Add("@serial", SqlDbType.VarChar, 50).Value = serial;
                        param = new SqlParameter("@packStatus", SqlDbType.Bit);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@boxNumber", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@casePackPosition", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@opticPackPosition", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@boxID", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@boxQty", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("@lineNo", SqlDbType.VarChar, 50);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();

                        lvResults.Columns.Add("Serial Number", -2, HorizontalAlignment.Left);
                        lvResults.Columns.Add("Order Line Number", -2, HorizontalAlignment.Left);
                        lvResults.Columns.Add("Optic Position", -2, HorizontalAlignment.Center);
                        lvResults.Columns.Add("Case Position", -2, HorizontalAlignment.Center);

                        ListViewItem item = new ListViewItem(cmd.Parameters["@serial"].Value.ToString());
                        item.SubItems.Add(cmd.Parameters["@lineNo"].Value.ToString());
                        item.SubItems.Add(cmd.Parameters["@opticPackPosition"].Value.ToString());
                        item.SubItems.Add(cmd.Parameters["@casePackPosition"].Value.ToString());

                        lvResults.Items.Add(item);

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"delete from aof_optic_inserted
                                        where serial_number = '" + serial + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Add more optics to the boards
                    cmd.CommandText = @"insert into aof_optic_inserted
                                    (serial_number,
                                        board_number
                                    )
                                    values
                                    ( (
                                            select top 1 upper(ol.serial_number)
                                            from aof_order_optics as ol
                                                inner join aof_order_line_queue as lq
                                                    on ol.so_line_number = lq.so_line_number
                                                inner join form_factor as ff
                                                    on lq.form_factor_id = ff.form_factor_id
                                            where not exists (
                                                                select serial_number
                                                                from aof_optic_results as rl
                                                                where ol.serial_number = rl.serial_number
                                            )
                                                and not exists (
                                                                    select serial_number
                                                                    from aof_optic_inserted as oi
                                                                    where ol.serial_number = oi.serial_number
                                            )
                                                and ol.picked = 'False'
                                            order by abs(ol.so_line_number) asc,
                                                    ol.rack,
                                                    ol.tray,
                                                    ol.serial_number asc
                                    ),
                                        (
                                            select top 1 b.board_number
                                            from boards b
                                            where not exists (
                                                                select i.board_number
                                                                from aof_optic_inserted i
                                                                where b.board_number = i.board_number
                                            )
                                                and left(b.board_number, 2) = 'OF'
                                            order by newid()
                                        )
                                    );";
                    cmd.ExecuteNonQuery();
                }
            }
            cmd = null;
            conn.Close();
            conn = null;
            UpdateValues();
        }


        private void dbReset()
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "rt_sp_testAOFreset";
                    result = cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Gets the list of serials from AOF_ORDER_OPTICS
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        private List<string> GetSerialList(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "select serial_number from aof_order_optics";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<string> serials = new List<string>();
                        while (reader.Read())
                        {
                            serials.Add(reader.GetString(0));
                        }
                        return serials;
                    }
                }
            }
        }

        /// <summary>
        /// Update the displayed values
        /// </summary>
        private void UpdateValues()
        {
            lblRemaining.Text = PackedQuantity(Program.ConnStr).ToString();
            lblInserted.Text = InsertedQuantity(Program.ConnStr).ToString();
            lblResults.Text = ResultQuantity(Program.ConnStr).ToString();
        }

        /// <summary>
        /// Returns the quantity of optics packed
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public int PackedQuantity(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(serial_number) from aof_order_optics where packed = 0";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    lblRemaining.Text = result.ToString();
                    return (int)result;
                }
            }
        }

        /// <summary>
        /// Returns the quantity of optics inserted
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public int InsertedQuantity(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(serial_number) from aof_optic_inserted";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    return (int)result;
                }
            }
        }

        /// <summary>
        /// Returns the quantity of optics with results
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public int ResultQuantity(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(serial_number) from aof_optic_results";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    return (int)result;
                }
            }
        }

        /// <summary>
        /// Returns a serial to be used for inserting
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public string SelectSerialToPick(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select top 1 i.serial_number
                                            from aof_optic_inserted i
                                            where not exists(select r.serial_number
                                                                from aof_optic_results r
                                                                where i.serial_number = r.serial_number )
                                            order by newid()";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    return result.ToString();
                }
            }
        }

        /// <summary>
        /// Returns a serial to be used for packing
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public string SelectSerialToPack(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select top 1 r.serial_number
                                            from aof_optic_results r
                                                 join aof_order_optics oo
                                                     on r.serial_number = oo.serial_number
                                            where oo.packed = 0
                                            order by oo.so_line_number, 
                                                     newid()";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    return result.ToString();
                }
            }
        }

        /// <summary>
        /// Returns a board number to be used for inserting
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public string SelectBoardResult(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select top 1 i.board_number
                                            from aof_optic_inserted i
                                            where not exists(select r.serial_number
                                                                from aof_optic_results r
                                                                where i.serial_number = r.serial_number )
                                            order by newid()";
                    result = cmd.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    return result.ToString();
                }
            }
        }

        /// <summary>
        /// Inserts passing results for the selected serial
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="serial"></param>
        /// <param name="board"></param>
        public void InsertResult(string connStr, string serial, string board)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"insert into aof_optic_results
                    (serial_number, 
                        board_number, 
                        reject
                    )
                    values
                    ('" + serial + @"', 
                        '" + board + @"', 
                        0
                    )";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Inserts an optic into a board
        /// </summary>
        /// <param name="connStr"></param>
        public void InsertOptic(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    // Add more optics to the boards
                    cmd.CommandText = @"insert into aof_optic_inserted
                                    (serial_number,
                                        board_number
                                    )
                                    values
                                    ( (
                                            select top 1 upper(ol.serial_number)
                                            from aof_order_optics as ol
                                                inner join aof_order_line_queue as lq
                                                    on ol.so_line_number = lq.so_line_number
                                                inner join form_factor as ff
                                                    on lq.form_factor_id = ff.form_factor_id
                                            where not exists (
                                                                select serial_number
                                                                from aof_optic_results as rl
                                                                where ol.serial_number = rl.serial_number
                                            )
                                                and not exists (
                                                                    select serial_number
                                                                    from aof_optic_inserted as oi
                                                                    where ol.serial_number = oi.serial_number
                                            )
                                                and ol.picked = 'False'
                                            order by abs(ol.so_line_number) asc,
                                                    ol.rack,
                                                    ol.tray,
                                                    ol.serial_number asc
                                    ),
                                        (
                                            select top 1 b.board_number
                                            from boards b
                                            where not exists (
                                                                select i.board_number
                                                                from aof_optic_inserted i
                                                                where b.board_number = i.board_number
                                            )
                                                and left(b.board_number, 2) = 'OF'
                                            order by newid()
                                        )
                                    );";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Processes a reject if the serial is contained within the serial list provided
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool RejectList( string connStr, List<string> rejects, string serial)
        {
            if (rejects.Contains(serial))
            {
                using (SqlConnection conn = new SqlConnection(Program.ConnStr))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update aof_optic_results set reject = 1 where serial_number = '" + serial + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "delete from aof_optic_inserted where serial_number = '" + serial + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "delete from aof_order_optics where serial_number = '" + serial + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            else return false;
        }
    }
}
