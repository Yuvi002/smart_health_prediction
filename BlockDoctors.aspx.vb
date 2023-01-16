Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class BlockDoctors
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString =
WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        getActiveUsers()
        If (IsNothing(Session("admUsername"))) Then
            Response.Redirect("~/Dashboard.aspx")

        End If

    End Sub

    Private Sub getActiveUsers()
        Dim dbconn As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select Doctor_Id, Firstname, Lastname, Username, Profile_Pic from tblDoctor  where status=1"
        cmd.Connection = dbconn
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da.Fill(dt)
        gvsDoc.DataSource = dt
        gvsDoc.DataBind()
    End Sub

    Protected Sub gvs_PreRender(sender As Object, e As EventArgs)

        If (gvsDoc.Rows.Count > 0) Then
            'This replaces <td> with <th> and adds the scope attribute
            gvsDoc.UseAccessibleHeader = True
            'This will add the <thead> and <tbody> elements
            gvsDoc.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

    Protected Sub lnkblock_Click(sender As Object, e As EventArgs)

        'Retrieving the PatientID from the command argument link button 
        Dim docname As Integer = Convert.ToInt32(CType(sender, LinkButton).CommandArgument)
        Dim dbconn As New SqlConnection(_conString)
        'open Connection
        dbconn.Open()
        'Create Command
        Dim ucmd As New SqlCommand()
        ucmd.CommandType = CommandType.Text
        ucmd.CommandText = "update tblDoctor set status='0' Where Doctor_Id ='" & docname & "'"
        ucmd.Connection = dbconn
        ucmd.ExecuteNonQuery()
        dbconn.Close()
        getActiveUsers()

    End Sub
End Class