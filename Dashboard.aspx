<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="Dashboard.aspx.vb" Inherits="smart_health_prediction.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/Dashboard.css" />
    <link href="CSS/video.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">  

     <div runat="server" style= "background-image: url('assets/img/Wallpaper2.jpeg'); height: 100vh; margin:0; padding:0; background-size:cover; background-repeat:no-repeat;">
         <div class="container" >
             <div class="row form-group" runat="server"  >
                 <div> <h1 ID="Welcoming"  >Welcome to Smart Health Prediction System</h1></div>

                 <div style="padding-right:12em;">
                      <%--Category Dropdown--%>
             <div class="col-sm-2" style="float: right; padding-top:7%; padding-left:1em;">
             <div class="input-group">
                 <asp:DropDownList ID="ddlCategory" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TextBox1_TextChanged" style="height:8vh; border:groove; border-radius: 60px; background:transparent"></asp:DropDownList>  
             </div>
         </div>

                     <%--SearchBar for Symptoms--%>
                     <div class="conatiner col-xl-7" style="float: right; align-items: center; padding-top: 7%;">
                         <div class="input-group">
                             <asp:TextBox ID="TextBox1" OnTextChanged="TextBox1_TextChanged" runat="server" AutoPostBack="true"
                                 CssClass="form-control" Style="background: transparent; border: groove; border-radius: 60px; height: 8vh; background: rgba(255,255,255, 0.3); display: flex; align-items: center;" Placeholder="Search your symptom..." />
                             <span class="input-group-addon"><i style="font-size: 2em; padding-left: 1em; margin-top: 8px" class="fa fa-search"></i></span>
                             <br />
                             <br />
                             <br /><br />

                             <div>
                                 <asp:GridView ID="gvCustomers"  OnRowCommand="gvCustomers_RowCommand" runat="server" AllowSorting="true" UpdateMode="Conditional" Style="border-color: black; position: relative; padding-right:0;" class="border border-5 col-lg-12 " AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvCustomers_RowDataBound1" OnPreRender="gvCustomers_PreRender" ClientIDMode="Static"  OnPageIndexChanging="gvCustomers_PageIndexChanging">
                                
                                 <Columns>
                                     <asp:BoundField HeaderStyle-Width="200px" DataField="Info_Name" HeaderText="Symptom Name" ItemStyle-CssClass="Symtoms Name" />
                                     <asp:BoundField HeaderStyle-Width="300px" DataField="Info_Spec" HeaderText="Symptom Specification" ItemStyle-CssClass="Symptom Specification" />
                                     <asp:ButtonField Text="Book Appointment" runat="server" CausesValidation="false" CommandName="Book Your Appointment Today" ButtonType="Link" HeaderStyle-Width="200px" HeaderText="Book Your Appointment Today" ItemStyle-CssClass="btn btn-outline-success" />
                                 </Columns>
                             </asp:GridView>
                             </div>
                         </div>
                     </div>
                 </div>
             </div> 
       </div>
     </div>
</asp:Content>
 