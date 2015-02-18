<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ResultsList.ascx.cs" 
    Inherits="Sitecore.TestStar.UI.userControls.ResultsList" %>
<%@ Import Namespace="NUnit.Core" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Utility" %>
<%@ Import Namespace="Sitecore.TestStar.Core.Providers" %>

<div class="resultWrap">
    <h2><%= TextProviderPaths.Page.Results(new SCTextEntryProvider()) %> <div class="resultCounter"></div></h2>
    <div class="result-head"></div>
    <div class="resultSet">
            
    </div>
	<div class="result-foot"></div>
</div>