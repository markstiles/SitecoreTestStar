<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="WebTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.sublayouts.WebTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.UI.Providers" %>
<%@ Register Src="../userControls/TestSuiteList.ascx" TagPrefix="ts" TagName="TestSuiteList" %>
<%@ Register Src="../userControls/GenerateForm.ascx" TagPrefix="ts" TagName="GenerateForm" %>
<%@ Register Src="../userControls/ResultList.ascx" TagPrefix="ts" TagName="ResultList" %>

<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="wt">
    <div class="whiteBox generate corners">
        <h3><%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %></h3>
	    <ts:GenerateForm ID="GenerateForm" runat="server"></ts:GenerateForm>
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
            <div class="wtSites whiteBox corners">
                <h3><%= TextProviderPaths.Page.Systems(new SCTextEntryProvider()) %></h3>
                <div class="testInputs sysInputs">
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
                <h3><%= TextProviderPaths.Page.Sites(new SCTextEntryProvider()) %></h3>
                <div class="testInputs siteInputs">
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
		    <ts:TestSuiteList ID="WebTestList" runat="server" TestType="Web"></ts:TestSuiteList>
        </div>
	    <ts:ResultList ID="ResultList" runat="server"></ts:ResultList>
    </div>
</div>