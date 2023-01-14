Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class drmngbooking
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString =
WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsNothing(Session("docname"))) Then
            Response.Redirect("~/DoctorLogin.aspx")
        End If

        getPendingBooking()

    End Sub

    Private Sub getPendingBooking()
        Dim dbconn As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select App_id, App_dateregistered, App_time, RoleID, Doctor_Id, lbtech_Id, Patient_Id from tblAppointment where App_status = 0"
        cmd.Connection = dbconn
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da.Fill(dt)
        gvsAcceptBooking.DataSource = dt
        gvsAcceptBooking.DataBind()
    End Sub

    Protected Sub gvsAcceptBooking_PreRender(sender As Object, e As EventArgs)
        If (gvsAcceptBooking.Rows.Count > 0) Then
            'This replaces <td> with <th> and adds the scope attribute
            gvsAcceptBooking.UseAccessibleHeader = True
            'This will add the <thead> and <tbody> elements
            gvsAcceptBooking.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)
        'Retrieving the RecruiterID from the command argument link button 
        Dim docid As Integer = Convert.ToInt32(CType(sender, LinkButton).CommandArgument)
        Dim dbconn As New SqlConnection(_conString)
        'open Connection
        dbconn.Open()
        'Create Command
        Dim ucmd As New SqlCommand()
        ucmd.CommandType = CommandType.Text
        ucmd.CommandText = "update tblAppointment set App_status='1' Where App_id ='" & docid & "'"
        ucmd.Connection = dbconn
        ucmd.ExecuteNonQuery()
        dbconn.Close()
        getPendingBooking()
    End Sub
End Class