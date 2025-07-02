using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextWise_Compiler_Edition
{
    public static class database_Access
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\Downloads\Ankan 2\Ankan\TextWise-Compiler Edition\textwise.mdf"";Integrated Security=True;Connect Timeout=30";

        public static DataTable getData(string query, out string error)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                //command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet set = new DataSet();
                adapter.Fill(set);
                DataTable dt = set.Tables[0];

                connection.Close();
                error = "";
                return dt;

            }
            catch (Exception e)
            {
                error = e.Message;

                return null;
            }
        }
        public static void InsertData(string query, out string error)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                error = "";


            }
            catch (Exception e)
            {
                error = e.Message;
            }
        }
        public static DataTable getData(string query, SqlParameter[] parameters, out string error)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to the command
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet set = new DataSet();
                adapter.Fill(set);
                DataTable dt = set.Tables[0];

                connection.Close();
                error = "";
                return dt;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }


    }
}
