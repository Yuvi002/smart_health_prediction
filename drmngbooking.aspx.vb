Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Web.Configuration

Public Class drmngbooking
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString =
WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsNothing(Session("docname"))) Then
            Response.Redirect("~/DoctorLogin.aspx")
        End If

        getPendingBooking()

    End Sub

    Private Sub getPendingBooking()
        Dim dbconn As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select App_id, App_dateregistered, App_time, RoleID, Doctor_Id, lbtech_Id, Patient_Id from tblAppointment where App_status = 0"
        cmd.Connection = dbconn
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da.Fill(dt)
        gvsAcceptBooking.DataSource = dt
        gvsAcceptBooking.DataBind()

    End Sub

    Protected Sub gvsAcceptBooking_PreRender(sender As Object, e As EventArgs)
        If (gvsAcceptBooking.Rows.Count > 0) Then
            'This replaces <td> with <th> and adds the scope attribute
            gvsAcceptBooking.UseAccessibleHeader = True
            'This will add the <thead> and <tbody> elements
            gvsAcceptBooking.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)
        'Retrieving the RecruiterID from the command argument link button 
        Dim docid As Integer = Convert.ToInt32(CType(sender, LinkButton).CommandArgument)
        Dim dbconn As New SqlConnection(_conString)
        'open Connection
        dbconn.Open()
        'Create Command
        Dim ucmd As New SqlCommand()
        ucmd.CommandType = CommandType.Text
        ucmd.CommandText = "update tblAppointment set App_status='1' Where App_id ='" & docid & "'"
        ucmd.Connection = dbconn
        ucmd.ExecuteNonQuery()
        dbconn.Close()
        getPendingBooking()
        sendemail(sender)
        'sendemail2(sender)
    End Sub

    'linkDeny
    Protected Sub lnkDeny_Click(sender As Object, e As EventArgs)
        'Retrieving the RecruiterID from the command argument link button 
        Dim docid As Integer = Convert.ToInt32(CType(sender, LinkButton).CommandArgument)
        Dim dbconn As New SqlConnection(_conString)
        'open Connection
        dbconn.Open()
        'Create Command
        Dim ucmd As New SqlCommand()
        ucmd.CommandType = CommandType.Text
        ucmd.CommandText = "update tblAppointment set App_status='0' Where App_id ='" & docid & "'"
        ucmd.Connection = dbconn
        ucmd.ExecuteNonQuery()
        dbconn.Close()
        getPendingBooking()
        'sendemail(sender)
        sendemail2(sender)

    End Sub


    Public Sub sendemail(sender As Object)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim grvRow As GridViewRow = CType(lnk.NamingContainer, GridViewRow)
        Dim hf As HiddenField = CType(grvRow.FindControl("hidpatient"), HiddenField)
        Dim patientId As Integer = Convert.ToInt32(hf.Value)

        Dim lnk1 As LinkButton = CType(sender, LinkButton)
        Dim grvRow1 As GridViewRow = CType(lnk1.NamingContainer, GridViewRow)
        Dim hf1 As HiddenField = CType(grvRow1.FindControl("hiddoctor"), HiddenField)
        Dim Doctor_Id As Integer = Convert.ToInt32(hf1.Value)

        Dim btn As LinkButton
        btn = CType(sender, LinkButton)
        Dim App_id As Integer = Convert.ToInt32(btn.CommandArgument)
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        Dim pf_email As String
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select Email_Address from tblPatient Where Patient_Id = @patientId"
        cmd.Parameters.AddWithValue("@patientId", patientId)
        'set the status to inactive or active
        con.Open()
        pf_email = cmd.ExecuteScalar()
        cmd.ExecuteNonQuery()
        'call the sendemail method
        con.Close()

        Dim msg As New MailMessage()
        Dim sc As New SmtpClient()
        Try
            msg.From = New MailAddress("ygangaram@umail.utm.ac.mu")
            msg.To.Add(pf_email)
            msg.Subject = "Concerning Booking!"
            msg.IsBodyHtml = True
            Dim msgBody As New StringBuilder()
            msgBody.Append("Dear " + "Sir/Madam" + ", your online appointment has been confirmed.  To join the video meeting, click this link: https://meet.google.com/zpr-yrtx-fcv Otherwise, 
to join by phone, dial +1 470-285-0294 and enter this PIN: 696 421 404#
")

            msgBody.Append("<a href='http://" +
           HttpContext.Current.Request.Url.Authority + "/login'>Click here to login to ...</a>")
            msg.Body = msgBody.ToString()
            sc.Host = "smtp.gmail.com"
            sc.Port = 587
            sc.UseDefaultCredentials = False
            sc.Credentials = New System.Net.NetworkCredential("ygangaram@umail.utm.ac.mu", "umail@0204")
            sc.EnableSsl = True
            sc.Send(msg)
            Response.Write("Email Sent successfully")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub sendemail2(sender As Object)
        Dim lnk2 As LinkButton = CType(sender, LinkButton)
        Dim grvRow2 As GridViewRow = CType(lnk2.NamingContainer, GridViewRow)
        Dim hf2 As HiddenField = CType(grvRow2.FindControl("hidpatient"), HiddenField)
        Dim patientId As Integer = Convert.ToInt32(hf2.Value)
        Dim btn As LinkButton
        btn = CType(sender, LinkButton)
        Dim App_id As Integer = Convert.ToInt32(btn.CommandArgument)
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        Dim pf_email As String
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select Email_Address from tblPatient Where Patient_Id = @patientId"
        cmd.Parameters.AddWithValue("@patientId", patientId)
        'set the status to inactive or active
        con.Open()
        pf_email = cmd.ExecuteScalar()
        cmd.ExecuteNonQuery()
        'call the sendemail method
        con.Close()

        Dim msg As New MailMessage()
        Dim sc As New SmtpClient()
        Try
            msg.From = New MailAddress("ygangaram@umail.utm.ac.mu")
            msg.To.Add(pf_email)
            msg.Subject = "Concerning Booking!"
            msg.IsBodyHtml = True
            Dim msgBody As New StringBuilder()
            msgBody.Append("Dear " + "Sir/Madam" + ", your online booking has been deny. Please choose another date and time. Thank you for your coorperation!")

            msgBody.Append("<a href='http://" +
           HttpContext.Current.Request.Url.Authority + "/F12_DoctorBooking.aspx'>Click here to login to ...</a>")
            msg.Body = msgBody.ToString()
            sc.Host = "smtp.gmail.com"
            sc.Port = 587
            sc.UseDefaultCredentials = False
            sc.Credentials = New System.Net.NetworkCredential("ygangaram@umail.utm.ac.mu", "umail@0204")
            sc.EnableSsl = True
            sc.Send(msg)
            Response.Write("Email Sent successfully")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class