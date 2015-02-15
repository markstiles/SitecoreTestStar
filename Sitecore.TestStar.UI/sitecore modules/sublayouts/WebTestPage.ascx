<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="WebTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.sublayouts.WebTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Providers" %>
<%@ Register Src="../userControls/TestSuiteList.ascx" TagPrefix="ts" TagName="TestSuiteList" %>

<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="whiteBox generate corners">
    <h3><%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %></h3>
	<div class="genToggle">+</div>
    <div class="genFields">
        <label for="wtScriptName" class="title"><%= TextProviderPaths.Page.ScriptName(new SCTextEntryProvider()) %></label>
        <input type="text" ID="wtScriptName"></input>
		<div class="submit corners">
			<input id="wtGenerate" type="submit" value="<%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %>">
		</div>
    </div>
</div>
<div></div>
<div class="testForm">
    <div class="suiteWrap">
        <h2><%= TextProviderPaths.Page.TestSettings(new SCTextEntryProvider()) %></h2>
        <div class="wtEnvs whiteBox corners">
            <h3><%= TextProviderPaths.Page.Environments(new SCTextEntryProvider()) %></h3>
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
            <h3><%= TextProviderPaths.Page.Systems(new SCTextEntryProvider()) %></h3>
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
            <h3><%= TextProviderPaths.Page.Sites(new SCTextEntryProvider()) %></h3>
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
        <h2><%= TextProviderPaths.Page.TestSuites(new SCTextEntryProvider()) %></h2>
		<div class="whiteSubmit corners">
			<input id="wtSubmit" type="submit" value="<%= TextProviderPaths.Page.Run(new SCTextEntryProvider()) %>">
		</div>
        <ts:TestSuiteList ID="WebTestList" runat="server" TestType="Web"></ts:TestSuiteList>
    </div>
	<div class="resultWrap">
        <h2><%= TextProviderPaths.Page.Results(new SCTextEntryProvider()) %> <div class="resultCounter"></div></h2>
        <div class="result-head"></div>
        <div class="resultSet">
            
        </div>
		<div class="result-foot"></div>
    </div>
</div>