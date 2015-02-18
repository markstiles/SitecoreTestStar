<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="UnitTestPage.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.sublayouts.UnitTestPage" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Providers" %>
<%@ Register Src="../userControls/TestSuiteList.ascx" TagPrefix="ts" TagName="TestSuiteList" %>
<%@ Register Src="../userControls/GenerateForm.ascx" TagPrefix="ts" TagName="GenerateForm" %>
<%@ Register Src="../userControls/ResultList.ascx" TagPrefix="ts" TagName="ResultList" %>

<div class="log corners"></div><div></div>
<div class="error corners"></div><div></div>

<div class="whiteBox generate corners ut">
    <h3><%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %></h3>
    <ts:GenerateForm ID="GenerateForm" runat="server"></ts:GenerateForm>
</div>
<div></div>
<div class="testForm">
    <div class="suiteWrap">
        <h2><%= TextProviderPaths.Page.UnitTestSuites(new SCTextEntryProvider()) %></h2>
		<div class="blueSubmit corners">
			<input id="utSubmit" type="submit" value="<%= TextProviderPaths.Page.Run(new SCTextEntryProvider()) %>">
		</div>
		<ts:TestSuiteList ID="UnitTestList" runat="server" TestType="Unit"></ts:TestSuiteList>
	</div>
	<ts:ResultList ID="ResultList" runat="server"></ts:ResultList>
</div>
