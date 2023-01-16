<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="drmngbooking.aspx.vb" Inherits="smart_health_prediction.drmngbooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #gvsAcceptBooking {
            width: 100%;
        }

        th {
            background: #494e5d;
            color: chartreuse;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div style="padding:80px;">

    <div class="row">
        <div class="col-lg-12">
            <h2>Search and Accept/Delete Bookings:</h2>
        </div>
    </div>

     <asp:GridView ID="gvsAcceptBooking" OnPreRender="gvsAcceptBooking_PreRender" ClientIDMode="Static" CssClass="table table-striped table-bordered"
        runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="App_dateregistered" HeaderText="Appointment Date" />
            <asp:BoundField DataField="App_time" HeaderText="Appointment Time" />
            <asp:BoundField DataField="Doctor_Id" HeaderText="Doctor Id" />
            <asp:BoundField DataField="Patient_Id" HeaderText="Patient Id" />
           
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%-- Assign the User_Id to the link button --%>
                    <asp:HiddenField ID="hiddoctor" runat="server" Value='<%#Eval("Doctor_Id") %>' />
                    <asp:HiddenField ID="hidpatient" runat="server" Value='<%#Eval("Patient_Id") %>' />
                    <asp:LinkButton ID="lnkAccept" OnClick="lnkAccept_Click" CssClass="btn btn-outline-warning"
                        runat="server" CommandArgument='<%# Eval("App_id") %>'>Confirm Online Booking</asp:LinkButton>
                     <asp:LinkButton ID="lnkDeny" OnClick="lnkDeny_Click" CssClass="btn btn-outline-warning"
                        runat="server" CommandArgument='<%# Eval("App_id") %>'>Deny </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

        </div>

</asp:Content>
