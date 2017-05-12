using Services;
using System;

namespace UrlShortener
{
	public partial class main : System.Web.UI.Page
	{
		private Connection _conn;
		private Connection Conn
		{
			get
			{
				if (_conn == null)
				{
					_conn = new Connection();
				}
				return _conn;
			}
		}

		private UrlEncoder _encoder;
		private UrlEncoder Encoder
		{
			get
			{
				if (_encoder == null)
				{
					_encoder = new UrlEncoder(Conn.SqlConn);
				}
				return _encoder;
			}
		}

		protected void Page_OnUnload(object sender, EventArgs e)
		{
			Conn.CloseConnection();
		}

		protected void GetEncoded_Click(object sender, EventArgs e)
		{
			encodeInput.Value = Encoder.Encode(urlInputForEncoder.Value);
		}

		protected void GetDecoded_Click(object sender, EventArgs e)
		{
			decodeInput.Value = Encoder.Decode(urlInputForDecoder.Value);
		}
	}
}