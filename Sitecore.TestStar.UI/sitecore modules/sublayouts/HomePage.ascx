<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="HomePage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.HomePage" %>
 <%@ Import Namespace="NUnit.Core" %>

<div class="home">
    <div class="whiteBox generate corners">
		<h3><asp:Literal ID="ltlUCount" runat="server"></asp:Literal></h3>
		<asp:Repeater ID="rptUSuites" runat="server">
			<HeaderTemplate><ol></HeaderTemplate>
			<ItemTemplate>
				<li>
					<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				</li>
			</ItemTemplate>
			<FooterTemplate></ol></FooterTemplate>
		</asp:Repeater>
	</div>
	<div class="whiteBox generate corners">
		<h3><asp:Literal ID="ltlWCount" runat="server"></asp:Literal></h3>
		<asp:Repeater ID="rptWSuites" runat="server">
			<HeaderTemplate><ol></HeaderTemplate>
			<ItemTemplate>
				<li>
					<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				</li>
			</ItemTemplate>
			<FooterTemplate></ol></FooterTemplate>
		</asp:Repeater>
	</div>
</div>