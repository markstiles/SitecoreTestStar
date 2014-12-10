<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="WebTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.WebTestPage" %>
 <%@ Import Namespace="NUnit.Core" %>


<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:Panel ID="pnlLog" runat="server" CssClass="log corners">
    <asp:Literal ID="ltlLog" runat="server"></asp:Literal>
</asp:Panel>
<div></div>
<asp:Panel ID="pnlError" runat="server" CssClass="error corners">
    <asp:Literal ID="ltlError" runat="server"></asp:Literal>
</asp:Panel>
<div></div>
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
        <h2>Web Test Suites</h2>
        <div class="wtEnvs whiteBox corners">
            <h3>Environments</h3>
            <div class="testInputs">
                <asp:CheckBoxList ID="cblEnv" CssClass="cblEnv checkboxlist" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="wtSystems whiteBox corners">
            <h3>Systems</h3>
            <div class="testInputs">
                <asp:CheckBoxList ID="cblSystems" CssClass="cblSystems checkboxlist" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="wtSites whiteBox corners">
            <h3>Sites</h3>
            <div class="testInputs">
                <asp:CheckBoxList ID="cblSites" CssClass="cblSites checkboxlist" runat="server"></asp:CheckBoxList>
            </div>    
        </div>
        <div></div>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
                <div class="wtTests whiteBox corners">
                    <h3><%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %></h3>
                    <div class="testInputs">
                        <asp:Repeater ID="rptFixtures" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <input type="checkbox" id="id<%# ((ListItem)Container.DataItem).Text %>" name="<%# ((ListItem)Container.DataItem).Text %>" value="<%# ((ListItem)Container.DataItem).Text %>">
                                    <label for="id<%# ((ListItem)Container.DataItem).Text %>"><%# ((ListItem)Container.DataItem).Value %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="submit corners">
			            <input id="wtSubmit" type="submit" value="Run">
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