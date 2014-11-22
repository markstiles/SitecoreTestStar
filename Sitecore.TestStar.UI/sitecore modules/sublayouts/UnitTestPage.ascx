<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>

<h1>Unit Testing</h1>
<div class="log">
    <asp:Literal ID="ltlLog" runat="server"></asp:Literal>
</div>
<div class="error">
    <asp:Literal ID="ltlError" runat="server"></asp:Literal>
</div>
<div class="generate">
    <div class="formRow">
        <asp:Label ID="lblScript" AssociatedControlID="txtScriptName" Text="Script Name" CssClass="title" runat="server"></asp:Label>
        <div class="bordered">
            <asp:TextBox ID="txtScriptName" runat="server"></asp:TextBox>
            <asp:Button ID="btnCreateScript" CssClass="generateSubmit submit" OnClick="btnCreateScript_Click" Text="Generate Script" runat="server" />
        </div>
    </div>
</div>
<div class="UnitTestForm">
    <div class="row suites">
		<asp:Repeater ID="rptSuites" runat="server">
			<ItemTemplate>
				<a class="utSuite" href="#" test="<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>">
					<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				</a>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	<div class="formRow">
		<div class="bordered utCategories">
            
        </div>
    </div>
	<div class="formRow">
        <label class="title">Results</label>
		<input id="utSubmit" class="submit" type="submit" value="Run">
		<div class="subtext">If no categories are selected the entire suite will run.</div>
        <div class="resultSet bordered">
            
        </div>
    </div>
</div>