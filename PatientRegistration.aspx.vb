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
        If Not Page.IsPostBack Then

        End If

        rvDob.MinimumValue = DateAndTime.Now.AddYears(-45).ToShortDateString()
        rvDob.MaximumValue = DateAndTime.Now.AddYears(-18).ToShortDateString()
        rvDob.Type = ValidationDataType.Date
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


    Public Sub sendemail()
        Dim filename As String
        filename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim msg As New MailMessage()
        Dim sc As New SmtpClient()
        Try
            msg.From = New MailAddress("dwarkayudhav@gmail.com")
            msg.To.Add(txtEmail.Text)
            msg.Subject = "This is a Test Mail"
            msg.IsBodyHtml = True
            Dim msgBody As New StringBuilder()
            msgBody.Append("Dear " + txtUsername.Text + ", your registration is successful, thank you for signing up on The Agricultural Farming Land Lease Management System.")
            msg.Attachments.Add(New Attachment(Server.MapPath("~/Images/") +
           filename))
            msgBody.Append("<a href='http://" +
           HttpContext.Current.Request.Url.Authority + "/login'>Click here to login to ...</a>")
            msg.Body = msgBody.ToString()
            sc.Host = "smtp.gmail.com"
            sc.Port = 587
            sc.UseDefaultCredentials = False
            sc.Credentials = New System.Net.NetworkCredential("mojobcantona@gmail.com", "nulesrois2")
            sc.EnableSsl = True
            sc.Send(msg)
            Response.Write("Email Sent successfully")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub btnSubmit_Click1(sender As Object, e As EventArgs)

        If (FileUpload1.HasFile) Then
            'Add the filename name as a parameter
            If (CheckFileType(FileUpload1.FileName)) Then
                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName)

                Dim con As New SqlConnection(_conString)
                Dim cmd As New SqlCommand()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                'search for username from tblJobSeeker 
                cmd.CommandText = "select Username from tblDoctor where Username=@DoctorUsername"
                'create a parameterized query
                cmd.Parameters.AddWithValue("@DoctorUsername", txtUsername.Text.Trim())
                'Create DataReader
                Dim myReader As SqlDataReader
                con.Open()
                myReader = cmd.ExecuteReader

                'Check if username already exists in the DB
                If (myReader.HasRows) Then
                    lblmsg.Text = "Username Already Exist, Please Choose Another"
                    lblmsg.ForeColor = System.Drawing.Color.Red
                    txtUsername.Focus()
                Else
                    'Ensure the DataReader is closed
                    myReader.Close()
                    Dim strDate As String
                    strDate = txtDob.Text
                    'Dim dt As datetime
                    'dt = Convert.ToDateTime(txtDob.Text)
                    'Create another Command object to store insert statement
                    Dim con1 As New SqlConnection(_conString)
                    Dim cmd1 As New SqlCommand()
                    cmd1.Connection = con1
                    cmd1.CommandType = CommandType.Text
                    cmd1.CommandText = "INSERT INTO tblDoctor(Firstname, Lastname, Role, Gender, DoB, Address, Phone_Number, NIC, Email_Address, Profile_Pic, Username, Password, Status) VALUES (@firstname, @lastname, @role, @gender, @dob, @address, @phone, @nic, @email, @pic, @username, @pwd, @status)"
                    cmd1.Parameters.AddWithValue("@firstname", txtFname.Text)
                    cmd1.Parameters.AddWithValue("@lastname", txtLname.Text)
                    cmd1.Parameters.AddWithValue("@role", txtRole.Text)
                    cmd1.Parameters.AddWithValue("@gender", rbtngen.SelectedValue)
                    cmd1.Parameters.AddWithValue("@dob", strDate)
                    cmd1.Parameters.AddWithValue("@address", txtAddress.Text)
                    cmd1.Parameters.AddWithValue("@phone", txtPhoneNum.Text)
                    cmd1.Parameters.AddWithValue("@nic", txtNic.Text)
                    cmd1.Parameters.AddWithValue("@email", txtEmail.Text)
                    cmd1.Parameters.AddWithValue("@pic", fileName)
                    cmd1.Parameters.AddWithValue("@username", txtUsername.Text)
                    'add a method to encrypt your password 
                    cmd1.Parameters.AddWithValue("@pwd", Encrypt(txtPassword.Text))
                    'set the status to inactive or active
                    cmd1.Parameters.AddWithValue("@status", 1)
                    con1.Open()
                    cmd1.ExecuteNonQuery()
                    'call the sendemail method
                    sendemail()
                    con1.Close()
                    Response.Redirect("Login")
                End If
            End If
        End If

    End Sub

    Protected Sub btnClear_Click1(sender As Object, e As EventArgs)

        txtFname.Text = ""
        txtLname.Text = ""
        txtRole.Text = ""
        txtDob.Text = ""
        txtAddress.Text = ""
        txtNic.Text = ""
        txtPhoneNum.Text = ""
        txtEmail.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtCpassword.Text = ""

    End Sub
End Class