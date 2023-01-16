<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="TimeDashboard.aspx.vb" Inherits="smart_health_prediction.TimeDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .code{
            color: darkblue;
            font-size: 40px;
            background-color:aqua;
            font: bold;
            
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

     <div runat="server" style= "background-color:burlywood; margin:0; padding:10px; background-size:cover;">

    <div style="padding:70px;" >
        <asp:Label ID="lblMsg" runat="server" Text=" " CssClass="text-success"></asp:Label>
        <hr />
        <div class="code">
        Add, Update, Delete Records Consultation Time using GiridView</div><br /><br />
        <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTimeId" Font-Bold="true" ForeColor="Black" runat="server" Text="Time ID"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTimeId" runat="server" CssClass="form-control" Enabled="false" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTime" Font-Bold="true" ForeColor="Black" runat="server" Text="Time"> </asp:Label>
                </td>
                <td><br />
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" Text="*"
                        ControlToValidate="txtTime" ForeColor="Red" ValidationGroup="vgAdd" />
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:Button ID="btnInsert" BackColor="#3399ff" ForeColor="Black" runat="server"
                        Text="Insert" OnClick="btnInsert_Click" ValidationGroup="vgAdd" CssClass="btn btn-outline-primary" />
                    <asp:Button ID="btnUpdate" runat="server"
                        Text="Update" BackColor="#3399ff" ForeColor="Black" OnClick="btnUpdate_Click1"  ValidationGroup="vgAdd" CssClass="btn btn-outline-warning" />
                    <asp:Button ID="btnDelete" BackColor="Red" ForeColor="Black" OnClick="btnDelete_Click" runat="server" CssClass="btn btn-outline-danger"
                        OnClientClick="return confirm('Are you sure you want to delete this Time Session?')"
                        Text="Delete" ValidationGroup="vgAdd" />
                    <asp:Button ID="btnCancel" BackColor="Yellow" ForeColor="Black" runat="server"
                        Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-outline-success" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <!-- set the primary for the category table as the DataKeynames-->
        <asp:GridView ID="gvsTime" OnSelectedIndexChanged="gvsTime_SelectedIndexChanged" OnPageIndexChanging="gvsTime_PageIndexChanging" PageSize="4" AllowPaging="true" DataKeyNames="constimeid" AutoGenerateColumns="false"
            Width="500" runat="server">
            <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
            <AlternatingRowStyle BackColor="#f5f5f5" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Time Sessions">
                    <ItemTemplate>
                        <!-- display the category name -->
                        <asp:Label ID="lblSessionTime" Text='<%#Eval("constimetime")%>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
        </div>

</asp:Content>
