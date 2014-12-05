<%@ Page Title="Portal" Language="C#" MasterPageFile="~/Navigation.Master" AutoEventWireup="true" CodeBehind="Portal.aspx.cs" Inherits="iWorkout.Portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <link href="Style/Portal.css" rel="stylesheet" />
    <div id="body">
        <div class="content-wrapper">
            <asp:Panel  ID="ClientPortalPanel" runat="server">
                <div class="portaldiv">
            <h2>Today's Activities </h2>
                    <asp:Panel class="panel" ID="TodayPanel" Height="200px" ScrollBars="Both" runat="server">
            </asp:Panel>
        </div>
                <div class="portaldiv">
            <h2>Activity Log </h2>
            <asp:Panel ID="LogPanel" Height="250px" runat="server">
                <asp:DropDownList ID="LogActivityDropDownList" runat="server"></asp:DropDownList>
                <br />
                <div style="display:inline-block " >
                <asp:Label ID="AccomplisthLabel" runat="server" Text="Accomplished"></asp:Label>
                <asp:CheckBox ID="AccomplishedCheckBox" runat="server" />
                </div>
                <br />
                <asp:Label ID="MemoLabel" runat="server" Text="Supply a Memo"></asp:Label>
                <asp:TextBox ID="MemoTextBox" runat="server"></asp:TextBox>
                <br />
                <asp:Button  ID="SubmitLogButton" runat="server" Text="Submit this log." OnClick="SubmitLogButton_Click" />
            </asp:Panel>
        </div>
                <div class="portaldiv">
            <h2>Subscriptions </h2>
            <asp:Panel ID="SubscriptionPanel" Height="200px" ScrollBars="Vertical" runat="server">
            </asp:Panel>
                    <asp:Panel ID="UnsubscribePanel" runat="server">
                <asp:DropDownList ID="AllSubscriptionDropDownList" runat="server"></asp:DropDownList>
                <asp:Button ID="UnsubscribeButton" runat="server" Text="Unsubscribe" OnClick="UnsubscribeButton_Click" />
            </asp:Panel>

        </div>
                <div class="portaldiv">
            <h2>All Programs 
                    <asp:Button ID="Button1" runat="server" Text="Create a New Program" OnClick="Button1_Click" />
                <asp:Button ID="SubscribeNewProgramButton" runat="server" Text="Subscribe to a New Program" OnClick="SubscribeNewProgramButton_Click" />
            </h2>
                    <asp:Panel ID="AllProgramPanel" ScrollBars="Vertical" Height="200px" runat="server">
                <asp:Table ID="AllProgramTable" Width="700px" runat="server">
                </asp:Table>
            </asp:Panel>
        </div>
    </asp:Panel>  
       </div>
     
        <div class="content-wrapper">
        <asp:Panel ID="CoachPortalPanel" runat="server">
                <asp:Panel class="portaldiv"  ID="getNewClientPanel" BorderStyle="Dotted" BorderWidth="1" runat="server">
                <h2>Get a new Client</h2>
                <asp:Label runat="server">Client user name: 
                <asp:TextBox ID="ClientNameTextBox" Width="200px" runat="server"></asp:TextBox>
                </asp:Label>
                <br />
                <asp:Button ID="MakeRelationshipButton" runat="server" Text="Engage this client." OnClick="MakeRelationshipButton_Click" />
                <asp:Label ID="EngageClientStatus" runat="server" Visible="false" Text=""></asp:Label>
            </asp:Panel>
                <asp:Panel class="portaldiv" ID="SubscribeClientPanel" BorderStyle="Dotted" BorderWidth="1" runat="server">
                <h2>Subscribe your clients to programs</h2>
                <asp:Button ID="CoachSubscribeButton" CssClass="ButtonLarge" runat="server" Text="Go to the market place!" OnClick="CoachSubscribeButton_Click" />
            </asp:Panel>
             <asp:Panel style="padding:20px" ID="ActivityLogPanel" BorderStyle="Dotted" BorderWidth="1" Height="250px" runat="server">
                <h2>See your clients activity logs</h2>
                 <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" >
                     <asp:Table ID="ActivityLogTable" Width="700px"  runat="server">
                     </asp:Table>
                 </asp:Panel>                
            </asp:Panel>
        </asp:Panel> 
    </div>
    </div>

</asp:Content>
 