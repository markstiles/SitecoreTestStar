$(document).ready(function () {

    // show properties and environments on page load
    DisplayProperties();
    DisplayEnvironments();

    // add property button click
    $(".sitePropAdd input").click(function (e) {
        e.preventDefault();
        AddProp();
    });

    // on add or edit property submit click
    $(PropSubmitSelector).click(function (e) {
        e.preventDefault();
        UpdateProp($(PropEKeySelector).val(), $(PropEValueSelector).val());
        DisplayProperties();
        CloseAll();
    });

    // add environment button click
    $(".siteEnvAdd input").click(function (e) {
        e.preventDefault();
        AddEnv();
    });

    // on add or edit environment submit click
    $(EnvSubmitSelector).click(function (e) {
        e.preventDefault();
        var eID = $(EnvENameSelector).val();
        UpdateEnv($(EnvENameSelector + " option[value='" + eID + "']").text().split(eID + "-").join(""), eID, $(EnvEDomainSelector).val(), $(EnvEIPSelector).val());
        DisplayEnvironments();
        CloseAll();
    });

    // cancel closes the popup
    $("#envCancel, #propCancel").click(function (e) {
        e.preventDefault();
        CloseAll();
    });

    //handle remove clicks
    $(".envSelSub").click(function (e) {
        var eID = $(".envSelDDL").val();
        var eName = $(".envSelDDL option[value='" + eID + "']").text()
        if (!confirm("Are you sure you want to remove the '" + eName + "' Environment?"))
            e.preventDefault();
    });

    $(".sysSelSub").click(function (e) {
        var sID = $(".sysSelDDL").val();
        var sName = $(".sysSelDDL option[value='" + sID + "']").text()
        if (!confirm("Are you sure you want to remove the '" + sName + "' System?"))
            e.preventDefault();
    });

    $(".siteSelSub").click(function (e) {
        var sID = $(".siteSelDDL").val();
        var sName = $(".siteSelDDL option[value='" + sID + "']").text()
        if (!confirm("Are you sure you want to remove the '" + sName + "' Site?"))
            e.preventDefault();
    });
});

/* PROPERTIES */

var PropListSelector = ".sitePropList";
var PropValSelector = ".sitePropVals input";
var PropSubmitSelector = "#propSubmit";
var PropEKeySelector = ".propPopup .ekey input";
var PropEValueSelector = ".propPopup .evalue input";

// open property popup and clear values
function AddProp(){
    $(PropEKeySelector + ", " + PropEValueSelector).val("");
    OpenPropWindow();
}

// open property popup and set values
function EditProp(propName) {
    var json = GetJSONObj(PropValSelector);
    var json = (json == null) ? {} : json;
    jQuery.each(json, function (key, val) {
        if (key == propName) {
            $(PropEKeySelector).val(key);
            $(PropEValueSelector).val(val);
        }
    });
    OpenPropWindow();
}

// open property window
function OpenPropWindow(){
    $(".cover").show();
    $(".propPopup").show();
}

// update the stored property valule and links with the new data
function UpdateProp(propName, propValue) {
    var json = GetJSONObj(PropValSelector);
    var json = (json == null) ? {} : json;
    if (propValue == "" && (propName in json)) { //if the value is empty, remove it
        delete json[propName];
    } else { // add or update
        json[propName] = propValue;
    }
    $(PropValSelector).val(JSON.stringify(json));
    DisplayProperties(); // update links
}

// setup the edit/remove property links 
function DisplayProperties() {
    var json = GetJSONObj(PropValSelector);
    var json = (json == null) ? {} : json;
    $(PropListSelector).html("");
    jQuery.each(json, function (key, val) {
        $(PropListSelector).append(
            "<div class='attValues'><span>" + key + ":" + val + "</span>" +
            "<a class='attEditLnk' id='' onclick='javascript:EditProp(\"" + key + "\");'>edit</a>" +
            "<a class='attRemoveLnk' id='' onclick='javascript:UpdateProp(\"" + key + "\", \"\");'>remove</a></div>"
        );
    });
}

/* ENVIRONMENTS */

var EnvListSelector = ".siteEnvList";
var EnvValSelector = ".siteEnvVals input";
var EnvSubmitSelector = "#envSubmit";
var EnvENameSelector = ".envPopup .ename select";
var EnvEDomainSelector = ".envPopup .edomain input";
var EnvEIPSelector = ".envPopup .eip input";

// open environment popup and clear values
function AddEnv() {
    $(EnvENameSelector).val("0");
    $(EnvEDomainSelector + "," + EnvEIPSelector).val("");
    OpenEnvWindow();
}

// open environment popup and set values
function EditEnv(envID) {
    var json = GetJSONObj(EnvValSelector);
    var json = (json == null) ? [] : json;
    jQuery.each(json, function (key, val) {
        if (val.ID == envID) {
            $(EnvENameSelector).val(val.ID);
            $(EnvEDomainSelector).val(val.DomainPrefix);
            $(EnvEIPSelector).val(val.IPAddress);
        }
    });
    OpenEnvWindow();
}

// open property window
function OpenEnvWindow() {
    $(".cover").show();
    $(".envPopup").show();
}

// update the stored property value and links with the new data
function UpdateEnv(eName, eNameID, eDomain, eIP) {
    var json = GetJSONObj(EnvValSelector);
    var json = (json == null) ? [] : json;
    var pos = -1;
    jQuery.each(json, function (key, val) {
        if (val.ID == eNameID) 
            pos = key;
    });
    if(pos >= 0 && eName == "") { // remove
            json.splice(pos, 1);
    } else if (pos >= 0) { // update
        json[pos].Name = eName;
        json[pos].ID = eNameID;
        json[pos].DomainPrefix = eDomain;
        json[pos].IPAddress = eIP;
    } else { // add
        json.push({ "ID": eNameID, "Name": eName, "DomainPrefix": eDomain, "IPAddress": eIP });
    }
    $(EnvValSelector).val(JSON.stringify(json));
    DisplayEnvironments(); // update links
}

// setup the edit/remove environment links 
function DisplayEnvironments() {
    var json = GetJSONObj(EnvValSelector);
    var json = (json == null) ? [] : json;
    $(EnvListSelector).html("");
    var append = "";
    jQuery.each(json, function (key, val) {
        append += "<div class='attValues'><span>" + val.Name + " -</span>" + 
        "<a class='attEditLnk' id='' onclick='javascript:EditEnv(\"" + val.ID + "\");'>edit</a>" +
        "<a class='attRemoveLnk' id='' onclick='javascript:UpdateEnv(\"\", \"" + val.ID + "\", \"\", \"\");'>remove</a>";
        if(val.DomainPrefix.length > 0)
            append += "<div class='att'>Domain Prefix: " + val.DomainPrefix + "</div>";
        if(val.IPAddress.length > 0)
            append += "<div class='att'>IP Address: " + val.IPAddress + "</div>";
        append += "</div>";
    });
    $(EnvListSelector).append("<div>" + append + "</div>");
}

/* UNIVERSAL */

// gets a json obj or null from the input selected
function GetJSONObj(selector) {
    if ($(selector).length == 0)
        return null;
    var values = $(selector).val();
    return (values == "null" || values == "")
        ? null
        : jQuery.parseJSON(values);
}

// hide the popups and grey bg
function CloseAll() {
    $(".cover, .envPopup, .propPopup").hide();
}

