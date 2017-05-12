using System.Data.SqlClient;

namespace Services
{
	public class Connection
	{
		private SqlConnection _sqlConn;
		public SqlConnection SqlConn
		{
			get
			{
				if (_sqlConn == null)
				{
					_sqlConn = OpenConnection();
				}
				return _sqlConn;
			}
		}

		public SqlConnection OpenConnection()
		{
			_sqlConn = new SqlConnection(@"Data Source=serve2894.database.windows.net;Initial Catalog=Urls;Integrated Security=False;User ID=jing89ming;Password=abc123$%^;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			_sqlConn.Open();

			return _sqlConn;
		}

		public void CloseConnection()
		{
			_sqlConn.Close();
		}
	}
}

