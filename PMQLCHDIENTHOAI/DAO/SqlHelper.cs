using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public static class SqlHelper
    {
        #region CombineParameters

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        #endregion

        #region Connection String & Timeout

        public static string GetConnectionString(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            else
                return connectionString;
        }

        public static int ConnectionTimeout { get; set; }

        public static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }

        #endregion
        
        #region Query

        public static IEnumerable<dynamic> QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<dynamic> QuerySQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<T> QuerySQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static IEnumerable<object> QuerySP(Type type, string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<object> QuerySQL(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

     

    }
}
