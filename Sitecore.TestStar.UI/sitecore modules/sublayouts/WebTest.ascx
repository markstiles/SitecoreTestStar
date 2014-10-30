<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="WebTest.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.WebTest" %>
 
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<h1>Web Testing</h1>
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
<div class="TestForm">
    <div class="formRow">
        <asp:Label ID="lblTests" CssClass="title" AssociatedControlID="cblTests" Text="Tests" runat="server"></asp:Label>
        <div class="bordered">
            <asp:CheckBoxList ID="cblTests" CssClass="cblTests checkboxlist" runat="server"></asp:CheckBoxList>
        </div>
    </div>
    <div class="formRow">
        <asp:Label ID="lblEnv" CssClass="title" AssociatedControlID="cblEnv" Text="Environments" runat="server"></asp:Label>
        <div class="bordered">
            <asp:CheckBoxList ID="cblEnv" CssClass="cblEnv checkboxlist" runat="server"></asp:CheckBoxList>
        </div>
    </div>
    <div class="formRow">
        <asp:Label ID="lblSystems" CssClass="title" AssociatedControlID="cblSystems" Text="Systems" runat="server"></asp:Label>
        <div class="bordered">
            <asp:CheckBoxList ID="cblSystems" CssClass="cblSystems checkboxlist" runat="server"></asp:CheckBoxList>
        </div>
    </div>
    <div class="formRow">
        <asp:Label ID="lblSites" CssClass="title" AssociatedControlID="cblSites" Text="Sites" runat="server"></asp:Label>
        <div class="bordered">
            <asp:CheckBoxList ID="cblSites" CssClass="cblSites checkboxlist" runat="server"></asp:CheckBoxList>
        </div>
    </div>
    <div class="formRow">
        <label class="title">Results</label>
        <asp:Button ID="btnSubmitTests" CssClass="testSubmit submit" OnClick="btnSubmitTests_Click" Text="Run" runat="server" />
        <div class="resultSet bordered">
            <asp:Literal ID="ltlResults" runat="server"></asp:Literal>
        </div>
    </div>
</div>