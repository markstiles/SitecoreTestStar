<%@ Page language="c#" AutoEventWireup="true" 
	Inherits="Sitecore.TestStar.Core.UI.layouts.Master" 
	CodeBehind="Master.aspx.cs" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Import Namespace="Sitecore.Data.Items" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TestStar</title>
    <link href="/sitecore modules/web/teststar/css/style.css" rel="stylesheet" />
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
	<script src="/sitecore modules/web/teststar/js/webtests.js"></script>
	<script src="/sitecore modules/web/teststar/js/unittests.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="innerwrapper">
            <div class="logo">
                <a href="/">
					<img src="/sitecore modules/web/teststar/images/teststar.png" />
				</a>
                <div class="logoTitle">Test Star<span>The most powerful Web and Unit tester in your star system</span></div>
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
            <sc:Placeholder Key="main" runat="server"></sc:Placeholder>
        </div>
    </div>
    </form>
    <div class="cover"></div>
</body>
</html>
