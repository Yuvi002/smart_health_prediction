Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography


Public Class TimeDashboard
    Inherits System.Web.UI.Page
    Private ReadOnly _conString As String

    Public Sub New()
        _conString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""
        If Not IsPostBack Then
            BindTimeData()
        End If

        If (IsNothing(Session("regName"))) Then
            Response.Redirect("~/Dashboard.aspx")

        Else
            lblMsg.Text = " "
        End If
        btnDelete.Visible = False
        btnUpdate.Visible = False

    End Sub

    Private Sub BindTimeData()
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM tblConsultationTime"
        cmd.Connection = sqlCon
        Dim da As New SqlDataAdapter(cmd)
        'Create a DataTable
        Dim dt As New DataTable
        Using (da)
            'Populate the DataTable
            da.Fill(dt)
        End Using
        'Set the DataTable as the DataSource
        gvsTime.DataSource = dt
        gvsTime.DataBind()
    End Sub

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs)

        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        'search for category name from tblcategory 
        cmd.CommandText = "select Cat_Name from tblCategory where Cat_Name=@catname"
        'create a parameterized query
        cmd.Parameters.AddWithValue("@catname", txtTime.Text.Trim())
        'Create DataReader
        Dim myReader As SqlDataReader
        con.Open()
        myReader = cmd.ExecuteReader

        'Check if username already exists in the DB
        If (myReader.HasRows) Then
            lblMsg.Text = "Session Time Already Exist, Please Choose Another"
            lblMsg.ForeColor = System.Drawing.Color.Red
            txtTime.Focus()
        Else
            'Ensure the DataReader is closed
            myReader.Close()
            Dim IsAdded As Boolean = False
            'Add built-in function to remove spaces from Textbox Category name
            Dim TimeName As String = txtTime.Text
            Dim sqlCon As New SqlConnection(_conString)
            Dim cmd1 As New SqlCommand()
            'add INSERT statement to create new category name
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into tblConsultationTime (constimetime) values (@constimetime)"

            'create one Parameterized query to prevent sql injection by
            cmd.Parameters.AddWithValue("@constimetime", txtTime.Text.Trim())
            'using above String name
            cmd.Connection = sqlCon
            Try
                sqlCon.Open()

                'use Command method to execute INSERT statement and return 
                'boolean if number of records inserted is greater than zero
                IsAdded = cmd.ExecuteNonQuery() > 0
                lblMsg.Text = "'" & TimeName & "' Time Session added successfully!"
                lblMsg.ForeColor = System.Drawing.Color.Green
                'Refresh the GridView by calling the BindCategoryData()
                BindTimeData()
            Catch ex As Exception
                lblMsg.Text = "Error while adding '" & TimeName & "' category"
                lblMsg.ForeColor = System.Drawing.Color.Red
            Finally
                sqlCon.Close()
            End Try

            ResetAll()
        End If

    End Sub

    Protected Sub btnUpdate_Click1(sender As Object, e As EventArgs)

        'check whether the categoryid textbox is empty
        If String.IsNullOrEmpty(txtTime.Text.Trim()) Then
            lblMsg.Text = "Please select record to update"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsUpdated As Boolean = False
        Dim TimeID As Integer = Convert.ToInt32(txtTimeId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim TimeName As String = txtTime.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add UPDATE statement to update category name for the above CatID
        cmd.CommandText = "update tblConsultationTime set constimetime = @constimetime where constimeid = @constimeid"
        'Create two parameterized queries [CatID and CatName]
        cmd.Parameters.AddWithValue("@constimeid", txtTimeId.Text.Trim())
        cmd.Parameters.AddWithValue("@constimetime", txtTime.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsUpdated = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & TimeName & "' Time Session updated successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindTimeData()
        Catch ex As Exception
            lblMsg.Text = "Error while updating '" & TimeName & "' category"
            lblMsg.ForeColor = System.Drawing.Color.Red
        End Try
        sqlCon.Close()
        'use Command method to execute UPDATE statement and return 
        'boolean if number of records UPDATED is greater than zero
        'Ensure that no rows are selected in Gridview by changing the EditIndex
        ResetAll()

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)

        'check whether the categoryid textbox is empty
        If String.IsNullOrEmpty(txtTime.Text.Trim()) Then
            lblMsg.Text = "Please select record to delete"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsDeleted As Boolean = False
        Dim TimeID As Integer = Convert.ToInt32(txtTimeId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim TimeName As String = txtTime.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add DELETE statement to delete the selected category for the above CatID
        cmd.CommandText = "delete from tblConsultationTime where constimeid=@constimeid"
        'Create a parametererized query for CatID
        cmd.Parameters.AddWithValue("@constimeid", txtTimeId.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsDeleted = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & TimeName & "' category deleted successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindTimeData()
        Catch ex As Exception
            lblMsg.Text = "Error while deleting '" & TimeName & txtTime.Text
            lblMsg.ForeColor = System.Drawing.Color.Red
        End Try
        sqlCon.Close()
        'use Command method to execute DELETE statement and return 
        'boolean if number of records DELETED is greater than zero

        ResetAll()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)

        ResetAll()

    End Sub

    Private Sub ResetAll()
        btnInsert.Visible = True
        txtTimeId.Text = ""
        txtTime.Text = ""
    End Sub

    Protected Sub gvsTime_SelectedIndexChanged(sender As Object, e As EventArgs)

        'Read data from GridView and Populate the form
        txtTimeId.Text = gvsTime.DataKeys(gvsTime.SelectedRow.RowIndex).Value.ToString()
        txtTime.Text = (TryCast(gvsTime.SelectedRow.FindControl("lblSessionTime"),
Label)).Text
        'Hide Insert button during update/delete
        btnInsert.Visible = False
        btnUpdate.Visible = True
        btnDelete.Visible = True
    End Sub

    Protected Sub gvsTime_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvsTime.PageIndex = e.NewPageIndex
        BindTimeData()
    End Sub
End Class