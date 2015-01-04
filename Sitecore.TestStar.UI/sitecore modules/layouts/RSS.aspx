<%@ Page language="c#" AutoEventWireup="true" 
	Inherits="Sitecore.TestStar.Core.UI.layouts.RSS" 
	CodeBehind="RSS.aspx.cs" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Entities" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="Sitecore.Data.Fields" %>

<asp:Literal id="XmlOutput" runat="server"></asp:Literal>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom">
	<channel>
		<title>Sitecore TestStar Test Results</title>
		<description><![CDATA[]]></description>
		<link><%= string.Format("http://{0}", Request.Url.Host) %></link>
		<language>en</language>
		<asp:Repeater ID="rptRSS" runat="server" OnItemDataBound="rptRSS_ItemDataBound">
			<ItemTemplate>
				<item>
					<title>
						<%# string.Format("<![CDATA[{0}]]>", ((TestResultList)Container.DataItem).Title) %>
					</title>
					<description>
						<%# "<![CDATA[" %>
						<asp:Repeater ID="rptEntries" runat="server" OnItemDataBound="rptEntries_ItemDataBound">
							<ItemTemplate>
								<div>
									<div>
										<%# ((DateField)((Item)Container.DataItem).Fields["Date"]).DateTime.ToString("hh:mm tt") %>: <%# ((Item)Container.DataItem)["Type"] %> - <%# ((Item)Container.DataItem)["Method"] %>
									</div>
									<div>
										<asp:Literal ID="ltlWebTestDetails" runat="server"></asp:Literal>
									</div>
									<div>
										<%# ((Item)Container.DataItem)["Message"] %>
									</div>
								</div>
							</ItemTemplate>
						</asp:Repeater>
						<%# "]]>" %>
					</description>
					<link>
						<%# string.Format("http://{0}/results", Request.Url.Host) %>
					</link>
					<guid>
						<%# string.Format("http://{0}/results?g={0}", Request.Url.Host, ((TestResultList)Container.DataItem).ID.Replace("{",string.Empty).Replace("-",string.Empty)) %>
					</guid>
					<pubDate>
						<%# ((TestResultList)Container.DataItem).Date.ToString("ddd, dd MMM yyyy hh:mm:ss") +  " GMT" %>
					</pubDate>
					<author>
						Sitecore TestStar
					</author>
				</item>	
			</ItemTemplate>
		</asp:Repeater>
	</channel>
</rss>