<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="Dashboard.aspx.vb" Inherits="smart_health_prediction.Dashboard" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

     <div style= "background-image: url('/assets/img/Wallpaper2.jpeg'); height: 100vh; padding: 0; margin:0; background-size:cover; background-repeat:no-repeat" >

    <h1 style="color:black;padding:5%; margin-left:15%; " >Welcome to Smart Health Prediction System</h1>
        5
         <div class="col-sm-6" style="position:center; float:right; height: 7vh; margin:25%; margin-top: 5%; background: rgba(255,255,255, 0.3); display:flex; align-items: center; border-radius: 60px; padding: 10px 20px; ">

                <div class="input-group">
                    <%--generate text changed event--%>
                    <asp:TextBox ID="TextBox1" OnTextChanged="TextBox1_TextChanged"  runat="server" AutoPostBack="true"
                        CssClass="form-control" style="background:transparent; border:hidden" Placeholder="Search your symptom..." />
                    <span class="input-group-addon">
                        <i class="fa fa-search" style="color:#000000; font: bold; font-size:25px; padding: 25%;"></i></span>
                </div>
            </div>
         </div>
</asp:Content>
 

       