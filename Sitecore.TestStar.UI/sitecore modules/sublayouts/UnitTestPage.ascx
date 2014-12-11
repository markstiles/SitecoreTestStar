<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>


<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="whiteBox generate corners">
    <h3>Generate Script</h3>
    <div class="genFields">
        <asp:Label ID="lblScript" AssociatedControlID="txtScriptName" Text="Script Name" CssClass="title" runat="server"></asp:Label>
        <asp:TextBox ID="txtScriptName" runat="server"></asp:TextBox>
    </div>
    <div class="submit corners">
        <asp:Button ID="btnCreateScript" OnClick="btnCreateScript_Click" Text="Generate Script" runat="server" />
    </div>
</div>
<div></div>
<div class="testForm">
    <div class="suiteWrap">
        <h2>Unit Test Suites</h2>
        <div class="whiteBox subtext corners">If no categories are selected the entire suite will run.</div>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
			    <div class="whiteBox corners">
                	<h3>
					    <%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				    </h3>
                    <div class="testInputs">
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
        <h2>Results</h2>
        <div class="result-head"></div>
        <div class="resultSet">
            
        </div>
		<div class="result-foot"></div>
    </div>
</div>
