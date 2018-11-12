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

    //public partial class Simulation : TableForm
    //{
    //    public List<string> FormData = new List<string>();
    //    private object result;
    //    private int quantity = -1;

    //    /// <summary>
    //    /// Runs the simulation
    //    /// </summary>
    //    public void Simulate()
    //    {
    //        int inserted = 0;
    //        // object[,,,] results;
    //        string serial = string.Empty;
    //        // Create a list of serials from the database
    //        List<string> serials = GetSerialList(Program.ConnStr);
    //        List<string> rejects = new List<string>() {
    //            "EOPJ7L150092",
    //            "EOSJ8V260015",
    //            "EOSJ61180059",
    //            "EOSJ61170040",
    //            "EOSJ5N230030",
    //            "EOSJ61130107"
    //        };
    //        // Run the process for each serial
    //        Random rnd = new Random();
    //        SqlConnection conn = new SqlConnection(Program.ConnStr);
    //        SqlCommand cmd;
    //        conn.Open();

    //        while (quantity != 0)
    //        {
    //            cmd = new SqlCommand();
    //            cmd = conn.CreateCommand();
    //            int quantity = PackedQuantity(Program.ConnStr);
                        
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = "select count(serial_number) from aof_optic_inserted";
    //            result = cmd.ExecuteScalar();
    //            result = (result == DBNull.Value) ? null : result;
    //            inserted = (int)result;
    //            if (inserted > rnd.Next(1, 5))
    //            {
    //                // Theres enough optics in the boards, create and process results
    //                cmd.CommandText = @"select top 1 i.serial_number
    //                                        from aof_optic_inserted i
    //                                        where not exists(select r.serial_number
    //                                                            from aof_optic_results r
    //                                                            where i.serial_number = r.serial_number )
    //                                        order by newid()";
    //                result = cmd.ExecuteScalar();
    //                result = (result == DBNull.Value) ? null : result;
    //                serial = (string)result;

    //                cmd.CommandText = @"select top 1 i.board_number
    //                                        from aof_optic_inserted i
    //                                        where not exists(select r.serial_number
    //                                                            from aof_optic_results r
    //                                                            where i.serial_number = r.serial_number )
    //                                        order by newid()";
    //                result = cmd.ExecuteScalar();
    //                result = (result == DBNull.Value) ? null : result;
    //                string board = (string)result;

    //                cmd.CommandText = @"insert into aof_optic_results
    //                (serial_number, 
    //                    board_number, 
    //                    reject
    //                )
    //                values
    //                ('" + serial + @"', 
    //                    '" + board + @"', 
    //                    0
    //                )";
    //                result = cmd.ExecuteNonQuery();
    //                cmd.CommandText = @"select top 1 r.serial_number
    //                                        from aof_optic_results r
    //                                                join aof_order_optics oo
    //                                                    on r.serial_number = oo.serial_number
    //                                        where oo.packed = 0
    //                                        order by oo.so_line_number, 
    //                                                    newid()";
    //                result = cmd.ExecuteScalar();
    //                result = (result == DBNull.Value) ? null : result;
    //                serial = (string)result;

    //                if (rejects.Contains(serial))
    //                {
    //                    cmd.CommandText = "update aof_optic_results set reject = 1 where serial_number = '" + serial + "'";
    //                    cmd.CommandText = "delete from aof_optic_inserted where serial_number = '" + serial + "'";
    //                    cmd.CommandText = "delete from aof_order_optics where serial_number = '" + serial + "'";
    //                }
    //                else
    //                {
    //                    SqlParameter param = new SqlParameter();
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.CommandText = "rt_sp_aof_packOptic";
    //                    // cmd.Parameters.AddWithValue("@serial", serial);
    //                    cmd.Parameters.Add("@serial", SqlDbType.VarChar, 50).Value = serial;
    //                    param = new SqlParameter("@packStatus", SqlDbType.Bit);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@boxNumber", SqlDbType.Int);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@casePackPosition", SqlDbType.Int);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@opticPackPosition", SqlDbType.Int);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@boxID", SqlDbType.Int);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@boxQty", SqlDbType.Int);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    param = new SqlParameter("@lineNo", SqlDbType.VarChar, 50);
    //                    param.Direction = ParameterDirection.Output;
    //                    cmd.Parameters.Add(param);
    //                    cmd.ExecuteScalar();

    //                    FormData.Add(cmd.Parameters["@serial"].Value.ToString());
    //                    FormData.Add(cmd.Parameters["@lineNo"].Value.ToString());
    //                    FormData.Add(cmd.Parameters["@casePackPosition"].Value.ToString());
    //                    FormData.Add(cmd.Parameters["@opticPackPosition"].Value.ToString());
    //                }
    //            }
    //            else
    //            {
    //                // Add more optics to the boards
    //                cmd.CommandText = @"insert into aof_optic_inserted
    //                                (serial_number,
    //                                    board_number
    //                                )
    //                                values
    //                                ( (
    //                                        select top 1 upper(ol.serial_number)
    //                                        from aof_order_optics as ol
    //                                            inner join aof_order_line_queue as lq
    //                                                on ol.so_line_number = lq.so_line_number
    //                                            inner join form_factor as ff
    //                                                on lq.form_factor_id = ff.form_factor_id
    //                                        where not exists (
    //                                                            select serial_number
    //                                                            from aof_optic_results as rl
    //                                                            where ol.serial_number = rl.serial_number
    //                                        )
    //                                            and not exists (
    //                                                                select serial_number
    //                                                                from aof_optic_inserted as oi
    //                                                                where ol.serial_number = oi.serial_number
    //                                        )
    //                                            and ol.picked = 'False'
    //                                        order by abs(ol.so_line_number) asc,
    //                                                ol.rack,
    //                                                ol.tray,
    //                                                ol.serial_number asc
    //                                ),
    //                                    (
    //                                        select top 1 b.board_number
    //                                        from boards b
    //                                        where not exists (
    //                                                            select i.board_number
    //                                                            from aof_optic_inserted i
    //                                                            where b.board_number = i.board_number
    //                                        )
    //                                            and left(b.board_number, 2) = 'OF'
    //                                        order by newid()
    //                                    )
    //                                );";
    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //        cmd = null;
    //        conn.Close();
    //        conn = null;
    //    }

    //    /// <summary>
    //    /// Gets the list of serials from AOF_ORDER_OPTICS
    //    /// </summary>
    //    /// <param name="connStr"></param>
    //    /// <returns></returns>
    //    private List<string> GetSerialList(string connStr)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            using (SqlCommand cmd = conn.CreateCommand())
    //            {
    //                conn.Open();
    //                cmd.CommandText = "select serial_number from aof_order_optics";
    //                using (SqlDataReader reader = cmd.ExecuteReader())
    //                {
    //                    List<string> serials = new List<string>();
    //                    while (reader.Read())
    //                    {
    //                        serials.Add(reader.GetString(0));
    //                    }
    //                    return serials;
    //                }
    //            }
    //        }
    //    }

    //    public int PackedQuantity(string connStr)
    //    {
    //        using (SqlConnection conn = new SqlConnection(Program.ConnStr))
    //        {
    //            using (SqlCommand cmd = conn.CreateCommand())
    //            {
    //                conn.Open();
    //                cmd.CommandType = CommandType.Text;
    //                cmd.CommandText = "select count(serial_number) from aof_order_optics where packed = 0";
    //                result = cmd.ExecuteScalar();
    //                result = (result == DBNull.Value) ? null : result;
    //                return (int)result;
    //            }
    //        }
    //    }
    //}
}