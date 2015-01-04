<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="TestResultPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.TestResultPage" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Entities" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="Sitecore.Data.Fields" %>

<div class="ResultList">
	<asp:Panel ID="pnlNav" Visible="false" CssClass="pagerWrap" runat="server">
		<asp:HyperLink ID="lnkPrev" Visible="false" Text="< Previous Page" CssClass="prevPage whiteBox corners" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext" Visible="false" Text="Next Page >" CssClass="nextPage whiteBox corners" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</asp:Panel>
	<div>
		<div class="message">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<asp:Repeater ID="rptResults" runat="server" OnItemDataBound="rptResults_ItemDataBound">
			<HeaderTemplate>
				
			</HeaderTemplate>
			<ItemTemplate>
				<asp:PlaceHolder ID="phDateHead" runat="server" Visible="false">
					<h2><%# ((TestResultList)Container.DataItem).Date.ToString("MMMM d, yyyy") %></h2>
				</asp:PlaceHolder>
				<div class="resultList whiteBox corners">
					<h3><%# ((TestResultList)Container.DataItem).Title %></h3>
					<asp:Repeater ID="rptEntries" runat="server" OnItemDataBound="rptEntries_ItemDataBound">
						<ItemTemplate>
							<div class="testResult">
								<div class="resultDate">
									<%# ((DateField)((Item)Container.DataItem).Fields["Date"]).DateTime.ToString("hh:mm tt") %>:
								</div>
								<div class="resultType t<%# ((Item)Container.DataItem)["Type"] %>">
									<%# ((Item)Container.DataItem)["Type"] %>
								</div>
								<div class="resultMethod">
									- <%# ((Item)Container.DataItem)["Method"] %>
								</div>
								<div class="clear"></div>
								<div>
									<asp:Literal ID="ltlWebTestDetails" runat="server"></asp:Literal>
								</div>
								<div class="resultMessage" style="<%# (((Item)Container.DataItem)["Message"]).Length > 0 ? string.Empty : "display:none;" %>">
									<%# ((Item)Container.DataItem)["Message"] %>
								</div>
								<div class="clear"></div>
							</div>
						</ItemTemplate>
					</asp:Repeater>	
				</div>
			</ItemTemplate>
			<FooterTemplate>
				
			</FooterTemplate>
		</asp:Repeater>
	</div>
	<asp:Panel ID="pnlNav2" Visible="false" CssClass="pagerWrap" runat="server">
		<asp:HyperLink ID="lnkPrev2" Visible="false" Text="< Previous Page" CssClass="prevPage whiteBox corners" runat="server"></asp:HyperLink>
		<asp:HyperLink ID="lnkNext2" Visible="false" Text="Next Page >" CssClass="nextPage whiteBox corners" runat="server"></asp:HyperLink>
        <div class="clear"></div>
	</asp:Panel>
</div>