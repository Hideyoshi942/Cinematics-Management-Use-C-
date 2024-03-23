using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace WPFModernVerticalMenu.DataProcessing
{
    internal class DataProcessing
    {
        string source = "Data Source = QuanLyRapPhim.db";
        SQLiteConnection connection = null;

        void OpenConnect()
        {
            connection = new SQLiteConnection(source);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        void CloseConect()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public DataTable ReadData(string sql)
        {
            OpenConnect();
            SQLiteDataAdapter db = new SQLiteDataAdapter(sql, connection);
            DataTable dbtable = new DataTable();
            db.Fill(dbtable);
            CloseConect();
            db.Dispose();
            return dbtable;
        }

        public List<Object> ReadColumnData(string sql)
        {
            List<Object> list = new List<Object>();
            OpenConnect();
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Object obj = reader.GetValue(0);
                list.Add(obj);
            }
            return list;
        }

        public void ChangeData(string sql)
        {
            OpenConnect();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            CloseConect();
        }

        public int Count(string sql)
        {
            OpenConnect();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = sql;
            cmd.Connection = connection;
            int rowcount = -1;
            rowcount = Convert.ToInt32(cmd.ExecuteScalar());
            CloseConect();
            return rowcount;
        }
    }
}
