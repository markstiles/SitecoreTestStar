<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="WebTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.WebTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>

<asp:ScriptManager runat="server"></asp:ScriptManager>

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
        <h2>Test Settings</h2>
        <div class="wtEnvs whiteBox corners">
            <h3>Environments</h3>
            <div class="testInputs">
                <asp:Repeater ID="rptEnvironments" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <input type="checkbox" 
                                id="<%# GetShortID(((ListItem)Container.DataItem).Value) %>" 
                                name="<%# ((ListItem)Container.DataItem).Text %>" 
                                value="<%# ((ListItem)Container.DataItem).Value %>">
                            <label for="<%# GetShortID(((ListItem)Container.DataItem).Value) %>">
                                <%# ((ListItem)Container.DataItem).Text %>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="wtSystems whiteBox corners">
            <h3>Systems</h3>
            <div class="testInputs">
                <asp:Repeater ID="rptSystems" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <input type="checkbox" 
                                id="<%# GetShortID(((ListItem)Container.DataItem).Value) %>" 
                                name="<%# ((ListItem)Container.DataItem).Text %>" 
                                value="<%# ((ListItem)Container.DataItem).Value %>">
                            <label for="<%# GetShortID(((ListItem)Container.DataItem).Value) %>">
                                <%# ((ListItem)Container.DataItem).Text %>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="wtSites whiteBox corners">
            <h3>Sites</h3>
            <div class="testInputs">
                <asp:Repeater ID="rptSites" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <input type="checkbox" 
                                id="<%# GetShortID(((ListItem)Container.DataItem).Value) %>" 
                                name="<%# ((ListItem)Container.DataItem).Text %>" 
                                value="<%# ((ListItem)Container.DataItem).Value %>" 
                                class="<%# GetSystemName(((ListItem)Container.DataItem).Value) %>">
                            <label for="<%# GetShortID(((ListItem)Container.DataItem).Value) %>">
                                <%# ((ListItem)Container.DataItem).Text %>
                                <span class='systemName'>
                                    <%# GetSystemName(((ListItem)Container.DataItem).Value) %>
                                </span>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>    
        </div>
        <h2>Test Suites</h2>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
                <div class="wtTests whiteBox corners">
                    <h3 title="<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>">
						<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
                    </h3>
                    <div class="testInputs">
                        <asp:Repeater ID="rptFixtures" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <input type="checkbox" 
                                        id="<%# CondenseClassName(((ListItem)Container.DataItem).Text) %>" 
                                        name="<%# ((ListItem)Container.DataItem).Text %>" 
                                        value="<%# ((ListItem)Container.DataItem).Value %>">
                                    <label for="<%# CondenseClassName(((ListItem)Container.DataItem).Text) %>">
                                        <%# TestUtility.GetClassName(((ListItem)Container.DataItem).Text) %>
                                    </label>
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