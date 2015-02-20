<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="TestSuiteList.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.userControls.TestSuiteList" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.UI.Providers" %>

<div class="blueSubmit corners">
	<input id="tSubmit" type="submit" value="<%= TextProviderPaths.Page.Run(new SCTextEntryProvider()) %>">
</div>
<div class="allSelector"></div>
<asp:Repeater ID="rptSuites" runat="server" OnItemDataBound="rptSuites_ItemDataBound">
	<ItemTemplate>
		<div class="whiteBox corners <%# GetTestType() %>Tests testList">
            <h3 title="<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>">
				<%# ((KeyValuePair<string, TestSuite>)Container.DataItem).Key %>
			</h3>
            <div class="testInputs">
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <input type="checkbox" 
                                id="<%# string.Format("{0}.{1}", ((ListItem)Container.DataItem).Value, CondenseCatName(((ListItem)Container.DataItem).Text)) %>" 
                                name="<%# ((ListItem)Container.DataItem).Text %>" 
                                value="<%# ((ListItem)Container.DataItem).Value %>">
                            <label for="<%# string.Format("{0}.{1}", ((ListItem)Container.DataItem).Value, CondenseCatName(((ListItem)Container.DataItem).Text)) %>">
                                <%# TestUtility.GetClassName(((ListItem)Container.DataItem).Text) %>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
	</ItemTemplate>
</asp:Repeater>