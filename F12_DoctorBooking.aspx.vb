Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
'Imports System.Data.SqlClient
'Imports System.Web.Configuration

Public Class F12_DoctorBooking
    Inherits System.Web.UI.Page

    Private ReadOnly _conString As String
    Public Sub New()
        _conString =
WebConfigurationManager.ConnectionStrings("MedicalCS").ConnectionString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (IsNothing(Session("pid"))) Then
            Response.Redirect("~/Login.aspx")
        End If

        If IsPostBack = False Then
            ' invoke the getcountry method
            getConsultationTime()
            getMedicalField()
            getDoctorName()
            getLabTechnician()

            'insert a default item in Appointment Time Dropdown
            Dim li As New ListItem("Select Time", "-1")
            ddlconsultationtime.Items.Insert(0, li)

            Dim lst As New ListItem("Select your Doctor Medical Field", "-1")
            ddlDoctorField.Items.Insert(0, lst)

            Dim lt As New ListItem("Select your Doctor", "-1")
            ddldoctorname.Items.Insert(0, lt)

            Dim ls As New ListItem("Select your Lab Technician", "-1")
            ddllabtech.Items.Insert(0, ls)


            'ddlDoctorField_SelectedIndexChanged1(ddlDoctorField, Nothing)
        End If
    End Sub

    Public Sub getConsultationTime()
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tblConsultationTime"
        cmd.Connection = con
        'Create DataAdapter
        Dim da As New SqlDataAdapter(cmd)
        'Create DataSet
        Dim ds As New DataSet()
        'Fill the Dataset and ensure the DB Connection is closed 
        da.Fill(ds)
        'To load country names in dropdown
        ddlconsultationtime.DataSource = ds
        'Assign field name and id to ddl
        ddlconsultationtime.DataTextField = "constimetime"
        ddlconsultationtime.DataValueField = "constimeid"
        ddlconsultationtime.DataBind()
    End Sub

    Public Sub getMedicalField()
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
        'To load country names in dropdown
        ddlDoctorField.DataSource = ds
        'Assign field name and id to ddl
        ddlDoctorField.DataTextField = "RoleName"
        ddlDoctorField.DataValueField = "RoleID"
        ddlDoctorField.DataBind()
    End Sub

    Public Sub getDoctorName()
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tblDoctor"
        cmd.Connection = con
        'Create DataAdapter
        Dim da As New SqlDataAdapter(cmd)
        'Create DataSet
        Dim ds As New DataSet()
        'Fill the Dataset and ensure the DB Connection is closed 
        da.Fill(ds)
        'To load country names in dropdown
        ddldoctorname.DataSource = ds
        'Assign field name and id to ddl
        ddldoctorname.DataTextField = "Firstname"
        ddldoctorname.DataValueField = "Doctor_Id"
        ddldoctorname.DataBind()
    End Sub

    Public Sub getLabTechnician()
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tbllab_tech"
        cmd.Connection = con
        'Create DataAdapter
        Dim da As New SqlDataAdapter(cmd)
        'Create DataSet
        Dim ds As New DataSet()
        'Fill the Dataset and ensure the DB Connection is closed 
        da.Fill(ds)
        'To load country names in dropdown
        ddllabtech.DataSource = ds
        'Assign field name and id to ddl
        ddllabtech.DataTextField = "lbtech_Firstname"
        ddllabtech.DataValueField = "lbtech_Id"
        ddllabtech.DataBind()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        txtappointdate.Text = ""
        ddlconsultationtime.SelectedIndex = 0
        ddlDoctorField.SelectedIndex = 0
        ddldoctorname.SelectedIndex = 0
        ddllabtech.SelectedIndex = 0
    End Sub


    'Protected Sub ddlDoctorField_SelectedIndexChanged1(sender As Object, e As EventArgs)
    '    Me.BindDDL()
    'End Sub

    'Private Sub BindDDL()
    '    Dim Doctor_Id As String = ddldoctorname.SelectedValue
    '    Dim RoleID As String = ddlDoctorField.SelectedValue
    '    Dim con As New SqlConnection(_conString)
    '    Dim cmd As New SqlCommand()
    '    cmd.Connection = con
    '    cmd.CommandType = CommandType.Text
    '    Dim sqlParam As String = ""
    '    Dim sqlParamCat As String = ""
    '    If (RoleID <> "-1") Then
    '        sqlParam = "and tblDoctorRole.RoleID = @RoleID"
    '    End If
    '    If (RoleID <> "-1") Then
    '        sqlParamCat = "and tblDoctor.RoleID = @RoleID"
    '    End If
    '    cmd.CommandText = "Select RoleID, RoleName FROM tblDoctorRole WHERE RoleName LIKE '%' + @RoleName + '%' " + sqlParam + sqlParamCat
    '    cmd.Parameters.AddWithValue("@RoleName", RoleID)
    '    cmd.Parameters.AddWithValue("@Doctor_Id", Doctor_Id)
    '    Dim dt As New DataTable()
    '    Dim da As New SqlDataAdapter(cmd)
    '    da.Fill(dt)
    'End Sub



End Class