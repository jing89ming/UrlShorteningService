<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="UrlShortener.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="Styles/myStyles.css">

<head runat="server">
	<title>URL Shortener</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<h1 class="header">Encode your url here</h1>
			<input runat="server" class="input" id="urlInputForEncoder" />
			<br />
			<asp:Button runat="server" ID="btnGetEncoded"  CssClass="button" Text="Get Encoded" OnClick="GetEncoded_Click" />
			<br />
			<input runat="server" class="input" readonly="true" id="encodeInput" />

			<h1 class="header">Decode your url here</h1>
			<input runat="server" class="input" id="urlInputForDecoder" />
			<br />
			<asp:Button runat="server" ID="btnGetDecoded" CssClass="button" Text="Get Decoded" OnClick="GetDecoded_Click" />
			<br />
			<input runat="server" class="input" readonly="true" id="decodeInput" />
		</div>
	</form>
</body>
</html>
