using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace restoran
{
    class Database
    {
        private SqlConnection conn;
        private string query;

   
        public Database()
        {
            try
            {
                this.conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=restoran;Trusted_Connection=True");
                this.conn.Open();

            }catch(SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public string getQuery()
        {
            return query;
        }
        public void setQuery(string query)
        {
            this.query = query;
        }

        public bool execute()
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                return true;
                
            }catch(SqlException e)
            {
                return false;
            }
        }

        public SqlDataAdapter executeWithData()
        {
            return new SqlDataAdapter(this.query, this.conn);
        }
    }
}
