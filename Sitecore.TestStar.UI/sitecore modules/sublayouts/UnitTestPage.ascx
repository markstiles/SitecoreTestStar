<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>


<asp:Panel ID="pnlLog" runat="server" CssClass="log corners">
    <asp:Literal ID="ltlLog" runat="server"></asp:Literal>
</asp:Panel>
<div></div>
<asp:Panel ID="pnlError" runat="server" CssClass="error corners">
    <asp:Literal ID="ltlError" runat="server"></asp:Literal>
</asp:Panel>
<div></div>
<div class="generate corners">
    <h2>Generate Script</h2>
    <div class="genFields">
        <asp:Label ID="lblScript" AssociatedControlID="txtScriptName" Text="Script Name" CssClass="title" runat="server"></asp:Label>
        <asp:TextBox ID="txtScriptName" runat="server"></asp:TextBox>
    </div>
    <div class="submit corners">
        <asp:Button ID="btnCreateScript" OnClick="btnCreateScript_Click" Text="Generate Script" runat="server" />
    </div>
</div>
<div></div>
<div class="utForm">
    <div class="utSuiteList">
        <h3>Unit Test Suites</h3>
        <div class="subtext corners">If no categories are selected the entire suite will run.</div>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
			    <div class="utSuite corners">
                	<h2>
					    <%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				    </h2>
                    <div class="utCategories">
                        <asp:Repeater ID="rptCategories" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <input type="checkbox" id="id<%# Container.DataItem %>" name="<%# Container.DataItem %>" value="<%# Container.DataItem %>">
                                    <label for="id<%# Container.DataItem %>"><%# Container.DataItem %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="submit corners">
					    <input id="utSubmit" type="submit" value="Run">
                    </div>
                </div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	<div class="resultWrap">
        <h3>Results</h3>
        <div class="resultSet">
            
        </div>
		<div class="result-foot"></div>
    </div>
</div>