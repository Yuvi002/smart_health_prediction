Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography


Public Class RegBodyDashboard
    Inherits System.Web.UI.Page
    Private ReadOnly _conString As String

    Public Sub New()
        _conString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        If Not IsPostBack Then
            BindCategData()
        End If

        If (IsNothing(Session("regName"))) Then
            Response.Redirect("~/Dashboard.aspx")

        Else
            lblMsg.Text = " "
        End If
        btnDelete.Visible = False
        btnUpdate.Visible = False

    End Sub

    Private Sub BindCategData()
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM tblCategory"
        cmd.Connection = sqlCon
        Dim da As New SqlDataAdapter(cmd)
        'Create a DataTable
        Dim dt As New DataTable
        Using (da)
            'Populate the DataTable
            da.Fill(dt)
        End Using
        'Set the DataTable as the DataSource
        gvsCategory.DataSource = dt
        gvsCategory.DataBind()
    End Sub

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs)

        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        'search for category name from tblcategory 
        cmd.CommandText = "select Cat_Name from tblCategory where Cat_Name=@catname"
        'create a parameterized query
        cmd.Parameters.AddWithValue("@catname", txtCategoryName.Text.Trim())
        'Create DataReader
        Dim myReader As SqlDataReader
        con.Open()
        myReader = cmd.ExecuteReader

        'Check if username already exists in the DB
        If (myReader.HasRows) Then
            lblMsg.Text = "Category Name Already Exist, Please Choose Another"
            lblMsg.ForeColor = System.Drawing.Color.Red
            txtCategoryName.Focus()
        Else
            'Ensure the DataReader is closed
            myReader.Close()
            Dim IsAdded As Boolean = False
            'Add built-in function to remove spaces from Textbox Category name
            Dim CategName As String = txtCategoryName.Text
            Dim sqlCon As New SqlConnection(_conString)
            Dim cmd1 As New SqlCommand()
            'add INSERT statement to create new category name
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into tblCategory (Cat_Name) values (@cname)"

            'create one Parameterized query to prevent sql injection by
            cmd.Parameters.AddWithValue("@cname", txtCategoryName.Text.Trim())
            'using above String name
            cmd.Connection = sqlCon
            Try
                sqlCon.Open()

                'use Command method to execute INSERT statement and return 
                'boolean if number of records inserted is greater than zero
                IsAdded = cmd.ExecuteNonQuery() > 0
                lblMsg.Text = "'" & CategName & "' category added successfully!"
                lblMsg.ForeColor = System.Drawing.Color.Green
                'Refresh the GridView by calling the BindCategoryData()
                BindCategData()
            Catch ex As Exception
                lblMsg.Text = "Error while adding '" & CategName & "' category"
                lblMsg.ForeColor = System.Drawing.Color.Red
            Finally
                sqlCon.Close()
            End Try

            ResetAll()
        End If

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)

        'check whether the categoryid textbox is empty
        If String.IsNullOrEmpty(txtCategoryName.Text.Trim()) Then
            lblMsg.Text = "Please select record to update"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsUpdated As Boolean = False
        Dim CategID As Integer = Convert.ToInt32(txtCategoryId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim CategName As String = txtCategoryName.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add UPDATE statement to update category name for the above CatID
        cmd.CommandText = "update tblCategory set Cat_Name = @cname where Cat_Id = @cid"
        'Create two parameterized queries [CatID and CatName]
        cmd.Parameters.AddWithValue("@cid", txtCategoryId.Text.Trim())
        cmd.Parameters.AddWithValue("@cname", txtCategoryName.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsUpdated = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & CategName & "' category updated successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindCategData()
        Catch ex As Exception
            lblMsg.Text = "Error while updating '" & CategName & "' category"
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
        If String.IsNullOrEmpty(txtCategoryName.Text.Trim()) Then
            lblMsg.Text = "Please select record to delete"
            lblMsg.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim IsDeleted As Boolean = False
        Dim CategID As Integer = Convert.ToInt32(txtCategoryId.Text)
        'Add built-in function to remove spaces from Textbox Category name
        Dim CategName As String = txtCategoryName.Text
        Dim sqlCon As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        'Add DELETE statement to delete the selected category for the above CatID
        cmd.CommandText = "delete from tblCategory where Cat_Id=@cid"
        'Create a parametererized query for CatID
        cmd.Parameters.AddWithValue("@cid", txtCategoryId.Text.Trim())
        cmd.Connection = sqlCon
        Try
            sqlCon.Open()
            IsDeleted = cmd.ExecuteNonQuery() > 0
            lblMsg.Text = "'" & CategName & "' category deleted successfully!"
            lblMsg.ForeColor = System.Drawing.Color.Green
            'Refresh the GridView by calling the BindCategoryData()
            BindCategData()
        Catch ex As Exception
            lblMsg.Text = "Error while deleting '" & CategName & txtCategoryName.Text
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
        txtCategoryId.Text = ""
        txtCategoryName.Text = ""
    End Sub

    Protected Sub gvsCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Read data from GridView and Populate the form
        txtCategoryId.Text = gvsCategory.DataKeys(gvsCategory.SelectedRow.RowIndex).Value.ToString()
        txtCategoryName.Text = (TryCast(gvsCategory.SelectedRow.FindControl("lblCatName"),
Label)).Text
        'Hide Insert button during update/delete
        btnInsert.Visible = False
        btnUpdate.Visible = True
        btnDelete.Visible = True
    End Sub

    Protected Sub gvsCategory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvsCategory.PageIndex = e.NewPageIndex
        BindCategData()
    End Sub



End Class