Public Class drmngbooking
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsNothing(Session("docname"))) Then
            Response.Redirect("~/DoctorLogin.aspx")
        End If
    End Sub

End Class