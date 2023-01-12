Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography


Public Class DoctorLogin
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString =
WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            'Verify if uname and pw cookies are not null
            If (Not IsNothing(Request.Cookies("Username")) And Not IsNothing(Request.Cookies("Password"))) Then
                'Populate the Username and Password Textboxes by retrieving the values 
                'from the cookies

                userLogin.Username = Request.Cookies("Username").Value
                userLogin.Password = Request.Cookies("Password").Value

            End If
        End If

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        'get the value of Username and Password fields and state of checkbox from 
        'login form
        Dim Username As String = userLogin.Username
        Dim Password As String = userLogin.Password
        Dim chk As Boolean = userLogin.Chk

        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        'searching for a record containing matching Username & Password with 
        'an active status
        cmd.CommandText = "select * from tblDoctor where Username=@un and Password=@pass and status=1"

        'create two parameterized query for the above select statement
        cmd.Parameters.AddWithValue("@un", Username)
        cmd.Parameters.AddWithValue("@pass", Decrypt(Password))

        'use above variables and decrypt Password
        Dim myReader As SqlDataReader
        con.Open()
        myReader = cmd.ExecuteReader

        'check if the DataReader contains a record
        If (myReader.HasRows) Then
            If myReader.Read Then
                'create a memory cookie to store Username and pwd
                Response.Cookies("Username").Value = Username
                Response.Cookies("Password").Value = Password

                If (chk) Then
                    'if checkbox is checked, make cookies persistent 
                    Response.Cookies("Username").Expires = DateAndTime.Now.AddDays(100)
                    Response.Cookies("Password").Expires = DateAndTime.Now.AddDays(100)
                Else
                    'delete the cookies if checkbox is unchecked
                    Response.Cookies("Username").Expires = DateAndTime.Now.AddDays(-100)
                    Response.Cookies("Password").Expires = DateAndTime.Now.AddDays(-100)
                    'delete content of Password field
                End If

                'create and save Username in a session variable
                Session("docname") = Username
                'create and save userid in a session variable
                Session("docid") = myReader("Doctor_Id").ToString()
                'redirect to the corresponding page
                Response.Redirect("~/F12_DoctorBooking")
                con.Close()
            End If
        Else
            'delete content of Password field 
            lblmsg.Text = "You are not registered or your account has been suspended!"
        End If
    End Sub

    Private Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                cipherText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function



End Class