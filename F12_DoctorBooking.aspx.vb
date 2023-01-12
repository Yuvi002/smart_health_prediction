Public Class F12_DoctorBooking
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsNothing(Session("pid"))) Then
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

End Class