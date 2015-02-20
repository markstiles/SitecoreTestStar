<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="GenerateForm.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.userControls.GenerateForm" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.UI.Providers" %>

<div class="genToggle">+</div>
<div class="genFields">
    <label for="tScriptName" class="title"><%= TextProviderPaths.Page.ScriptName(new SCTextEntryProvider()) %></label>
    <input type="text" ID="tScriptName"></input>
	<div class="submit corners">
		<input id="tGenerate" type="submit" value="<%= TextProviderPaths.Page.GenerateScript(new SCTextEntryProvider()) %>">
	</div>
</div>