<%@ Page Title="Login" Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="Login.aspx.cs" Inherits="iWorkout.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="body">
        <section class="content-wrapper main-content clear-fix">

            <!-- Login part -->
            <section id="loginForm">
                <h2>Log in.</h2>
                <!-- <h2>Use a local account to log in.</!--h2> -->
                <asp:Login DestinationPageUrl="~/Portal.aspx" runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
                    <LayoutTemplate>
                        <p class="validation-summary-errors">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                        <fieldset>
                            <legend>Log in Form</legend>
                            <ol>
                                <li>
                                    <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                    <asp:TextBox runat="server" ID="UserName" />
                                    <asp:RequiredFieldValidator ID="loginUsernameValidator" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." ValidationGroup="loginGroup" />
                                </li>
                                <li>
                                    <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." ValidationGroup="loginGroup" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                                </li>
                            </ol>
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log in" CausesValidation="true" ValidationGroup="loginGroup" /> 
                        </fieldset>
                    </LayoutTemplate>
                </asp:Login>
                <p>
                    <!--
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                    if you don't have an account.
                    -->
                    <asp:Button ID="ButtonRegister" runat="server" Text="Register!" OnClick="ButtonRegister_Click" />
                </p>
            </section>
            <section id="registerForm">
                <div id="RegisterDiv" runat="server">
                    <h2>Register now.</h2>
                    <asp:CreateUserWizard ID="CreateUserWizard" runat="server"
                        OnCreatedUser="CreateUserWizard_CreatedUser">
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td >
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="UserTypeLabel" runat="server" AssociatedControlID="UserTypeDropDownList">User Type:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="UserTypeDropDownList" runat="server" DataSourceID="Sql_UserType" DataTextField="Name" DataValueField="Id">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="Sql_UserType" runat="server" ConnectionString="<%$ ConnectionStrings:iWorkoutAzureDBConnectionString %>" SelectCommand="SELECT [Name], [Id] FROM [UserTypes]"></asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="color: Red;">
                                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </div>
            </section>
        </section>
    </div>
</asp:Content>



