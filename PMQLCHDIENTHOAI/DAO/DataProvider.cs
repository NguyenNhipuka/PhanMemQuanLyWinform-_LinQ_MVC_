using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DAO
{
    public class DataProvider
    {
        //private static SqlConnection _connection = null;
        private static string _connectionStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static DataProvider instance; 

        public static DataProvider Instance
        {
            get {
                if (instance == null)
                    instance = new DataProvider();
                return DataProvider.instance;
            }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        public string getConnectstring()
        {
            return _connectionStr;
        }
        public int check_Config()
        {
            if (_connectionStr == string.Empty)
                return 1;//chuoi cau hinh khong ton tai
            SqlConnection connection = new SqlConnection(_connectionStr);
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return 0;// ket noi thanh cong, chuoi cau hinh hop le
            }
            catch {
                return 2;//chuoi cau hinh khong phu hop
            }
        }
        //public DataProvider()
        //{
        //    var connStr = ConfigurationManager.AppSettings["ConnectionString"];
        //    _connectionStr = new SqlConnection(connStr);
        //}
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }


        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = null;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
