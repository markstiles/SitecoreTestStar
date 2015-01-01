<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="TestResultPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.TestResultPage" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Entities" %>

<div class="ResultList">
	<asp:Panel ID="pnlNav" Visible="false" CssClass="pagerWrap" runat="server">
		<asp:HyperLink ID="lnkPrev" Visible="false" Text="< Previous Page" CssClass="prevPage" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext" Visible="false" Text="Next Page >" CssClass="nextPage" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</asp:Panel>
	<div>
		<div class="Message">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<asp:Repeater ID="rptResults" runat="server">
			<HeaderTemplate>
				<div class="Results">
			</HeaderTemplate>
			<ItemTemplate>
				<div class="TestResult">
					<h3><%# ((TestResultEntry)Container.DataItem).Title %></a></h3>
					<div class="DateCreated">
						<%# ((TestResultEntry)Container.DataItem).Date.ToString("MMMM d, yyyy") %>
					</div>
					<div class"ResultType">
						<%# (((TestResultEntry)Container.DataItem).IsUnitTest) ? "Unit Test" : "Web Test" %>
					</div>
					<div class="clear"></div>
					<div class="Message">
						<%# ((TestResultEntry)Container.DataItem).Message %>
					</div>
					<div class="clear"></div>
				</div>
			</ItemTemplate>
			<FooterTemplate>
				</div>
			</FooterTemplate>
		</asp:Repeater>
	</div>
	<asp:Panel ID="pnlNav2" Visible="false" CssClass="pagerWrap" runat="server">
		<asp:HyperLink ID="lnkPrev2" Visible="false" Text="< Previous Page" CssClass="prevPage" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext2" Visible="false" Text="Next Page >" CssClass="nextPage" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</asp:Panel>
</div>