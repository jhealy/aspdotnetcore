using System;
using System.Data.SqlClient;
using System.Data;

public class SQLHelper
{
    private static string CONNECTION_STRING = @"Server=DESKTOP-PPD5M0F\SQLEXPRESS;Database=northwind;User Id=northwind_reader;Password=northwind_reader;";
    public static string ConnectionString { get { return CONNECTION_STRING; } }

    public static int ExecuteNonQuery(SqlConnection conn, string cmdText, SqlParameter[] cmdParms)
    {
        SqlCommand cmd = conn.CreateCommand();
        PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
    {
        SqlCommand cmd = conn.CreateCommand();
        using (conn)
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
    }

    public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
    {
        SqlCommand cmd = conn.CreateCommand();
        PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
        var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return rdr;
    }

    public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
    {
        SqlCommand cmd = conn.CreateCommand();
        PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
        object val = cmd.ExecuteScalar();
        cmd.Parameters.Clear();
        return val;
    }

    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
    {
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        if (trans != null)
        {
            cmd.Transaction = trans;
        }
        cmd.CommandType = cmdType;
        //attach the command parameters if they are provided
        if (commandParameters != null)
        {
            AttachParameters(cmd, commandParameters);
        }
    }
    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (SqlParameter p in commandParameters)
        {
            //check for derived output value with no value assigned
            if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
            {
                p.Value = DBNull.Value;
            }
            command.Parameters.Add(p);
        }
    }
}
