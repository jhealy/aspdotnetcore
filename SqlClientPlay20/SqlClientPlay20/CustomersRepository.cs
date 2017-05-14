using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public static class CustomersRepository
{
    public static List<Customer> GetAllCustomers()
    {
        List<Customer> retList = new List<Customer>();
        using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
        {
            SqlDataReader dr = SQLHelper.ExecuteReader(conn, System.Data.CommandType.Text, 
                @"SELECT TOP(10)[CustomerID],[CompanyName] FROM[northwind].[dbo].[Customers]", null);
            while ( dr.Read() )
            {
                Customer c = new Customer();
                c.CustomerId = System.Convert.ToString(dr[0]);
                c.CompanyName = System.Convert.ToString(dr[1]);
                retList.Add(c);
            }
        }
        return retList;
    }

    public static List<Customer> GetAllCustomersFromDataTable()
    {
        List<Customer> retList = new List<Customer>();
        using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
        {
            DataTable dt = SQLHelper.ExecuteDataTableSqlDA(conn, System.Data.CommandType.Text,
                @"SELECT TOP(10)[CustomerID],[CompanyName] FROM[northwind].[dbo].[Customers] ORDER BY CustomerId DESC", null);
            foreach (DataRow dr in dt.Rows)
            {
                Customer c = new Customer();
                c.CustomerId = System.Convert.ToString(dr[0]);
                c.CompanyName = System.Convert.ToString(dr[1]);
                retList.Add(c);
            }
        }
        return retList;
    }
}
