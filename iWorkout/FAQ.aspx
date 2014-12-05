<%@ Page Title="MiWorkout FAQ page" Language="C#" MasterPageFile="~/Navigation.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="iWorkout.FAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>FAQ of MiWorkout</h2>
    </hgroup>

    <article>

        <h5>What is MiWorkout?</h5>
        <p>* MiWorkout is a cloud based platform where users can design your own custom programs, publish these programs,
            and track yours or other's progress over these programs.
        </p>

        <h5>MiWorkout is right for me if...</h5>
        <p>* I've been looking for a single resource to organize and track my daily activities.</p> 
        <p>* There are activities that no one but ME knows about. And I would like to design and track that activity.</p>
        <p>* I'm looking for a place where its easy to design fitness programs and subscribe to other people's programs.</p>
        <p>* I rule at designing fitness programs and I'd like to share my program for free or for contract.</p>
        <p>* I have several fitness clients, and I'd like a way to manage their progress.</p>
        <p>* I'd like to engage a fitness coach who can track my progress over a customizable fitness plan.</p>

    </article>
    <hr style="border:1;border-bottom:1px dashed #ccc;background:#999"/>
</asp:Content>
