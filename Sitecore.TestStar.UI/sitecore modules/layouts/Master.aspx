<%@ Page language="c#" AutoEventWireup="true" 
	Inherits="Sitecore.TestStar.UI.layouts.Master" 
	CodeBehind="Master.aspx.cs" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Import Namespace="Sitecore.Data.Items" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>TestStar</title>
    <link href="/sitecore modules/web/teststar/css/style.css" rel="stylesheet" />
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
	<script src="//ajax.cdnjs.com/ajax/libs/json2/20110223/json2.js"></script>
	<script src="/sitecore modules/web/teststar/js/global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="innerwrapper">
            <header>
				<div class="logo">
					<a href="/" title="Sitecore TestStar"><span>Sitecore TestStar</span></a>                
				</div>
				<nav>
					<ul>
						<asp:Repeater ID="rptNav" runat="server">
							<ItemTemplate>
								<li>
									<%# GetLink((Item)Container.DataItem) %>
								</li>
							</ItemTemplate>
						</asp:Repeater>
					</ul>
				</nav>
			</header>
			<main>
				<div class="main-bg"></div>
				<h1><asp:Literal ID="ltlPageTitle" runat="server"></asp:Literal></h1>
				<div class="mainInner">
					<sc:Placeholder Key="main" runat="server"></sc:Placeholder>
				</div>
			</main>
			<footer><div class="preThin"><div class="preThick"></div>Sitecore TestStar <%= DateTime.Now.Year %></div></footer>
        </div>
    </div>
    </form>
    <div class="cover"></div>
</body>
</html>
