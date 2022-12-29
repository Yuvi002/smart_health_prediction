Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Dashboard
    Inherits System.Web.UI.Page

    Private ReadOnly _connectString As String
    Public Sub New()
        _connectString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            'LoadCat()
            Me.BindGrid()
            'TextBox1_TextChanged(TextBox1, Nothing)
        End If

    End Sub

    Private Sub LoadCat()

        Dim category As New DataTable()
        Dim con As New SqlConnection(_connectString)
        Using con
            Try
                Dim adapter = New SqlDataAdapter("SELECT * from tblCategory", _connectString)
                adapter.Fill(category)
                ddlCategory.DataSource = category
                ddlCategory.DataTextField = "Cat_ID"
                ddlCategory.DataTextField = "Cat_Name"
                ddlCategory.DataBind()
            Catch ex As Exception
                'Label1.Text = ex.Message
            End Try
        End Using
        ddlCategory.Items.Insert(0, New ListItem("Select Category", "-1"))
    End Sub



    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Me.BindGrid()
    End Sub

    Private Sub BindGrid()

        Dim Cat_ID As String = ddlCategory.SelectedValue
        Dim con As New SqlConnection(_connectString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        Dim sqlParam As String = ""
        Dim sqlParamCat As String = ""
        If (Not IsNothing(TextBox1.Text.Trim())) Then
            sqlParam = "Select Info_Name, Info_Spec, Cat_ID FROM tblInformation "
        End If
        'If (Cat_ID <> "-1") Then
        '    sqlParamCat = "tblInformation.Cat_ID = @Cat_ID"
        'End If
        cmd.CommandText = "Select Info_ID, Info_Name, Info_Spec, Cat_ID FROM tblInformation WHERE Info_Name LIKE '%' + @infoname + '%' " + sqlParam
        cmd.Parameters.AddWithValue("@infoname", TextBox1.Text.Trim())
        'cmd.Parameters.AddWithValue("@Cat_ID", Cat_ID)
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        gvCustomers.DataSource = dt
        gvCustomers.DataBind()
    End Sub

    'Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
    '    Dim Cat_ID As String = ddlCategory.SelectedValue
    '    Dim con As New SqlConnection(_connectString)
    '    Dim cmd As New SqlCommand()
    '    cmd.Connection = con
    '    cmd.CommandType = CommandType.Text
    '    Dim sqlParam As String = ""
    '    Dim sqlParamCat As String = ""
    '    If (Not IsNothing(TextBox1.Text.Trim())) Then
    '        sqlParam = "Select Info_Name, Info_Spec FROM tblInformation "
    '    End If
    '    If (Cat_ID <> "-1") Then
    '        sqlParamCat = "and tblInformation.Cat_ID = @Cat_ID"
    '    End If
    '    cmd.CommandText = "Select Info_ID, Info_Name, Info_Spec FROM tblInformation " + sqlParam + sqlParamCat
    '    cmd.Parameters.AddWithValue("@infoname", TextBox1.Text.Trim())
    '    cmd.Parameters.AddWithValue("@Cat_ID", Cat_ID)
    '    Dim table As New DataTable()
    '    Dim da As New SqlDataAdapter(cmd)
    '    da.Fill(table)
    'lvInfo.DataSource = table
    'lvInfo.DataBind()
    'UpdatePanel1.Visible = True
    'End Sub

    'Protected Sub lvInfo_PagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)
    '    TryCast(lvInfo.FindControl("DataPager1"), DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
    '    TextBox1_TextChanged(TextBox1, Nothing)

    'End Sub

    'Protected Sub gvsSymptoms_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(0).Text = Regex.Replace(e.Row.Cells(0).Text, lnkSearch.Text.Trim(), Function(match As Match) String.Format("<span style = 'background-color: #D9EDF7'>{0}</span>", match.Value),
    '        RegexOptions.IgnoreCase)
    '    End If
    'End Sub


    'Protected Sub gvInfo_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(0).Text = Regex.Replace(e.Row.Cells(0).Text, TextBox1.Text.Trim(), Function(match As Match) String.Format("<span style = 'background-color: #D9EDF7'>{0}</span>", match.Value),
    '        RegexOptions.IgnoreCase)
    '    End If
    'End Sub

    'Protected Sub gvInfo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
    '    TryCast(lvInfo.FindControl("DataPager1"), DataPager).SetPageProperties(e.NewPageIndex, e.NewPageIndex, False)
    '    TextBox1_TextChanged(TextBox1, Nothing)

    'End Sub


    Protected Sub gvCustomers_RowDataBound1(sender As Object, e As GridViewRowEventArgs)

    End Sub

    Protected Sub gvCustomers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        'TryCast(gvCustomers.FindControl("DataPager1"), DataPager).SetPageProperties(e.NewPageIndex, e.NewPageIndex, False)
        'TextBox1_TextChanged(TextBox1, Nothing)

        gvCustomers.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub

    Protected Sub gvCustomers_PreRender(sender As Object, e As EventArgs)
        If (gvCustomers.Rows.Count > 0) Then
            gvCustomers.UseAccessibleHeader = True
            gvCustomers.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
End Class