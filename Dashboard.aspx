<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="Dashboard.aspx.vb" Inherits="smart_health_prediction.Dashboard" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/Dashboard.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">  

     <div style= "background-image: url('/assets/img/Wallpaper2.jpeg'); height: 100vh; padding: 0; margin:0; background-size:cover; background-repeat:no-repeat" >

    <h1 ID="Welcoming"  >Welcome to Smart Health Prediction System</h1>

         <div class="col-sm-6" style="position:center; float:right; height: 7vh; margin:25%; margin-top: 5%; background: rgba(255,255,255, 0.3); display:flex; align-items: center; border-radius: 60px; padding: 10px 20px; ">
                <div class="input-group">
                    <%--generate text changed event--%>
                    <asp:TextBox ID="TextBox1"  runat="server" AutoPostBack="true"
                        CssClass="form-control" style="background:transparent; border:hidden" Placeholder="Search your symptom..." />
                    
                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="btn btn-no-outline-dark" CommandArgument='<%#Eval("Info_ID") %>' ToolTip="Search Symptoms" ><span class="fa fa-search"></span></asp:LinkButton>
                    <hr />
                    
                    <asp:GridView ID="gvsSymptoms" Visible="false" runat="server" AutoGenerateColumns="True" AllowPaging="true" OnRowDataBound="gvsSymptoms_RowDataBound" OnPageIndexChanging="gvsSymptoms_PageIndexChanging">         
                        <Columns>
                            <asp:BoundField HeaderStyle-Width="150px" DataField="Info_Name" HeaderText="Symptoms Name" ItemStyle-CssClass="Info_Name" />
                            <asp:BoundField  />

                            <asp:BoundField HeaderStyle-Width="150px" DataField="Info_Spec" HeaderText="Symptoms Specification" ItemStyle-CssClass="Info_Spec" />
                            
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
         </div>

    <%--<asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
            <hr />
            <asp:ListView ID="lvInfo" runat="server" ItemPlaceholderID="itemPlaceholder"  DataKeyNames="Info_ID" >
            <ItemTemplate>
                <div class="card h-100" style="width: 195px; float: left; margin: 8px;">
                    <div class="card-body">
                        <h6 class="card-title"><%#Eval("Info_Spec") %></h6>
                        <p class="card-text">
                        </p>
                        <asp:LinkButton ID="LinkButton1"   runat="server" Text="Book Appointment" CommandName="btnBook" CssClass="btn btn-danger"
                             href="Features/BookAppointment.aspx"
                        />
                    </div>
                </div>
            </ItemTemplate>



                </asp:ListView>
        </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />
    </Triggers>
    </asp:UpdatePanel>--%>

    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
</asp:Content>
  
 
       