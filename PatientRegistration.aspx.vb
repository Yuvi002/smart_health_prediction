Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography
Imports System.Net.Mail


Public Class PatientRegistration
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFname.Focus()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        If (FileUpload1.HasFile) Then
            If (CheckFileType(FileUpload1.FileName)) Then
                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/assets/img/") + fileName)
                Dim con As New SqlConnection(_conString)
                Dim cmd As New SqlCommand()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                cmd.CommandText = "Select Username from tblPatient where Username=@uname"
                cmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
                'Create DataReader
                Dim myReader As SqlDataReader
                con.Open()
                myReader = cmd.ExecuteReader
                If (myReader.HasRows) Then
                    lblmsg.Text = "Username Already Exist, Please Choose Another"
                    lblmsg.ForeColor = System.Drawing.Color.Red
                    txtUsername.Focus()
                Else
                    myReader.Close()
                    Dim strDate As String
                    strDate = txtDob.Text
                    Dim con1 As New SqlConnection(_conString)
                    Dim cmd1 As New SqlCommand()
                    cmd1.Connection = con1
                    cmd1.CommandType = CommandType.Text
                    cmd1.CommandText = "INSERT INTO tblPatient (Firstname, Lastname, Role, Gender, DoB, Address, Phone_Number, NIC, Email_Address, Profile_Pic, Username, Password, Status) VALUES (@Firstname, @Lastname, @Role, @Gender, @DoB, @Address, @Phone_Number, @NIC, @Email_Address, @Profile_Pic, @Username, @Password, @Status)"
                    cmd1.Parameters.AddWithValue("@Firstname", txtFname.Text)
                    cmd1.Parameters.AddWithValue("@Lastname", txtLname.Text)
                    cmd1.Parameters.AddWithValue("@Role", txtRole.Text)
                    cmd1.Parameters.AddWithValue("@Gender", rbtngen.Text)
                    cmd1.Parameters.AddWithValue("@DoB", strDate)
                    cmd1.Parameters.AddWithValue("@Address", txtAddress.Text)
                    cmd1.Parameters.AddWithValue("@Phone_Number", txtPhoneNum.Text)
                    cmd1.Parameters.AddWithValue("@NIC", txtNic.Text)
                    cmd1.Parameters.AddWithValue("@Email_Address", txtEmail.Text)
                    cmd1.Parameters.AddWithValue("@Profile_Pic", fileName)
                    cmd1.Parameters.AddWithValue("@Username", txtUsername.Text)
                    cmd1.Parameters.AddWithValue("@Password", Encrypt(txtPassword.Text))
                    cmd1.Parameters.AddWithValue("@Status", 1)
                    con1.Open()
                    cmd1.ExecuteNonQuery()
                    'sendemail()
                    con1.Close()
                    Response.Redirect("~/Login")
                End If
            End If
        End If
    End Sub

    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49,
           &H76, &H61, &H6E, &H20, &H4D,
            &H65, &H64, &H76, &H65, &H64, &H65,
            &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(),
               CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Function CheckFileType(ByVal fileName As String) As Boolean
        Dim ext As String = Path.GetExtension(fileName)
        Select Case ext.ToLower()
            Case ".gif"
                Return True
            Case ".png"
                Return True
            Case ".jpg"
                Return True
            Case ".jpeg"
                Return True
            Case Else
                Return False
        End Select
    End Function

    'Public Sub sendemail()
    '    Dim filename As String
    '    filename = Path.GetFileName(FileUpload1.PostedFile.FileName)
    '    Dim msg As New MailMessage()
    '    Dim sc As New SmtpClient()
    '    Try
    '        msg.From = New MailAddress("gangaramyogesh@gmail.com")
    '        msg.To.Add(txtEmail.Text)
    '        msg.Subject = "This is a Test Mail"
    '        msg.IsBodyHtml = True
    '        Dim msgBody As New StringBuilder()
    '        msgBody.Append("Dear " + txtUsername.Text + ", your registration is successful, thank you for signing up on SmartHealth Predixtion System.")
    '        msg.Attachments.Add(New Attachment(Server.MapPath("~/assets/img/") +
    '       filename))
    '        msgBody.Append("<a href='http://" +
    '      HttpContext.Current.Request.Url.Authority + "/Login'>Click here to login to ...</a>")
    '        msg.Body = msgBody.ToString()
    '        sc.Host = "smtp.gmail.com"
    '        sc.Port = 587
    '        sc.UseDefaultCredentials = False
    '        sc.Credentials = New System.Net.NetworkCredential("gangaramyogesh@gmail.com", "1234")
    '        sc.EnableSsl = True
    '        sc.Send(msg)
    '        Response.Write("Email Sent successfully")
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)

        txtFname.Text = ""
        txtLname.Text = ""
        txtRole.Text = ""
        rbtngen.Text = ""
        txtDob.Text = ""
        txtAddress.Text = ""
        txtPhoneNum.Text = ""
        txtNic.Text = ""
        txtEmail.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""

    End Sub
End Class