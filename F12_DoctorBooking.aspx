<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="F12_DoctorBooking.aspx.vb" Inherits="smart_health_prediction.F12_DoctorBooking" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style>
        #gvsViewPatientBooking {
            width: 100%;
        }

        th {
            background: #494e5d;
            color: chartreuse;
        }
    </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

   


    <div class="border-dark" style="padding:5px; padding-top:2em;" >
        <div style="padding:50px;"  >
        <h1 class="text-center">Book Your Appointment</h1>
        <div id="center">
            <fieldset>
                <div class="form">

                    <div class="div_textbox text-center">
                        <asp:TextBox ID="txtApp_id" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                    </div>

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
                        <asp:DropDownList ID="ddlDoctorField" OnSelectedIndexChanged="ddlDoctorField_SelectedIndexChanged" runat="server"  ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqDoctorField" runat="server" ErrorMessage="Required" ControlToValidate="ddlDoctorField"></asp:RequiredFieldValidator>
                    </div>
                   
                    <br />

                 
                    <asp:Label ID="lblDoctorName" runat="server" AssociatedControlID="ddldoctorname" Text="Choose your Doctor"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddldoctorname" OnSelectedIndexChanged="ddlDoctorField_SelectedIndexChanged" runat="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqdoctorname" runat="server" ErrorMessage="Required" ControlToValidate="ddldoctorname"></asp:RequiredFieldValidator>
                    </div>

                   

                     <br />

                    <%--Choose your Lab Technician--%>
                    <asp:Label ID="lbltech" runat="server" AssociatedControlID="ddllabtech" Text="Choose your Lab Technician"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddllabtech" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqtech" runat="server" ErrorMessage="Required" ControlToValidate="ddllabtech"></asp:RequiredFieldValidator>
                    </div>

                   
                    <br />

                    <%-- Status --%>
                    <asp:Label ID="lblstatus" runat="server" AssociatedControlID="ddllabtech" Text=""></asp:Label>

                    <%-- Button Set --%>
                    <div class="container">
                    <asp:Button ID="btnBook" OnClick="btnBook_Click" class="btn btn-outline-info" runat="server" Text="Book Appointment" />
                      
                    <asp:Button ID="btnClear" OnClick="btnClear_Click" class="btn btn-outline-danger" runat="server" Text="Clear" />
                    <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                        
                        </div>
                </div>
            </fieldset>
                </div>
        </div>

        <div style="padding:50px;">
    <asp:GridView ID="gvsViewPatientBooking" OnPreRender="gvsViewPatientBooking_PreRender" CssClass="table table-striped table-bordered"   OnPageIndexChanging="gvsViewPatientBooking_PageIndexChanging" DataKeyNames="App_id" OnSelectedIndexChanged="gvsViewPatientBooking_SelectedIndexChanged" AutoGenerateColumns="false" ClientIDMode="Static" PageSize="3" AllowPaging="true" Width="800" runat="server">
        <HeaderStyle BackColor="#eeeeee" ForeColor="White" Font-Bold="true"
            Height="30" />
      <AlternatingRowStyle BackColor="#f5f5f5" />
     <Columns>
        <asp:BoundField DataField="App_dateregistered" HeaderText="Appointment Date" />
            <asp:BoundField DataField="App_time" HeaderText="Appointment Time" />
         <asp:BoundField DataField="App_status" HeaderText="Appointment Status" />
         </Columns>
    </asp:GridView>
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

