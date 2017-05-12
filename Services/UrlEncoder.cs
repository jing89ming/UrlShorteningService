using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Services
{
	public class UrlEncoder
	{
		private readonly SqlConnection _sqlConn;
		public UrlEncoder(SqlConnection sqlConn)
		{
			_sqlConn = sqlConn;
		}

		#region Public methods

		public string Encode(string inputUrl)
		{
			if (inputUrl == null)
			{
				return "Please fill in url to encode!";
			}

			if (inputUrl.Contains("http"))
			{
				inputUrl = Regex.Replace(inputUrl, @"^(?:http(?:s)?://)?", string.Empty, RegexOptions.IgnoreCase);
			}

			if (inputUrl.Contains("/"))
			{
				string[] rootUrl = inputUrl.Split('/');
				return string.Format("{0}/{1}", rootUrl[0], StoreEncodedUrl(inputUrl));
			}
			else
			{
				return "Please fill in valid url to encode!";
			}
		}

		public string Decode(string inputUrl)
		{
			if (inputUrl == null)
			{
				return "Please fill in url to decode!";
			}

			return RetrieveDecodedUrl(inputUrl);
		}

		#endregion

		#region Prvate methods

		private string StoreEncodedUrl(string inputUrl)
		{
			string encodedUrl = string.Format("b123qtz{0}", GetCounter());

			SqlDataAdapter da = new SqlDataAdapter();
			da.InsertCommand = new SqlCommand("INSERT INTO UrlEncoder VALUES(@Encoded,@Url)", _sqlConn);
			da.InsertCommand.Parameters.Add("@Encoded", SqlDbType.VarChar).Value = encodedUrl;
			da.InsertCommand.Parameters.Add("@Url", SqlDbType.VarChar).Value = inputUrl;
			da.InsertCommand.ExecuteNonQuery();

			return encodedUrl;
		} 

		private string GetCounter()
		{
			SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UrlEncoder", _sqlConn);
			return command.ExecuteScalar().ToString();
		}

		private string RetrieveDecodedUrl(string inputUrl)
		{
			if (inputUrl.Contains("/"))
			{
				string[] rootUrl = inputUrl.Split('/');
				SqlCommand command =
					new SqlCommand(String.Format("SELECT Decoded FROM UrlEncoder WHERE Encoded='{0}'", rootUrl[1]), _sqlConn);

				object url = command.ExecuteScalar();

				if (url == null)
				{
					return "There is no decoded url found!";
				}
				return command.ExecuteScalar().ToString();
			}
			else
			{
				return "Please fill in valid url to decode!";
			}
		}

		#endregion

	}
}
