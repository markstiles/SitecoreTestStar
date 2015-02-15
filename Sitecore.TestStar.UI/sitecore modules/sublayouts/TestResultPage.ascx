<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="TestResultPage.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.sublayouts.TestResultPage" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Entities.Interfaces" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="Sitecore.Data.Fields" %>

<div class="resultList">
    <div class="rssLink">
        <asp:HyperLink ID="lnkFeed" Target="_blank" runat="server" Text="RSS"></asp:HyperLink>
	</div>
	<div class="pagerWrap">
		<asp:HyperLink ID="lnkPrev" Visible="false" CssClass="prevPage whiteBox corners" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext" Visible="false" CssClass="nextPage whiteBox corners" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</div>
	<div>
		<div class="message">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<asp:Repeater ID="rptResults" runat="server" OnItemDataBound="rptResults_ItemDataBound">
			<ItemTemplate>
				<asp:PlaceHolder ID="phDateHead" runat="server" Visible="false">
					<h2><%# ((ITestResultList)Container.DataItem).Date.ToString("MMMM d, yyyy") %></h2>
				</asp:PlaceHolder>
				<div class="resultList whiteBox corners">
					<h3><%# ((ITestResultList)Container.DataItem).Title %></h3>
					<asp:Repeater ID="rptEntries" runat="server">
						<ItemTemplate>
							<div class="testResult">
								<div class="resultDate">
									<%# ((ITestResult)Container.DataItem).Date.ToString("hh:mm tt") %>:
								</div>
								<div class="resultType t<%# ((ITestResult)Container.DataItem).Type %>">
									<%# ((ITestResult)Container.DataItem).Type %>
								</div>
								<div class="resultMethod">
									- <%# ((ITestResult)Container.DataItem).Method %>
								</div>
								<div class="clear"></div>
								<div>
                                    <%# ((ITestResult)Container.DataItem).AdditionalInfo %>
								</div>
								<div class="resultMessage" style="<%# (((ITestResult)Container.DataItem).Message).Length > 0 ? string.Empty : "display:none;" %>">
									<%# ((ITestResult)Container.DataItem).Message %>
								</div>
								<div class="clear"></div>
							</div>
						</ItemTemplate>
					</asp:Repeater>	
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	<div class="pagerWrap">
		<asp:HyperLink ID="lnkPrev2" Visible="false" CssClass="prevPage whiteBox corners" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext2" Visible="false" CssClass="nextPage whiteBox corners" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</div>
</div>