<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.Core.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>


<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="whiteBox generate corners">
    <h3>Generate Script</h3>
    <div class="genToggle">+</div>
    <div class="genFields">
        <label for="utScriptName" class="title">Script Name</label>
        <input type="text" ID="utScriptName"></input>
		<div class="submit corners">
			<input id="utGenerate" type="submit" value="Generate Script">
		</div>
    </div>
</div>
<div></div>
<div class="testForm">
    <div class="suiteWrap">
        <h2>Unit Test Suites</h2>
        <asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
			<ItemTemplate>
			    <div class="whiteBox corners">
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
					    <input id="utSubmit" type="submit" value="Run">
                    </div>
                </div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	<div class="resultWrap">
        <h2>Results <div class="resultCounter"></div></h2>
        <div class="result-head"></div>
        <div class="resultSet">
            
        </div>
		<div class="result-foot"></div>
    </div>
</div>
