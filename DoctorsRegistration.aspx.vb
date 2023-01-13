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

Public Class DoctorsRegistration
    Inherits System.Web.UI.Page
    Private ReadOnly _conString As String

    Public Sub New()
        _conString = WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ' invoke the getDoctorRole method
            getDoctorRole()
            'insert a default item in Dropdown
            Dim li As New ListItem("Select Medical Field", "-1")
            ddlRole.Items.Insert(0, li)
        End If
        txtFname.Focus()
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

    Public Sub getDoctorRole()
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tblDoctorRole"
        cmd.Connection = con

        'Create DataAdapter
        Dim da As New SqlDataAdapter(cmd)
        'Create DataSet
        Dim ds As New DataSet()
        'Fill the Dataset and ensure the DB Connection is closed 
        da.Fill(ds)
        'To load RoleName names in dropdown
        ddlRole.DataSource = ds
        'Assign field name and id to ddl
        ddlRole.DataTextField = "RoleName"
        ddlRole.DataValueField = "RoleID"
        ddlRole.DataBind()
    End Sub



    Protected Sub btnSubmit_Click1(sender As Object, e As EventArgs)
        If (FileUpload1.HasFile) Then
            If (CheckFileType(FileUpload1.FileName)) Then
                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/assets/img/") + fileName)
                Dim con As New SqlConnection(_conString)
                Dim cmd As New SqlCommand()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                cmd.CommandText = "Select Username from tblDoctor where Username=@uname"
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
                    'Dim dt As DateTime
                    'dt = Convert.ToDateTime(txtDob.Text)

                    Dim con1 As New SqlConnection(_conString)
                    Dim cmd1 As New SqlCommand()
                    cmd1.Connection = con1
                    cmd1.CommandType = CommandType.Text

                    cmd1.CommandText = "INSERT INTO tblDoctor (Firstname, Lastname, RoleID, Gender, DoB, Address, Phone_Number, NIC, Email_Address, Profile_Pic, Username, Password, Status) VALUES (@Firstname, @Lastname, @RoleID, @Gender, @DoB, @Address, @Phone_Number, @NIC, @Email_Address, @Profile_Pic, @Username, @Password, @Status) "
                    cmd1.Parameters.AddWithValue("@Firstname", txtFname.Text.Trim())
                    cmd1.Parameters.AddWithValue("@Lastname", txtLname.Text.Trim())
                    cmd1.Parameters.AddWithValue("@RoleID", ddlRole.SelectedValue)
                    cmd1.Parameters.AddWithValue("@Gender", rbtngen.Text.Trim())
                    cmd1.Parameters.AddWithValue("@DoB", strDate)
                    cmd1.Parameters.AddWithValue("@Address", txtAddress.Text.Trim())
                    cmd1.Parameters.AddWithValue("@Phone_Number", txtPhoneNum.Text.Trim())
                    cmd1.Parameters.AddWithValue("@NIC", txtNic.Text.Trim())
                    cmd1.Parameters.AddWithValue("@Email_Address", txtEmail.Text.Trim())
                    cmd1.Parameters.AddWithValue("@Profile_Pic", fileName)
                    cmd1.Parameters.AddWithValue("@Username", txtUsername.Text.Trim())
                    cmd1.Parameters.AddWithValue("@Password", Encrypt(txtPassword.Text.Trim()))
                    cmd1.Parameters.AddWithValue("@Status", 1)
                    con1.Open()
                    cmd1.ExecuteNonQuery()
                    'sendemail()
                    con1.Close()
                    Response.Redirect("~/DoctorLogin")
                End If
            End If
        End If
    End Sub

    Protected Sub btnClear_Click1(sender As Object, e As EventArgs)
        txtFname.Text = ""
        txtLname.Text = ""
        ddlRole.SelectedIndex = 0
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