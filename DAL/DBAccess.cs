using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    [Serializable]
    public class SqlHelper
    {
        private string connectionStr;

        public SqlHelper()
        {
            connectionStr = string.Empty;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrWhiteSpace(commandText)) throw new ArgumentNullException(nameof(commandText));

            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("Transaction is no longer valid.", nameof(transaction));
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                bool mustCloseConnection = false;
                try
                {
                    PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                    return cmd.ExecuteNonQuery();
                }
                finally
                {
                    cmd.Parameters.Clear();
                    if (mustCloseConnection && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        #endregion

        #region ExecuteDataset

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlCommand cmd = new SqlCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                bool mustCloseConnection = false;
                try
                {
                    PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    if (mustCloseConnection && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                bool mustCloseConnection = false;
                try
                {
                    PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                    return cmd.ExecuteScalar();
                }
                finally
                {
                    cmd.Parameters.Clear();
                    if (mustCloseConnection && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        #endregion

        #region ExecuteReader

        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                cmd.Dispose();
                if (mustCloseConnection && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw;
            }
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return ExecuteReader(connection, null, commandType, commandText, commandParameters);
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }

        #endregion
    }
}