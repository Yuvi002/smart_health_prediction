Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Dashboard
    Inherits System.Web.UI.Page

    Private ReadOnly _connectString As String
    Public Sub New()
        _connectString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            TextBox1_TextChanged(TextBox1, Nothing)
        End If

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

End Class