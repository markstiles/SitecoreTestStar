<%@ Page language="c#" AutoEventWireup="true" 
	Inherits="Sitecore.TestStar.UI.layouts.Master" 
	CodeBehind="Master.aspx.cs" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TestStar</title>
    <link href="/sitecore modules/teststar/css/style.css" rel="stylesheet" />
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
	<script src="/sitecore modules/teststar/js/manager.js"></script>
	<script src="/sitecore modules/teststar/js/webtests.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="innerwrapper">
            <div class="logo">
                <img src="/sitecore modules/teststar/images/teststar.png" />
                <div class="logoTitle">Test Star<span>The most powerful Web and Unit tester in your star system</span></div>
            </div>
            <nav>
                <ul>
                    <asp:Repeater ID="rptNav" runat="server">
                        <ItemTemplate>
                            <li>
                                <%# GetLink((KeyValuePair<string,string>)Container.DataItem) %>
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
