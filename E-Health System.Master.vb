Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration

Public Class E_Health_System
    Inherits System.Web.UI.MasterPage
    Private _conString As String = ConfigurationManager.ConnectionStrings("MedicalCS").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            If (Not IsNothing(Request.Cookies("auname")) And Not IsNothing(Request.Cookies("admPass"))) Then
                ucadminlogin.Username = Request.Cookies("auname").Value
                ucadminlogin.Password = Request.Cookies("admPass").Value
            End If
        End If

        'Admin  
        If (Not IsNothing(Session("admUsername"))) Then
            regist.Visible = False
            LgRegis.CssClass = "nav navbar-nav navbar-right"
            lbllgged.CssClass = "btn btn-outline-success text-white"
            lbllgged.Text = "Welcome " + Session("admUsername")
            lbllgged.CssClass = "btn btn-outline-success text-black"
            btnlgOut.Visible = True
            dropdown06.Visible = False
            'pnlmanage.Visible = True
            'pnlmanagemov.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlmanagemov)
            'pnlprofile.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlprofile)
            'pnllog.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnllog)
        End If

        'Patient
        If (Not IsNothing(Session("pat_uname"))) Or (Not IsNothing(Session("pat_id"))) Then
            regist.Visible = False
            LgRegis.CssClass = "nav navbar-nav navbar-right"
            lbllgged.CssClass = "btn btn-outline-success text-white"
            lbllgged.Text = "Welcome " + Session("pat_uname")
            lbllgged.CssClass = "btn btn-outline-success text-black"
            btnlgOut.Visible = True
            dropdown06.Visible = False
            'pnlmanage.Visible = True
            'pnlmanagemov.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlmanagemov)
            'pnlprofile.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlprofile)
            'pnllog.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnllog)

            'Retrieving Patients Session
            Dim patient_id As Integer = Convert.ToInt32(Session("pid"))


        End If

        'Doctor
        If (Not IsNothing(Session("doc_uname"))) Then
            regist.Visible = False
            LgRegis.CssClass = "nav navbar-nav navbar-right"
            lbllgged.CssClass = "btn btn-outline-success text-white"
            lbllgged.Text = "Welcome " + Session("doc_uname")
            lbllgged.CssClass = "btn btn-outline-success text-black"
            btnlgOut.Visible = True
            dropdown06.Visible = False
            'pnlmanage.Visible = True
            'pnlmanagemov.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlmanagemov)
            'pnlprofile.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnlprofile)
            'pnllog.Style.Add("visibility", "hidden")
            'Page.Controls.Remove(pnllog)
        End If



    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        'get the value of username and password fields and state of checkbox from
        'admin login form
        Dim username As String = ucadminlogin.Username
        Dim password As String = ucadminlogin.Password
        Dim chk As Boolean = ucadminlogin.Chk
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        'searching for a record containing matching username & password with an active status
        cmd.CommandText = "select * from tblAdmin where Adm_username=@un and Adm_password=@pass and Adm_status=1"
        'create two parameterized query for the above select statement
        cmd.Parameters.AddWithValue("@un", username)
        cmd.Parameters.AddWithValue("@pass", password)
        'use above variables
        Dim myReader As SqlDataReader
        con.Open()
        myReader = cmd.ExecuteReader
        'check if the DataReader contains a record
        If (myReader.HasRows) Then
            If myReader.Read Then
                'create a memory cookie to store username and pwd
                Response.Cookies("auname").Value = username
                Response.Cookies("admPass").Value = password
                If (chk) Then
                    'if checkbox is checked, make cookies persistent
                    Response.Cookies("auname").Expires = DateAndTime.Now.AddDays(100)
                    Response.Cookies("admPass").Expires = DateAndTime.Now.AddDays(100)
                Else
                    'delete the cookies if checkbox is unchecked
                    Response.Cookies("auname").Expires = DateAndTime.Now.AddDays(-100)
                    Response.Cookies("admPass").Expires = DateAndTime.Now.AddDays(-100)
                    'delete content of password field
                End If
                'create and save adminuname in a session variable
                Session("admUsername") = username
                'create and save adminid in a session variable
                Session("admid") = myReader("Adm_id").ToString()
                'redirect to the dashboard page
                Response.Redirect("~/Dashboard")
                con.Close()
            End If
        Else

            'delete content of password field
            lblmsg.Style.Add("margin-left", "10%")
            lblmsg.ForeColor = Drawing.Color.Red
            username = ""
            password = ""
            lblmsg.Text = "You are not registered or your account has been suspended!"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop",
"adminModal();", True)
        End If
    End Sub


    Sub btnlgOut_Click(sender As Object, e As EventArgs)
        logout()
    End Sub

    Private Sub logout()
        If (Not IsNothing(Session("admUsername"))) Or (Not IsNothing(Session("doc_uname"))) Or (Not IsNothing(Session("pat_uname"))) Then
            'Remove all session
            Session.RemoveAll()
            'Destroy all Session objects
            Session.Abandon()
            'Redirect to homepage or login page
            Response.Redirect("~/Dashboard")
        End If
    End Sub
End Class