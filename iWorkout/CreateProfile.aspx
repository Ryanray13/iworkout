<%@ Page Title="Create a new Profile!" Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="CreateProfile.aspx.cs" Inherits="iWorkout.CreateProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/CreateProfile.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div style="border-style:groove">
            <asp:Button ID="ShowActivityDetailButton" runat="server" Text="Create a new Activity!" OnClick="ShowActivityDetailButton_Click" />
            <asp:Panel id="ActivityDetailPanel" runat="server">
                <asp:TextBox ID="ActDescripTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ActDescripTextBoxValidator" EnableClientScript="false" runat="server" ErrorMessage="Make a name for this activity!" ToolTip="Make a name for this activity!" ControlToValidate="ActDescripTextBox">*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="AvailableActivityType" runat="server" 
                    DataSourceID="ActivityTypeDataSource" 
                    DataTextField="Name" 
                    DataValueField="Id">
                </asp:DropDownList>
                <asp:SqlDataSource ID="ActivityTypeDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iWorkoutAzureDBConnectionString %>" SelectCommand="SELECT [Id], [Name] FROM [ActivityTypes]"></asp:SqlDataSource>

                <asp:Button ID="SubmitActivityButton" runat="server" Text="Finish!" OnClick="SubmitActivityButton_Click" />
                <asp:Button ID="CancelActivityButton" runat="server" Text="Cancel" OnClick="CancelActivityButton_Click" />
            </asp:Panel>
            <asp:Label ID="ActivityConfirmLabel" runat="server" Text=""></asp:Label>
        </div>
        
        <div style="border-style:groove">
            <asp:DropDownList ID="AvailableActivities" runat="server"
                    DataSourceID="AvailableActivityDataSource" 
                    DataTextField="Description" 
                    DataValueField="Id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="AvailableActivityDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:iWorkoutAzureDBConnectionString %>" SelectCommand="SELECT [Id], [Description] FROM [Activities]"></asp:SqlDataSource>
            <br />
            <asp:DropDownList ID="SelectedAddDay" runat="server"></asp:DropDownList>
            <br />
            <asp:Button ID="ActivityAddButton" runat="server" Text="Add activity to selected day" OnClick="ActivityAddButton_Click" />
            <asp:Button ID="AddDayButton" runat="server" Text="Add a new Day" OnClick="AddDayButton_Click" />

        </div>

        <div style="border-style:groove">
            <asp:DropDownList ID="SelectedRemoveDay" runat="server"></asp:DropDownList>
            <br />
            <asp:Button ID="RemoveDayButton" runat="server" Text="Remove selected day" OnClick="RemoveDayButton_Click" />
        </div>
        <asp:Panel ID="ProfileDetailPanel" BorderStyle="Groove" ScrollBars="Both" Height="410px" runat="server"/>
        <asp:Button ID="RemoveActivitiesButton" runat="server" Text="Remove all checked activities" OnClick="RemoveActivitiesButton_Click" />

        <asp:Button ID="DeselectButton" runat="server" OnClick="DeselectButton_Click" Text="Deselect all activities" />

        <br />
        <br />
        <asp:Button ID="SubmitProfileButton" runat="server" OnClick="SubmitProfileButton_Click" Text="Finish Creating Profile" />

        <asp:Button ID="CancelProfileButton" runat="server" OnClick="CancelProfileButton_Click" Text="Cancel" />


</asp:Content>

