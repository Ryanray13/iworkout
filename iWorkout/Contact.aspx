<%@ Page Title="Contact us" Language="C#" MasterPageFile="~/Navigation.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="iWorkout.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

    <section class="contact">
            <h3>Email:</h3>
       
        <p>
            <span class="label">David Hung:</span>
            <span><a href="mailto:Brian.Williams@nbc.com">Brian.Williams@nbc.com</a></span>
        </p>
        <p>
            <span class="label">Jingxig (Steven) Zhu:</span>
            <span><a href="mailto:jz1371@nyu.edu">blalah@nyu.edu</a></span>
        </p>
        <p>
            <span class="label">Wuping (Ray) Lei:</span>
            <span><a href="mailto:General@example.com">hoo@sina.com</a></span>
        </p>
    </section>
    <section class="contact">
            <h3>Address:</h3>
        <p>
             251 Mercer St,<br />
            New York, NY 10012   
        </p>
    </section>


</asp:Content>
