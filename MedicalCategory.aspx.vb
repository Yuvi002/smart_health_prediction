Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography


Public Class MedicalCategory
    Inherits System.Web.UI.Page
    Private ReadOnly _conString As String

    Public Sub New()
        _conString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""
        If Not IsPostBack Then
            BindMedCateg()
        End If

        If (IsNothing(Session("regName"))) Then
            Response.Redirect("~/Dashboard.aspx")

        Else
            lblMsg.Text = " "
        End If
        btnDelete.Visible = False
        btnUpdate.Visible = False

    End Sub

    Private Sub BindMedCateg()
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM tblDoctorRole"
        cmd.Connection = sqlCon
        Dim da As New SqlDataAdapter(cmd)
        'Create a DataTable
        Dim dt As New DataTable
        Using (da)
            'Populate the DataTable
            da.Fill(dt)
        End Using
        'Set the DataTable as the DataSource
        gvsMedCategory.DataSource = dt
        gvsMedCategory.DataBind()
    End Sub

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs)

        Dim con3 As New SqlConnection(_conString)
        Dim cmd3 As New SqlCommand()
        cmd3.CommandType = CommandType.Text
        cmd3.Connection = con3
        'search for category name from tblcategory 
        cmd3.CommandText = "select RoleName from tbldoctorRole where RoleName=@RoleName"
        'create a parameterized query
        cmd3.Parameters.AddWithValue("@RoleName", txtMedName.Text.Trim())
        'Create DataReader
        Dim myReader1 As SqlDataReader
        con3.Open()
        myReader1 = cmd3.ExecuteReader

        'Check if username already exists in the DB
        If (myReader1.HasRows) Then
            lblMsg.Text = " Medical Category Name Already Exist, Please Choose Another"
            lblMsg.ForeColor = System.Drawing.Color.Red
            txtMedName.Focus()
        Else
            'Ensure the DataReader is closed
            myReader1.Close()
            Dim IsAdded As Boolean = False
            'Add built-in function to remove spaces from Textbox Category name
            Dim MedCategName As String = txtMedName.Text
            Dim sqlCon As New SqlConnection(_conString)
            Dim cmd1 As New SqlCommand()
            'add INSERT statement to create new category name
            cmd3.CommandType = CommandType.Text
            cmd3.CommandText = "insert into tblDoctorRole (RoleName) values (@RoleName)"

            'create one Parameterized query to prevent sql injection by
            cmd3.Parameters.AddWithValue("@RoleName", txtMedName.Text.Trim())
            'using above String name
            cmd3.Connection = sqlCon
            Try
                sqlCon.Open()

                'use Command method to execute INSERT statement and return 
                'boolean if number of records inserted is greater than zero
                IsAdded = cmd3.ExecuteNonQuery() > 0
                lblMsg.Text = "'" & MedCategName & "' Medical category added successfully!"
                lblMsg.ForeColor = System.Drawing.Color.Green
                'Refresh the GridView by calling the BindCategoryData()
                BindMedCateg()
            Catch ex As Exception
                lblMsg.Text = "Error while adding '" & MedCategName & "' category"
                lblMsg.ForeColor = System.Drawing.Color.Red
            Finally
                sqlCon.Close()
            End Try

            ResetAll()
        End If

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)

        'check whether the categoryid textbox is empty
        If String.IsNullOrEmpty(txtMedName.Text.Trim()) Then
            lblMsg.Text = "Please select Medical record to update"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsUpdated As Boolean = False
        Dim MedCategID As Integer = Convert.ToInt32(txtMedCategId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim MedCategName As String = txtMedName.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add UPDATE statement to update category name for the above CatID
        cmd.CommandText = "update tblDoctorRole set RoleName = @RoleName where RoleID = @RoleID"
        'Create two parameterized queries [CatID and CatName]
        cmd.Parameters.AddWithValue("@RoleID", txtMedCategId.Text.Trim())
        cmd.Parameters.AddWithValue("@RoleName", txtMedName.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsUpdated = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & MedCategName & "' Medical category updated successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindMedCateg()
        Catch ex As Exception
            lblMsg.Text = "Error while updating '" & MedCategName & "' category"
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
        If String.IsNullOrEmpty(txtMedName.Text.Trim()) Then
            lblMsg.Text = "Please select Medical record to delete"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsDeleted As Boolean = False
        Dim MedCategID As Integer = Convert.ToInt32(txtMedCategId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim MedCategName As String = txtMedName.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add DELETE statement to delete the selected category for the above CatID
        cmd.CommandText = "delete from tblDoctorRole where RoleID=@RoleID"
        'Create a parametererized query for CatID
        cmd.Parameters.AddWithValue("@RoleID", txtMedCategId.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsDeleted = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & MedCategName & "' Medical category deleted successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindMedCateg()
        Catch ex As Exception
            lblMsg.Text = "Error while deleting '" & MedCategName & txtMedName.Text
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
        txtMedCategId.Text = ""
        txtMedName.Text = ""
    End Sub

    Protected Sub gvsMedCategory_SelectedIndexChanged(sender As Object, e As EventArgs)

        'Read data from GridView and Populate the form
        txtMedCategId.Text = gvsMedCategory.DataKeys(gvsMedCategory.SelectedRow.RowIndex).Value.ToString()
        txtMedName.Text = (TryCast(gvsMedCategory.SelectedRow.FindControl("lblMedCateg"),
Label)).Text
        'Hide Insert button during update/delete
        btnInsert.Visible = False
        btnUpdate.Visible = True
        btnDelete.Visible = True

    End Sub

    Protected Sub gvsMedCategory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        gvsMedCategory.PageIndex = e.NewPageIndex
        BindMedCateg()

    End Sub
End Class