<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/E-Health System.Master" CodeBehind="PatientRegistration.aspx.vb" Inherits="smart_health_prediction.PatientRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container">
        <h1 class="text-center" style="color: aquamarine">Patient's Registration</h1>
        <br />
        <br />
        <div id="leftSide">
            <fieldset>
                <legend>Personal Details</legend>

                <div class="form">
                    <asp:Label ID="lblFname" runat="server" Text="First Name:" AssociatedControlID="txtFname"></asp:Label>
                    <div class="div_texbox">
                        <asp:TextBox ID="txtFname" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqFname" runat="server" ControlToValidate="txtFname" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>

                    <asp:Label ID="lblLname" runat="server" Text="Last Name:" AssociatedControlID="txtLname"></asp:Label>
                    <div class="div_texbox">
                        <asp:TextBox ID="txtLname" runat="server"
                            CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqLname" ControlToValidate="txtLname" runat="server" ErrorMessage="Lastname field is required"></asp:RequiredFieldValidator>

                    </div>

                    <asp:Label ID="lblRole" runat="server" Text="Role:" AssociatedControlID="txtRole"></asp:Label>
                    <div class="div_texbox">
                        <asp:TextBox ID="txtRole" runat="server"
                            CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRole" runat="server" ErrorMessage="Role field is required"></asp:RequiredFieldValidator>

                    </div>


                    <asp:Label ID="lblGender" runat="server" Font-Bold="true" Text="Gender:"></asp:Label>
                    <div class="div_texbox">

                        <div class="col-md-10">
                            <div class="form-check-inline">
                                <asp:RadioButtonList ID="rbtngen" runat="server">
                                    <asp:ListItem Value="1" Selected="True" Text="Vacant"> Male</asp:ListItem>
                                    <asp:ListItem Value="0" Text="Occupied"> Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>


                    <%--<asp:Label ID="lblGender" runat="server" Text="Gender:" AssociatedControlID="txtGender"></asp:Label>
                    <div class="div_texbox">
                        <asp:TextBox ID="txtGender" runat="server"
                            CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqGender" ControlToValidate="txtGender" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                </div>
                

                <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth" AssociatedControlID="txtDob"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtDob" TextMode="Date" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqDob" runat="server" ErrorMessage="dob is required" ControlToValidate="txtDob"></asp:RequiredFieldValidator><br />
                    <%--<asp:RegularExpressionValidator ID="RegDob" runat="server" ControlToValidate="txtDob"
                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                            ErrorMessage="Date must be dd/mm/yyyy"></asp:RegularExpressionValidator>--%>
                    <%--<asp:RangeValidator ID="rvDob" runat="server"
                        ControlToValidate="txtDob"
                        CssClass="text-danger"
                        Text="Out of range"
                        Display="Dynamic"
                        Type="Date"
                        SetFocusOnError="true"
                        ErrorMessage="You should be 18 to 45 years old"></asp:RangeValidator>--%>
                </div>
            </fieldset>
        </div>

        <fieldset>
            <legend>Contact Details</legend>
            <div class="form">

                <asp:Label ID="lblPhoneNum" runat="server" Text="Phone Number" AssociatedControlID="txtPhoneNum"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtPhoneNum" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPhone" ControlToValidate="txtPhoneNum" runat="server" ErrorMessage="Phone Num is required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                        runat="server"
                        ControlToValidate="txtPhoneNum"
                        Display="Dynamic"
                        CssClass="text-danger"
                        SetFocusOnError="true"
                        Text="Incorrect Format"
                        ValidationExpression="5(\d{7})"
                        ErrorMessage="Number must start with 5 and the total number of digits is 8"></asp:RegularExpressionValidator>
                </div>

                <asp:Label ID="lblAddress" runat="server" Text="Address" AssociatedControlID="txtAddress"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtAddress" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqAddress" ControlToValidate="txtAddress" runat="server" ErrorMessage="Address is required"></asp:RequiredFieldValidator>
                </div>

                <asp:Label ID="lblNic" runat="server" Text="NIC:" AssociatedControlID="txtNic"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtNic" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNic" ControlToValidate="txtNic" runat="server" ErrorMessage="Nic field is required"></asp:RequiredFieldValidator>
                </div>
                

                <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                <div class="div_texbox">

                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is required"></asp:RequiredFieldValidator><br />
                   <%-- <asp:RegularExpressionValidator ID="RegEmail" runat="server" ControlToValidate="txtEmail"
                        ValidationExpression="^[a-zA-Z][\w\.-][a-zA-Z0-9]@[a-zA-Z0-9][\w\.-][a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                        ErrorMessage="Not Valid"></asp:RegularExpressionValidator>--%>
                </div>
            </div>
        </fieldset>

        <fieldset>
            <legend>Login Details</legend>
            <div class="form">

                <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtUsername"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtUsername" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" ControlToValidate="txtUsername" runat="server" ErrorMessage="Username Required"></asp:RequiredFieldValidator><br />
                  <%--  <asp:RegularExpressionValidator ID="regUsername" ControlToValidate="txtUsername"
                        ValidationExpression="^[a-zA-Z]{5,}$" runat="server"
                        ErrorMessage="Username must be minimum 5 characters"></asp:RegularExpressionValidator>--%>
                </div>

                <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" ControlToValidate="txtPassword" runat="server" ErrorMessage="password required"></asp:RequiredFieldValidator><br />
                   <%-- <asp:RegularExpressionValidator ID="regPassword" ControlToValidate="txtPassword"
                        ValidationExpression="^(?=.\d{2})(?=.[a-zA-Z]{2}).{6,}$" runat="server" ErrorMessage="Password Not Strong"></asp:RegularExpressionValidator>--%>
                </div>

                <asp:Label ID="lblCpassword" runat="server" Text="Confirm Password" AssociatedControlID="txtCpassword"></asp:Label>
                <div class="div_texbox">
                    <asp:TextBox ID="txtCpassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqCpassword" ControlToValidate="txtCpassword" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                    <asp:CompareValidator ID="conPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtCpassword"
                        ErrorMessage="Password does not match"></asp:CompareValidator>
                </div>

                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Upload Profile Picture"></asp:Label>
                <asp:FileUpload ID="FileUpload1" runat="server"
                    CssClass="form-control" />
            </div>
        </fieldset>
        <br />

        <fieldset>
            <asp:Button ID="btnSubmit" BackColor="Blue" ForeColor="Black" OnClick="btnSubmit_Click"  runat="server" CssClass="btn btn-outline-primary btn-block" Text="Register"/>
            <asp:Button ID="btnClear" BackColor="Red" ForeColor="Black" OnClick="btnClear_Click"  runat="server" CssClass="btn btn-outline-danger btn-block" Text="Clear All" CausesValidation="false" />
            <br />
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </fieldset>
    </div>

</asp:Content>





