﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="RegBodyLogIn.aspx.vb" Inherits="smart_health_prediction.RegBodyLogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%@ Register TagPrefix="user" TagName="login" Src="~/LoginCrel.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

      <div id="container" style="padding:50px" class="container" >
        <div id="leftSide">
            <fieldset>
                <legend>Regulatoring Body Login</legend>
                <div class="form">
                    <user:login ID="userLogin" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click"
                        CssClass="btn btn-outline-primary" Text="Log in" />
                </div>
            </fieldset>
            <fieldset>
                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label><br />
                <asp:Button ID="btnRegister" runat="server"
                    PostBackUrl="~/RegBodyDashboard.aspx" Text="Don’t have an account yet? Join now"
                    CausesValidation="false" CssClass="btn btn-outline-warning" /><br />
            </fieldset>
        </div>
    </div>

</asp:Content>