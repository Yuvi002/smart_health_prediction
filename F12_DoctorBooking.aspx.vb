Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
'Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

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
            ' invoke the getConsultationTime;getMedicalField;getDoctorName;getLabTechnician method
            getConsultationTime()
            getMedicalField()
            getDoctorName()
            getLabTechnician()
            BindAppointmentData()

            'insert a default item in Appointment Time Dropdown
            Dim li As New ListItem("Select Time", "-1")
            ddlconsultationtime.Items.Insert(0, li)

            'insert a default item in ddlDoctorField Dropdown
            Dim lst As New ListItem("Select your Doctor Medical Field", "-1")
            ddlDoctorField.Items.Insert(0, lst)

            'insert a default item in ddldoctorname Dropdown
            Dim lt As New ListItem("Select your Doctor", "-1")
            ddldoctorname.Items.Insert(0, lt)

            'insert a default item in ddllabtech Dropdown
            Dim ls As New ListItem("Select your Lab Technician", "-1")
            ddllabtech.Items.Insert(0, ls)

            'ddlDoctorField_SelectedIndexChanged(ddlDoctorField, Nothing)

        End If
    End Sub

    Private Sub BindAppointmentData()
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM tblAppointment"
        Dim da As New SqlDataAdapter(cmd)
        'Create a DataTable
        Dim dt As New DataTable
        Using (da)
            'Populate the DataTable
            da.Fill(dt)
        End Using
        'Set the DataTable as the DataSource
        gvsViewPatientBooking.DataSource = dt
        gvsViewPatientBooking.DataBind()
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
        lblstatus.Text = ""
    End Sub

    Protected Sub ddlDoctorField_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim Doctor_Id As String = ddldoctorname.SelectedValue
        Dim RoleID As String = ddlDoctorField.SelectedValue
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        Dim sqlParamRole As String = ""
        Dim sqlParamDoctor As String = ""
        If (Not IsNothing(ddldoctorname.DataTextField())) Then
            sqlParamDoctor = "and Firstname like '%' + Firstname + '%'"
        End If
        If (RoleID <> "-1") Then
            sqlParamRole = "and tblDoctor.RoleID = @RoleID"
        End If
        cmd.CommandText = "Select RoleID, RoleName FROM tblDoctorRole WHERE RoleName LIKE '%' + @RoleName + '%' " + sqlParamRole
        cmd.CommandText = "Select Doctor_Id, Firstname, Lastname, RoleID, Gender, DoB, Address, Phone_Number, NIC, Email_Address, Profile_Pic, Username, Password FROM tblDoctor WHERE Status = 1" + sqlParamDoctor
        cmd.Parameters.AddWithValue("@RoleName", RoleID)
        cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(Session("docid")))
        cmd.Parameters.AddWithValue("@Doctor_Id", Doctor_Id)
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
    End Sub

    Protected Sub btnBook_Click(sender As Object, e As EventArgs)

        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        cmd.CommandText = "Select Doctor_Id from tblAppointment where App_time = @App_id and App_dateregistered = @Date"
        'cmd.CommandText = "Select Doctor_Id from tblAppointment where Doctor_Id=@doctorID"
        cmd.Parameters.AddWithValue("@App_id", ddlconsultationtime.SelectedItem.Text)
        cmd.Parameters.AddWithValue("@Date", txtappointdate.Text)
        'Create DataReader
        Dim myReader As SqlDataReader
        con.Open()
        myReader = cmd.ExecuteReader

        If (myReader.HasRows) Then
            lblstatus.Text = "There is already a booking at that time, please choose another time or doctor!!!"
            lblstatus.ForeColor = System.Drawing.Color.Red
        Else
            myReader.Close()

            'Dim strDate As String
            'strDate = ddlconsultationtime.SelectedValue
            'Dim dt As DateTime
            'dt = Convert.ToDateTime(txtDob.Text)

            Dim con1 As New SqlConnection(_conString)
            Dim cmd1 As New SqlCommand()
            Dim IsAdded As Boolean = False
            cmd1.Connection = con1
            cmd1.CommandType = CommandType.Text

            cmd1.CommandText = "INSERT INTO tblAppointment (App_dateregistered, App_time, App_status, RoleID, Doctor_Id, lbtech_Id, Patient_Id) VALUES (@App_dateregistered, @App_time, @App_status, @RoleID, @Doctor_Id, @lbtech_Id, @Patient_Id) "
            cmd1.Parameters.AddWithValue("@App_dateregistered", txtappointdate.Text)
            cmd1.Parameters.AddWithValue("@App_time", ddlconsultationtime.SelectedItem.Text)
            cmd1.Parameters.AddWithValue("@App_status", 0)
            cmd1.Parameters.AddWithValue("@RoleID", ddlDoctorField.SelectedValue)
            cmd1.Parameters.AddWithValue("@Doctor_Id", ddldoctorname.SelectedValue)
            cmd1.Parameters.AddWithValue("@lbtech_Id", ddllabtech.SelectedValue)
            cmd1.Parameters.AddWithValue("@Patient_Id", Session("pid"))
            con1.Open()
            IsAdded = cmd1.ExecuteNonQuery() > 0
            'sendemail()
            con1.Close()
            If (IsAdded) Then
                lblstatus.Text = "Booking Sent for Confirmation"
                lblstatus.ForeColor = System.Drawing.Color.Green
                BindAppointmentData()
            Else
                lblstatus.Text = "Error in Booking"
                lblstatus.ForeColor = System.Drawing.Color.Red
            End If
            Response.Redirect("~/F12_DoctorBooking.aspx")
        End If
    End Sub

    Protected Sub gvsViewPatientBooking_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvsViewPatientBooking.PageIndex = e.NewPageIndex
        BindAppointmentData()
    End Sub

    Protected Sub gvsViewPatientBooking_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMsg.Text = ""

        txtApp_id.Text = gvsViewPatientBooking.DataKeys(gvsViewPatientBooking.SelectedRow.RowIndex).Value.ToString()
        txtappointdate.Text = (TryCast(gvsViewPatientBooking.SelectedRow.FindControl("Label1"), Label)).Text
        Dim con As New SqlConnection(_conString)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        'create the movieid parameter
        cmd.Parameters.AddWithValue("@app_id", txtApp_id.Text)
        'assign the parameter to the sql statement
        cmd.CommandText = "Select App_id, App_dateregistered, App_time, RoleID, Doctor_Id, lbtech_Id, Patient_Id from tblAppointment where App_status = 0"
        Dim dr As SqlDataReader
        con.Open()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            'retrieving FIELD values and assign the form controls
            txtappointdate.Text = dr("App_dateregistered").ToString
            ddlconsultationtime.SelectedIndex = Convert.ToInt32(dr("App_time"))
            ddlDoctorField.SelectedIndex = Convert.ToInt32(dr("RoleID"))
            ddldoctorname.SelectedIndex = Convert.ToInt32(dr("Doctor_Id"))
            ddllabtech.SelectedIndex = Convert.ToInt32(dr("lbtech_Id"))
        End While

        con.Close()
        btnBook.Visible = False
        btnClear.Visible = False

    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ResetAll()
    End Sub

    Private Sub ResetAll()
        btnBook.Visible = True
        txtappointdate.Text = ""
        ddlconsultationtime.SelectedIndex = 0
        ddlDoctorField.SelectedIndex = 0
        ddldoctorname.SelectedIndex = 0
        ddllabtech.SelectedIndex = 0
    End Sub

    Protected Sub gvsViewPatientBooking_PreRender(sender As Object, e As EventArgs)
        If (gvsViewPatientBooking.Rows.Count > 0) Then
            'This replaces <td> with <th> and adds the scope attribute
            gvsViewPatientBooking.UseAccessibleHeader = True
            'This will add the <thead> and <tbody> elements
            gvsViewPatientBooking.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

End Class