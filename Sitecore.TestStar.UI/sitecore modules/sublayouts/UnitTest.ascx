<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTest.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.UnitTest" %>

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
    <div class="formRow">
        <asp:Label ID="lblCat" CssClass="title" AssociatedControlID="cblCategories" Text="Category Filter" runat="server"></asp:Label>
		<div class="bordered">
            <asp:CheckBoxList runat="server" ID="cblCategories" />
        </div>
    </div>
	<div class="formRow">
        <label class="title">Results</label>
		<asp:Button runat="server" CssClass="submit" ID="btnSubmitTests" Text="Run" OnClick="btnSubmitTests_Click" />
        <div class="subtext">If no categories are selected the entire suite will run.</div>
        <div class="resultSet bordered">
            <asp:Literal ID="ltlResults" runat="server"></asp:Literal>
        </div>
    </div>
</div>