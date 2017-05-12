using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System.Data.SqlClient;

namespace ServicesTest
{
	[TestClass]
	public class UrlEncoderTest
	{
		private SqlConnection _sqlConn;
		private UrlEncoder _encoder;

		[TestInitialize]
		public void TestInitialize()
		{
			Connection conn = new Connection();
			_sqlConn = conn.SqlConn;
			_encoder = new UrlEncoder(_sqlConn);
		}

		[TestMethod]
		public void EncodeUrl_WithEmptyString_ReturnMsg()
		{
			//Arrange
			string inputUrl = null;

			//Act
			string result = _encoder.Encode(inputUrl);

			//Assert
			Assert.AreEqual("Please fill in url to encode!", result);
		}

		[TestMethod]
		public void EncodeUrl_InvalidUrl_ReturnMsg()
		{
			//Arrange
			string inputUrl = "Invalid";

			//Act
			string result = _encoder.Encode(inputUrl);

			//Assert
			Assert.AreEqual("Please fill in valid url to encode!", result);
		}

		[TestMethod]
		public void EncodeUrl_WithoutHttp_ReturnEncodedUrl()
		{
			//Arrange
			string inputUrl = "www.abc.com/abcdefg1234/wer234/ffff=-12";

			//Act
			string result = _encoder.Encode(inputUrl);

			//Assert
			Assert.IsTrue(result.Contains("www.abc.com/b123qtz"));
		}

		[TestMethod]
		public void EncodeUrl_WithHttp_ReturnEncodedUrl()
		{
			//Arrange
			string inputUrl = "http://www.abc.com/abcdefg1234/wer234/ffff=-12";

			//Act
			string result = _encoder.Encode(inputUrl);

			//Assert
			Assert.IsTrue(result.Contains("www.abc.com/b123qtz"));
		}

		[TestMethod]
		public void EncodeUrl_WithHttps_ReturnEncodedUrl()
		{
			//Arrange
			string inputUrl = "https://www.abc.com/abcdefg1234/wer234/ffff=-12";

			//Act
			string result = _encoder.Encode(inputUrl);

			//Assert
			Assert.IsTrue(result.Contains("www.abc.com/b123qtz"));
		}

		[TestMethod]
		public void DecodeUrl_WithEmptyString_ReturnMsg()
		{
			//Arrange
			string inputUrl = null;

			//Act
			string result = _encoder.Decode(inputUrl);

			//Assert
			Assert.AreEqual("Please fill in url to decode!", result);
		}

		[TestMethod]
		public void DecodeUrl_InvalidUrl_ReturnMsg()
		{
			//Arrange
			string inputUrl = "Invalid";

			//Act
			string result = _encoder.Decode(inputUrl);

			//Assert
			Assert.AreEqual("Please fill in valid url to decode!", result);
		}

		[TestMethod]
		public void DecodeUrl_NoEncodedUrl_ReturnMsg()
		{
			//Arrange
			string inputUrl = "www.abc.com/Invalid";

			//Act
			string result = _encoder.Decode(inputUrl);

			//Assert
			Assert.AreEqual("There is no decoded url found!", result);
		}

		[TestMethod]
		public void DecodeUrl_HasEncodedUrl_ReturnDecodedUrl()
		{
			//Arrange
			string inputUrl = "www.abc.com/abcdefg1234/wer234/ffff=-12/qq";
			string encodedUrl = _encoder.Encode(inputUrl);

			//Act
			string result = _encoder.Decode(encodedUrl);

			//Assert
			Assert.AreEqual(inputUrl, result);
		}

	}
}
