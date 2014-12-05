<%@ Page Title="Create a new Program!" Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="CreateProgram.aspx.cs" Inherits="iWorkout.CreateProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/CreateProgram.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="body">
    <div class="content-wrapper">
        <asp:ListBox ID="AvailableProfiles" width="150px" runat="server" SelectionMode="Multiple"></asp:ListBox>
        <br />
        <asp:Button ID="AddProfiles" runat="server" Text="Add Selected Profiles" OnClick="AddProfiles_Click" CausesValidation="False" />
        <asp:Button ID="ShowProfileDetailButton" runat="server" Text="ShowDetail" OnClick="ShowProfileDetailButton_Click" CausesValidation="False" />
        <br />
        <asp:Button ID="CreateProfileButton" runat="server" CausesValidation="False" OnClick="CreateProfileButton_Click" Text="Create a new Profile!" />
        <br />
        <br />
        <asp:Panel class="portaldiv" ID="ProfileDetailPanel" runat="server">   
        </asp:Panel>
        <asp:Panel class="portaldiv" ID="Panel1"  runat="server">
            <asp:CheckBoxList ID="ChosenProfiles" runat="server"></asp:CheckBoxList>
        </asp:Panel>
        <br />
        <asp:Button ID="RemoveProfiles" runat="server" Text="Remove Checked Profiles" OnClick="RemoveProfiles_Click" CausesValidation="False" />
        <br />
        <br />
        

        <asp:Label ID="CreateProgramNameLabel" runat="server" Text="New Program Name"></asp:Label>
        <asp:TextBox ID="CreateProgramNameTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="SubmitProgram" runat="server" OnClick="SubmitProgram_Click" Text="Create this program! "></asp:Button>
        <asp:Button ID="CancelProgram" runat="server" Text="Cancel" CausesValidation="False" OnClick="CancelProgram_Click" />
        <asp:RequiredFieldValidator ID="ProgramNameRequiredValidator" runat="server" EnableClientScript="false" ToolTip="Must input a program name" ControlToValidate="CreateProgramNameTextBox">*</asp:RequiredFieldValidator>
        <%--asp:ListBox ID="ChosenProfiles" runat="server" SelectionMode="Multiple" ></asp:ListBox--%>
    </div>
    </div>
</asp:Content>
