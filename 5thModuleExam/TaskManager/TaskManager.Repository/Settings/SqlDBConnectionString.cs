using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Repository.Settings;

public class SqlDBConnectionString
{
	private string connectionString;

	public string ConnectionString
    {
		get { return connectionString; }
		set { connectionString = value; }
	}

	public SqlDBConnectionString(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
