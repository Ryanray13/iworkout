<%@ Page Title="Find a Program in the Market Place" Language="C#" MasterPageFile="~/Navigation.Master" AutoEventWireup="true" CodeBehind="MarketPlace.aspx.cs" Inherits="iWorkout.MarketPlace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .floatLeft { float: left; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelContent" CssClass="floatLeft" BorderStyle="Dotted" runat="server" >
        <asp:ListBox ID="ListBoxAvailablePrograms" SelectionMode="Single" runat="server" />
    </asp:Panel>

    <asp:Panel ID="PanelView" CssClass="floatLeft" BorderStyle="Solid" runat="server">

        <asp:Panel ID="PanelClientAccess" runat="server">
            <asp:Button ID="ButtonClientSubscribe" runat="server" Text="Subscribe to this program!" OnClick="ButtonClientSubscribe_Click" />
        </asp:Panel>

        <asp:Panel ID="PanelCoachAccess" runat="server">
            <asp:DropDownList ID="DropDownListCurrentClients" runat="server"></asp:DropDownList>
            <asp:Button ID="ButtonCoachSubscribe" runat="server" Text="Subscribe this Client." OnClick="ButtonCoachSubscribe_Click" />
        </asp:Panel>

    </asp:Panel>

</asp:Content>
