using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SerialCommunication
{
    class Database
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=admin;Password=kjkskvak;Database=measured_value;");

        MainWindow _parentWindow;

        public Database(MainWindow parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public bool InsertOnTable(List<Data> data)
        {
            if (data.Count > 0)
            {
                try
                {
                    this.OpenConn();
                    int count = data.Count;
                    NpgsqlCommand countCommand = new NpgsqlCommand("SELECT Count(*) from measured_value", conn);
                    NpgsqlDataReader rowsCount = countCommand.ExecuteReader();


                    rowsCount.Read();
                    int rowCount = int.Parse(rowsCount[0].ToString());
                    rowsCount.Close();
                    Debug.WriteLine(rowCount);

                    //30758400 - Number of rows after 1 year with every 1 sec measure
                    if (rowCount > 250) {
                        NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE from measured_value where id in (SELECT id FROM measured_value ORDER BY time DESC LIMIT " + count + ")", conn);
                        Int32 rowsDeleted = deleteCommand.ExecuteNonQuery();
                        NpgsqlCommand vacuumCommand = new NpgsqlCommand("VACUUM FULL measured_value", conn);
                        vacuumCommand.ExecuteNonQuery();

                    }

                    string SQL = "INSERT INTO measured_value(time, probe_id, value) VALUES ";
                    for (int i = 0; i < count - 1; i++)
                    {
                        SQL += "('" + data[i].Date + "','" + data[i].ProbeID + "','" + data[i].Value.ToString("0.00000000000000000", System.Globalization.CultureInfo.InvariantCulture) + "'),";
                    }
                    SQL += "('" + data[count - 1].Date + "','" + data[count - 1].ProbeID + "','" + data[count - 1].Value.ToString("0.00000000000000000", System.Globalization.CultureInfo.InvariantCulture) + "');";
             
                    // Execute command
                    NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                    Int32 rowsaffected = command.ExecuteNonQuery();

                    this.CloseConn();


                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    _parentWindow.Log("Error on db insert!" + e.ToString(), Color.Red);
                    return false;
                }
            }
            return true;
        }


        public void OpenConn()
        {
            try
            {
                conn.Open();
            }
            catch (Exception exp)
            {
                _parentWindow.Log("Error on Database Connection open: " + exp.ToString(), Color.Red);
            }
        }

        public void CloseConn()
        {
            try
            {
                conn.Close();
            }
            catch (Exception  ex)
            {
                _parentWindow.Log("Error on Databse connection close: " + ex.ToString());
            }
        }


    }
}


