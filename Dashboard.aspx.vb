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
            Me.BindGrid()
        End If

    End Sub

    'Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    '    Dim con As New SqlConnection(_connectString)
    '    Dim cmd As New SqlCommand()
    '    cmd.Connection = con
    '    cmd.CommandType = CommandType.Text
    '    Dim sqlParam As String = ""
    '    If (Not IsNothing(TextBox1.Text.Trim())) Then
    '        sqlParam = "Select Info_Name, Info_Spec FROM tblInformation "
    '    End If
    '    cmd.CommandText = "Select Info_ID, Info_Name, Info_Spec FROM tblInformation " + sqlParam
    '    cmd.Parameters.AddWithValue("@infoname", TextBox1.Text.Trim())
    '    Dim table As New DataTable()
    '    Dim da As New SqlDataAdapter(cmd)
    '    da.Fill(table)
    '    lvInfo.DataSource = table
    '    lvInfo.DataBind()
    '    UpdatePanel1.Visible = True
    'End Sub

    'Protected Sub lvInfo_PagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)
    '    TryCast(lvInfo.FindControl("DataPager1"), DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
    '    TextBox1_TextChanged(TextBox1, Nothing)
    '    lblmsg.Text = ""
    'End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        Me.BindGrid()
    End Sub

    Private Sub BindGrid()




        Dim constr As String = ""
        'ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As New SqlConnection(_connectString)
            Using cmd As New SqlCommand()
                cmd.CommandText = "Select Info_Name, Info_Spec from tblInformation WHERE Info_Name LIKE '%' + @infoname + '%' "
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@infoname", TextBox1.Text.Trim())
                Dim dt As New DataTable()
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                    gvsSymptoms.DataSource = dt
                        gvsSymptoms.DataBind()


                        gvsSymptoms.Visible = True

                End Using
            End Using
        End Using

    End Sub

    Protected Sub gvsSymptoms_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = Regex.Replace(e.Row.Cells(0).Text, lnkSearch.Text.Trim(), Function(match As Match) String.Format("<span style = 'background-color: #D9EDF7'>{0}</span>", match.Value),
            RegexOptions.IgnoreCase)
        End If
    End Sub

    Protected Sub gvsSymptoms_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvsSymptoms.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub
End Class