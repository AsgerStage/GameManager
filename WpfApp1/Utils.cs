using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    class Utils
    {
        string connectionString;
      public SqlConnection connection;
        SqlDataReader reader;

        public Utils()
        {
            connectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.DatabaseConnectionString"].ConnectionString;

        }

        public Boolean hasDuplicates(string Path)
        {
            Boolean result;
            string query = "Select * FROM ProgramList WHERE Path = '"+Path+"'";
            Console.WriteLine(query);
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();


                reader = command.ExecuteReader();

              result= reader.HasRows;


            }

            return result;
        }


        public void deleteFromDB(string Path)
        {
            string query = "DELETE FROM ProgramList  WHERE Path='"+Path+"'";
            Console.WriteLine(query);
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }


        }

        public Boolean ImportToDB(string Name, string Path)
        {
            if (!hasDuplicates(Path))
            {
                string query = "INSERT INTO ProgramList (Name,Path) VALUES ('" + Name + "','" + Path + "');";
                Console.WriteLine(query);
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }

            return false;
        }

        public void PopulateList(ListView lv)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ProgramList", connection))
            {
                connection.Open();
                DataTable programTable = new DataTable();
                adapter.Fill(programTable);
                lv.ItemsSource = programTable.DefaultView;


            }

        }

    }
}
