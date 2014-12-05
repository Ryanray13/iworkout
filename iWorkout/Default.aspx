<%@ Page Title="" Language="C#" MasterPageFile="~/Navigation.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iWorkout.Default" %>
<%@ Register TagPrefix="ccl" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        body {
            padding: 0px;
            text-align: center;
        }

        .Image {
            width: 800px;
            margin: 0px auto;
            text-align: center;
            padding: 20px;
        }
    </style>
    <div id="body">
        <div class="content-wrapper">
            <div>
                <ccl:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </ccl:ToolkitScriptManager>
                <div class="Image">
                    <asp:Image ID="img1" runat="server"
                        Height="400px" Width="600px" ImageUrl="~/Images/workout1.jpg"
                         />
                </div>
                <ccl:SlideShowExtender
                    ID="SlideShowExtender1" runat="server"
                    BehaviorID="SSBehaviorID"
                    TargetControlID="img1"
                    SlideShowServiceMethod="GetSlides"
                    AutoPlay="true"
                    ImageDescriptionLabelID="lblDesc"
                    NextButtonID="btnNext"
                    PreviousButtonID="btnPrev"
                    PlayButtonID="btnPlay"
                    PlayButtonText="Play"
                    StopButtonText="Stop" PlayInterval="8000" SlideShowAnimationType="FadeInOut"    
                    Loop="true">
                </ccl:SlideShowExtender>
                <div>
                    <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label><br />
                    <asp:Button ID="btnPrev" runat="server" Text="Previous" />
                    <asp:Button ID="btnPlay" runat="server" Text="Stop" />
                    <asp:Button ID="btnNext" runat="server" Text="Next" />
                </div>
            </div>
            <p>
                This is Default Page. Please log in to  navigate to your portal page.
            </p>
        </div>
    </div>
</asp:Content>

