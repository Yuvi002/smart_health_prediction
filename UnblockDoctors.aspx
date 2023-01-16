<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="UnblockDoctors.aspx.vb" Inherits="smart_health_prediction.UnblockDoctors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style>
        #gvs {
            width: 100%;
        }

        th {
            background: #494e5d;
            color: white;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div style="padding:80px;">
    <div class="row">
        <div class="col-lg-12">
            <h2>Search and Unblock unactive Doctors:</h2>
        </div>
    </div>
    <asp:GridView ID="gvsUn" OnPreRender="gvsUn_PreRender"  ClientIDMode="Static" CssClass="table table-striped table-bordered"
        runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Firstname" HeaderText="First Name" />
            <asp:BoundField DataField="Lastname" HeaderText="Last Name" />
            <asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:ImageField DataImageUrlField="Profile_Pic" ControlStyle-Width="50"
                DataImageUrlFormatString="~/images/{0}" HeaderText="Profile Pic" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%-- Assign the User_Id to the link button --%>
                    <asp:LinkButton ID="lnkblock" OnClick="lnkblock_Click"  CssClass="btn btn-outline-warning"
                        runat="server" CommandArgument='<%# Eval("Doctor_Id") %>'>Unblock</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        </div>
</asp:Content>
