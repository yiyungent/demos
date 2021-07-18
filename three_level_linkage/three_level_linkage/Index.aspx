<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="three_level_linkage.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList runat="server" ID="ddlCountry" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList runat="server" ID="ddlProvince" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList runat="server" ID="ddlCity" AutoPostBack="true"></asp:DropDownList>
    </form>
</body>
</html>
