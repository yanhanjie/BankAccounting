using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace ccslabsLogIn
{
    public class conn
    {
        SqlCeConnection mySqlConnection = null;

        public void Connection()
        {
            mySqlConnection = new SqlCeConnection(@"Data Source=C:\Users\YAM HON GIT\Documents\assignment2\C#\ccslabsLogIn\MyDatabase.sdf");

            mySqlConnection.Open();
        }



        public int update(string sql)
        {
            SqlCeCommand UpdateCommand = new SqlCeCommand(sql, mySqlConnection);

            UpdateCommand.CommandText = sql;

            UpdateCommand.CommandType = CommandType.Text;

            UpdateCommand.Connection = mySqlConnection;

            int x = UpdateCommand.ExecuteNonQuery();



            return x;
        }

        public SqlCeDataReader Select(string sql)
        {
            SqlCeCommand SelectCommand = new SqlCeCommand(sql, mySqlConnection);

            SqlCeDataReader SelectDataReader = SelectCommand.ExecuteReader();

            return SelectDataReader;
        } 


    }
}
