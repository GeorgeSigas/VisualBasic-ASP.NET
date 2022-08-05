<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <div style="height: 33px; margin-left: 300px;">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
<div style="height: 424px; margin-left: 300px">
            <asp:Table ID="Table1" runat="server" CellPadding="5" CellSpacing="5" Height="419px" Width="535px" BorderStyle="None" Font-Size="Medium" HorizontalAlign="Justify">
            </asp:Table>
        </div>
        <p style="width: 696px; margin-left: 920px">
            <asp:Button ID="Button1" runat="server" Text="Previous" Width="154px" Enabled="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Next" Width="139px" style="margin-left: 0px" Enabled="False" />
        </p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000">
        </asp:Timer>
        &nbsp;&nbsp;&nbsp;
        <div>
            <asp:Image ID="Image1" runat="server" Height="51px" ImageUrl="https://image.freepik.com/free-icon/stopwatch_318-49797.jpg" Width="64px" />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="20pt" Height="45px" Text=": " Width="104px"></asp:Label>
        </div>
    </form>
</body>
</html>
