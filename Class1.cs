using System;

public class Connector
{
	public Connector()
	{
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString =
        "Data Source=ServerName;" +
        "Initial Catalog=programDatabase;" +
        "Integrated Security=SSPI;";
        conn.Open();

    }
}
