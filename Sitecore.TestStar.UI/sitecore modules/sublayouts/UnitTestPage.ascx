<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Providers" %>

<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="whiteBox generate corners">
    <h3><%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %></h3>
    <div class="genToggle">+</div>
    <div class="genFields">
        <label for="utScriptName" class="title"><%= TextProviderPaths.Page.ScriptName(new SCTextEntryProvider()) %></label>
        <input type="text" ID="utScriptName"></input>
		<div class="submit corners">
			<input id="utGenerate" type="submit" value="<%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %>">
		</div>
    </div>
</div>
<div></div>
<div class="testForm">
    <div class="suiteWrap">
        <h2><%= TextProviderPaths.Page.UnitTestSuites(new SCTextEntryProvider()) %></h2>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
			    <div class="utTests whiteBox corners">
                	<h3 title="<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>">
					    <%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
				    </h3>
                    <div class="testInputs">
                        <asp:Repeater ID="rptCategories" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <input type="checkbox" 
                                        id="<%# CondenseCatName(((ListItem)Container.DataItem).Text) %>" 
                                        name="<%# ((ListItem)Container.DataItem).Text %>" 
                                        value="<%# ((ListItem)Container.DataItem).Value %>">
                                    <label for="<%# CondenseCatName(((ListItem)Container.DataItem).Text) %>">
                                        <%# TestUtility.GetClassName(((ListItem)Container.DataItem).Text) %>
                                    </label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="submit corners">
					    <input id="utSubmit" type="submit" value="<%= TextProviderPaths.Page.Run(new SCTextEntryProvider()) %>">
                    </div>
                </div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	<div class="resultWrap">
        <h2><%= TextProviderPaths.Page.Results(new SCTextEntryProvider()) %> <div class="resultCounter"></div></h2>
        <div class="result-head"></div>
        <div class="resultSet">
            
        </div>
		<div class="result-foot"></div>
    </div>
</div>
