using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace ccslabsLogIn
{
    public class Connection
    {
        SqlCeConnection mySqlConnection = null;

        public Connection()
        {
            if (mySqlConnection==null)
            {
                mySqlConnection = new SqlCeConnection(@"Data Source=G:\C#\ccslabsLogIn\MyDatabase.sdf");
                
                if (mySqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }
            }

        }

        

        public void mySqlConnectionClose()
        {
            if (mySqlConnection.State == ConnectionState.Open)
            {
                mySqlConnection.Close();
            }
        }

        public int update(string sql)
        {
            SqlCeCommand UpdateCommand = new SqlCeCommand(sql, mySqlConnection);

            UpdateCommand.CommandText = sql;

            UpdateCommand.CommandType = CommandType.Text;

            UpdateCommand.Connection = mySqlConnection;

            int x = UpdateCommand.ExecuteNonQuery();

            mySqlConnectionClose();

            return x;
        }

    }
}
