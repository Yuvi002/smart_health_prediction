<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="F12_DoctorBooking.aspx.vb" Inherits="smart_health_prediction.F12_DoctorBooking" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <style>
        .div_textbox {

            position:relative;
            resize:both;
            padding-right:300px;
            padding-left:300px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

   


    <div class="container">

       <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
         
    </asp:ScriptManager>--%>
        <br />
        <br />
        <br />
        <h1 class="text-center">Book Your Appointment</h1>
        <div id="leftside">
            <fieldset>

                <div class="form">

                    <asp:Label ID="Label1" runat="server" Text="Choose a preferred date for your appointment: -" AssociatedControlID="txtappointdate"></asp:Label>

                    
                    <br />

                    <div class="div_textbox text-center">
                        <asp:TextBox ID="txtappointdate" runat="server" TextMode="date" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqAppointDate" runat="server" ErrorMessage="Required" ControlToValidate="txtappointdate"></asp:RequiredFieldValidator>
                    </div>
                    
                    <%-- Choose Your Appointment time --%>
                    <asp:Label ID="lblChooseTime" runat="server" AssociatedControlID="ddlconsultationtime" Text="Choose Your Appointment time"></asp:Label>
                    <br />
                    <div class="input-group">
                        <asp:DropDownList ID="ddlconsultationtime" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqConsultationTime" runat="server" ErrorMessage="Required" ControlToValidate="ddlconsultationtime"></asp:RequiredFieldValidator>
                    </div>

                    <br />
                    
                    <%-- Choose your Doctor Medical Field --%>
                    <asp:Label ID="lbldoctorfield" runat="server" AssociatedControlID="ddlDoctorField" Text="Choose your Doctor Medical Field"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlDoctorField" runat="server"  ></asp:DropDownList>
                    </div>
                    <%--OnSelectedIndexChanged="ddlDoctorField_SelectedIndexChanged1"--%>
                    <br />

                    <%-- Choose your Doctor --%>
                    <asp:Label ID="lblDoctorName" runat="server" AssociatedControlID="ddldoctorname" Text="Choose your Doctor"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddldoctorname" runat="server" ></asp:DropDownList>
                    </div>

                     <%--OnSelectedIndexChanged="ddlDoctorField_SelectedIndexChanged1"--%>

                     <br />

                    <%--Choose your Lab Technician--%>
                    <asp:Label ID="lbltech" runat="server" AssociatedControlID="ddllabtech" Text="Choose your Lab Technician"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddllabtech" runat="server"></asp:DropDownList>
                    </div>

                    <br />
                    <br />

                    <%-- Status --%>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddllabtech" Text=""></asp:Label>

                    <br />
                    <br />

                    <%-- Button Set --%>
                    <asp:Button ID="btnBook" class="btn btn-outline-info" runat="server" Text="Book Appointment" />
                    <asp:Button ID="btnClear" OnClick="btnClear_Click" class="btn btn-outline-danger" runat="server" Text="Clear" />

                    <br />
                </div>
            </fieldset>
        </div>
         
    </div>
    
     <script>
         $(document).ready(function () {
             $(function () {
                 var todayDate = new Date();
                 var month = todayDate.getMonth() + 1
                 var year = todayDate.getUTCFullYear();
                 var tdate = todayDate.getDate();

                 if (month < 10) {
                     month = '0' + month.toString();
                 }
                 if (tdate < 10) {
                     tdate = '0' + tdate.toString();
                 }
                 var maxDate = tdate + "-" + month + "-" + year;
                 //$('#txtappointdate').attr('min', maxDate);
                 document.getElementById("txtappointdate").setAttribute('min', maxDate);
                 console.log(maxDate);
             })
         })
     </script>
</asp:Content>

