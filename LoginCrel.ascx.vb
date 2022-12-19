Public Class LoginCrel
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property Username() As String
        Get
            Return txtUsername.Text
        End Get
        Set(ByVal Value As String)
            txtUsername.Text = Value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return txtPassword.Text
        End Get
        Set(ByVal Value As String)
            txtPassword.Attributes("value") = Value
        End Set
    End Property
    Public Property Chk() As Boolean
        Get
            Return chkremem.Checked
        End Get
        Set(ByVal Value As Boolean)
            chkremem.Checked = Value
        End Set
    End Property

End Class