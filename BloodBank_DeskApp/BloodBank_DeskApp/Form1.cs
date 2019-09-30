using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Configuration;

using BloodBank_DeskApp.Reports;
using BloodBank_DeskApp;


namespace BloodBank_DeskApp
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;

        public Form1(DataTable dt)
        {
            InitializeComponent();

            //button panel
            panelHome.Visible = true;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }

        private void panelEmployee_Paint(object sender, PaintEventArgs e)
        {
            //Not for Use
        }
        //from button panel
        private void buttonHome_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonHome.Height;
            panelMove.Top = buttonHome.Top;

            panelHome.Visible = true;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonEmployee.Height;
            panelMove.Top = buttonEmployee.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = true;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonDonor_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonDonor.Height;
            panelMove.Top = buttonDonor.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = true;
            panelBloodTest.Visible =false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonPatient_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonPatient.Height;
            panelMove.Top = buttonPatient.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = true;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonDatabase_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonDatabase.Height;
            panelMove.Top = buttonDatabase.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = true;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonReports_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonReports.Height;
            panelMove.Top = buttonReports.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = true;
        }
        //from employee panel
        private void buttonEmpSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
                if (textBoxEmpName.Text !="" && textBoxEmpAge.Text != "" && textBoxEmpAddress.Text != "" && textBoxEmpPhone.Text != "" && textBoxEmpAge.Text != "")
                {
                    con.Open();
                    string qryInsert = "INSERT INTO Staff (Name,Designation,Gender,StaffAge,StaffBG,StaffAddress,StaffPhone,StaffEmail,JoiningDate,ResineingDate,IsActive) VALUES (@Name,@Designation,@Gender,@StaffAge,@StaffBG,@StaffAddress,@StaffPhone,@StaffEmail,@JoiningDate,@ResineingDate,@IsActive)";
                    SqlCommand cmd = new SqlCommand(qryInsert, con);
                    cmd.Parameters.AddWithValue("@Name", textBoxEmpName.Text);
                    cmd.Parameters.AddWithValue("@Designation", comboBoxEmpDesignation.GetItemText(comboBoxEmpDesignation.SelectedItem));
                    if (radioButtonEmpMale.Checked)
                        cmd.Parameters.AddWithValue("@Gender", "Male");
                    if (radioButtonEmpFemale.Checked)
                        cmd.Parameters.AddWithValue("@Gender", "Female");
                    if (radioButtonEmpOthers.Checked)
                        cmd.Parameters.AddWithValue("@Gender", "Others");
                    cmd.Parameters.AddWithValue("@StaffAge", textBoxEmpAge.Text);
                    cmd.Parameters.AddWithValue("@StaffBG", comboBoxEmpBloodGroup.GetItemText(comboBoxEmpBloodGroup.SelectedItem));
                    cmd.Parameters.AddWithValue("@StaffAddress", textBoxEmpAddress.Text);
                    cmd.Parameters.AddWithValue("@StaffPhone", textBoxEmpPhone.Text);
                    cmd.Parameters.AddWithValue("@StaffEmail", textBoxEmpEmail.Text);
                    cmd.Parameters.AddWithValue("@JoiningDate", dateTimePickerEmpJoiningDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@ResineingDate", dateTimePickerEmpResigningDate.Value.ToString());
                    int IsActive;
                    if (checkBoxIsActive.Checked == true)
                    {
                        IsActive = 1;
                    }
                    else
                    {
                        IsActive = 0;
                    }
                    cmd.Parameters.Add(new SqlParameter("@IsActive", IsActive));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record saved successfully!!!");


                    textBoxEmpName.Text = "";
                    comboBoxEmpDesignation.Text = "";
                    radioButtonEmpMale.Checked = false;
                    radioButtonEmpFemale.Checked = false;
                    radioButtonEmpOthers.Checked = false;
                    textBoxEmpAge.Text = "";
                    comboBoxEmpBloodGroup.Text = "";
                    textBoxEmpAddress.Text = "";
                    textBoxEmpPhone.Text = "";
                    textBoxEmpEmail.Text = "";
                    checkBoxIsActive.Checked = false;
                    con.Close();

                    con.Open();
                    string qryStaff = "SELECT * FROM Staff";
                    cmd = new SqlCommand(qryStaff, con);
                    DataTable dataTableStaff = new DataTable();
                    SqlDataAdapter sqlDataAdapterStaff = new SqlDataAdapter(cmd);
                    sqlDataAdapterStaff.Fill(dataTableStaff);
                    dataGridViewEmployee.DataSource = dataTableStaff.DefaultView;
                    con.Close();
                }
            else
                {
                    MessageBox.Show("Please Insert All The Values Correctly");
                }
        }

        private void buttonEmpExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //from button panel
        private void buttonBloodTest_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonBloodTest.Height;
            panelMove.Top = buttonBloodTest.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = true;
            panelPatient.Visible = false;
            panelUpdates.Visible = false;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from button panel
        private void buttonUpdates_Click(object sender, EventArgs e)
        {
            panelMove.Height = buttonUpdates.Height;
            panelMove.Top = buttonUpdates.Top;

            panelHome.Visible = false;
            panelEmployee.Visible = false;
            panelDonor.Visible = false;
            panelBloodTest.Visible = false;
            panelPatient.Visible = false;
            panelUpdates.Visible = true;
            panelDatabase.Visible = false;
            panelReports.Visible = false;
        }
        //from donor panel
        private void buttonDonorSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string qryInsert = "INSERT INTO Donor (DonorName,Gender,DonorAge,DonorAddress,DonorPhone,DonorBG,DonationDate,DonatedQty,StaffID) VALUES (@DonorName,@Gender,@DonorAge,@DonorAddress,@DonorPhone,@DonorBG,@DonationDate,@DonatedQty,@StaffID)";
                SqlCommand cmd = new SqlCommand(qryInsert, con);
                cmd.Parameters.AddWithValue("@DonorName", textBoxDonorName.Text);
                if (radioButtonDonorMale.Checked)
                    cmd.Parameters.AddWithValue("@Gender", "Male");
                if (radioButtonDonorFemale.Checked)
                    cmd.Parameters.AddWithValue("@Gender", "Female");
                if (radioButtonDonorOthers.Checked)
                    cmd.Parameters.AddWithValue("@Gender", "Others");
                cmd.Parameters.AddWithValue("@DonorAge", textBoxDonorAge.Text);
                cmd.Parameters.AddWithValue("@DonorAddress", textBoxDonorAddress.Text);
                cmd.Parameters.AddWithValue("@DonorPhone", textBoxDonorPhone.Text);
                cmd.Parameters.AddWithValue("@DonorBG", comboBoxDonorBloodGroup.GetItemText(comboBoxDonorBloodGroup.SelectedItem));
                cmd.Parameters.AddWithValue("@DonationDate", dateTimePickerDonatedDate.Value.ToString());
                cmd.Parameters.AddWithValue("@DonatedQty", textBoxDonatedQuantity.Text);
                cmd.Parameters.AddWithValue("@StaffID", textBoxDonorStaffID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record saved successfully!!!");


                textBoxDonorName.Text = "";
                comboBoxDonorBloodGroup.Text = "";
                textBoxDonorAge.Text = "";
                textBoxDonorAddress.Text = "";
                textBoxDonorPhone.Text = "";
                textBoxDonatedQuantity.Text = "";
                textBoxDonorStaffID.Text = "";
                con.Close();
            }
        }
        //from donor panel
        private void buttonDonorExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //from Blood test panel
        private void buttonBloodTestSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TestAndStore", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@donorid", SqlDbType.Int).Value = textBoxBTDonorID.Text;
                cmd.Parameters.AddWithValue("@testedbloodgroup", SqlDbType.NVarChar).Value = comboBoxBTBloodGroup.GetItemText(comboBoxBTBloodGroup.SelectedItem);
                if (radioButtonHIVPositive.Checked)
                    cmd.Parameters.AddWithValue("@hiv", 1);
                if (radioButtonHIVNegative.Checked)
                    cmd.Parameters.AddWithValue("@hiv", 0);
                if (radioButtonHepatitisBPositive.Checked)
                    cmd.Parameters.AddWithValue("@hepatitisb", 1);
                if (radioButtonHepatitisBNegative.Checked)
                    cmd.Parameters.AddWithValue("@hepatitisb", 0);
                if (radioButtonHepatitisCPositive.Checked)
                    cmd.Parameters.AddWithValue("@hepatitisc", 1);
                if (radioButtonHepatitisCNegative.Checked)
                    cmd.Parameters.AddWithValue("@hepatitisc", 0);
                if (radioButtonHTLVPositive.Checked)
                    cmd.Parameters.AddWithValue("@htlv", 1);
                if (radioButtonHTLVNegative.Checked)
                    cmd.Parameters.AddWithValue("@htlv", 0);
                cmd.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = textBoxBTStaffID.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record saved successfully!!!");

                textBoxBTDonorID.Text = "";
                comboBoxBTBloodGroup.Text = "";
                radioButtonHIVPositive.Checked = false;
                radioButtonHIVNegative.Checked = false;
                radioButtonHepatitisBPositive.Checked = false;
                radioButtonHepatitisBNegative.Checked = false;
                radioButtonHepatitisCPositive.Checked = false;
                radioButtonHepatitisCNegative.Checked = false;
                radioButtonHTLVPositive.Checked = false;
                radioButtonHTLVNegative.Checked = false;
                textBoxBTStaffID.Text = "";
                con.Close();
            }
        }
        //from Blood test panel
        private void buttonBloodTestExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bloodBankDBDataSetEmpUpdDatGridView.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter3.Fill(this.bloodBankDBDataSetEmpUpdDatGridView.Staff);
            // TODO: This line of code loads data into the 'bloodBankDBDataSetEmpUpdID.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter2.Fill(this.bloodBankDBDataSetEmpUpdID.Staff);
            // TODO: This line of code loads data into the 'bloodBankDBDataSetPatientStaffID.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter1.Fill(this.bloodBankDBDataSetPatientStaffID.Staff);
            // TODO: This line of code loads data into the 'bloodBankDBDataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.bloodBankDBDataSet.Staff);
            //For Crystal Report 
            CrystalReportBloodTest crbt = new CrystalReportBloodTest();
            crystalReportViewerTest.ReportSource = crbt;

            CrystalReportEmployee cremp = new CrystalReportEmployee();
            crystalReportViewerEmployee.ReportSource = cremp;

            CrystalReportStorage crs = new CrystalReportStorage();
            crystalReportViewerStorage.ReportSource = crs;

            //auto fill searchbox
            try
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string autoQry = "SELECT Name from Staff";
                SqlCommand cmd = new SqlCommand(autoQry, con);
                SqlDataReader dataReader = cmd.ExecuteReader();
                AutoCompleteStringCollection autoCompleteStringStaff = new AutoCompleteStringCollection();
                while(dataReader.Read())
                {
                    autoCompleteStringStaff.Add(dataReader.GetString(0));
                }
                textBoxAutoSearchStaff.AutoCompleteCustomSource = autoCompleteStringStaff;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name,Designation,Gender,StaffBG,StaffAddress,StaffPhone,StaffEmail,JoiningDate from Staff WHERE Name like '" + textBoxAutoSearchStaff.Text + "%'", con);
            sda.Fill(dt);
            dataGridViewEmployee.DataSource = dt;
            con.Close();

            textBoxAutoSearchStaff.Text = "";
        }

        private void buttonRefrish_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string qryStaff = "SELECT * FROM Staff";
            SqlCommand cmd = new SqlCommand(qryStaff, con);
            DataTable dataTableStaff = new DataTable();
            SqlDataAdapter sqlDataAdapterStaff = new SqlDataAdapter(cmd);
            sqlDataAdapterStaff.Fill(dataTableStaff);
            dataGridViewEmployee.DataSource = dataTableStaff.DefaultView;
            con.Close();

            textBoxAutoSearchStaff.Text = "";
        }
        //patient Save button
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_PatientAndStore", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@patientname", SqlDbType.NVarChar).Value = textBoxPatientName.Text;
                if (radioButtonPatientMale.Checked) { cmd.Parameters.AddWithValue("@patientgender", "Male"); }
                if (radioButtonPatientFemale.Checked) { cmd.Parameters.AddWithValue("@patientgender", "Female"); }
                if (radioButtonPatientOthers.Checked) { cmd.Parameters.AddWithValue("@patientgender", "Others"); }
                cmd.Parameters.AddWithValue("@patientage", SqlDbType.Int).Value = textBoxPatientAge.Text;
                cmd.Parameters.AddWithValue("@patientaddress", SqlDbType.NVarChar).Value = textBoxPatientAddress.Text;
                cmd.Parameters.AddWithValue("@patientphone", SqlDbType.NVarChar).Value = textBoxPatientPhone.Text;
                cmd.Parameters.AddWithValue("@requestedbg", SqlDbType.NVarChar).Value = comboBoxPatientBloodGroup.GetItemText(comboBoxPatientBloodGroup.SelectedItem);
                cmd.Parameters.AddWithValue("@receiveddate", dateTimePickerPatientDate.Value.ToString());
                cmd.Parameters.AddWithValue("@receivedqty", SqlDbType.Int).Value = textBoxPatientQuantity.Text;
                cmd.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = comboBoxPatientStaffID.GetItemText(comboBoxPatientStaffID.SelectedValue);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record saved successfully!!!");

                textBoxPatientName.Text = "";
                radioButtonPatientMale.Checked = false;
                radioButtonPatientFemale.Checked = false;
                radioButtonPatientOthers.Checked = false;
                textBoxPatientAge.Text = "";
                textBoxPatientAddress.Text = "";
                textBoxPatientPhone.Text = "";
                comboBoxPatientBloodGroup.Text = "";
                textBoxPatientQuantity.Text = "";
                comboBoxPatientStaffID.Text = "";
                con.Close();
            }
        }
        //patient Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Update Panel Employee Update Button
        private void buttonEmpUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string updateQry = "UPDATE Staff SET Name=@Name, Designation=@Designation, Gender=@Gender, StaffAge=@StaffAge, StaffBG=@StaffBG, StaffAddress=@StaffAddress, StaffPhone=@StaffPhone, StaffEmail=@StaffEmail, JoiningDate=@JoiningDate, ResineingDate=@ResineingDate, IsActive=@IsActive WHERE StaffID=@StaffID";
            SqlCommand cmd = new SqlCommand(updateQry, con);
            cmd.Parameters.AddWithValue("@Name", textBoxEmpUpdName.Text);
            cmd.Parameters.AddWithValue("@Designation", comboBoxEmpUpdDesignation.GetItemText(comboBoxEmpUpdDesignation.SelectedText));
            if (radioButtonEmpUpdMale.Checked)
                cmd.Parameters.AddWithValue("@Gender", "Male");
            if (radioButtonEmpUpdFemale.Checked)
                cmd.Parameters.AddWithValue("@Gender", "Female");
            if (radioButtonEmpUpdOthers.Checked)
                cmd.Parameters.AddWithValue("@Gender", "Others");
            cmd.Parameters.AddWithValue("@StaffAge", textBoxEmpUpdAge.Text);
            cmd.Parameters.AddWithValue("@StaffBG", comboBoxEmpUpdBloodGroup.GetItemText(comboBoxEmpUpdBloodGroup.SelectedItem));
            cmd.Parameters.AddWithValue("@StaffAddress", textBoxEmpUpdAddress.Text);
            cmd.Parameters.AddWithValue("@StaffPhone", textBoxEmpUpdPhone.Text);
            cmd.Parameters.AddWithValue("@StaffEmail", textBoxEmpUpdEmail.Text);
            cmd.Parameters.AddWithValue("@JoiningDate", dateTimePickerEmpUpdJoinDate.Value.ToString());
            cmd.Parameters.AddWithValue("@ResineingDate", dateTimePickerEmpUpdResignDate.Value.ToString());
            int IsActive;
            if (checkBoxEmpUpdIsActive.Checked == true)
            {
                IsActive = 1;
            }
            else
            {
                IsActive = 0;
            }
            cmd.Parameters.Add(new SqlParameter("@IsActive", IsActive));
            cmd.Parameters.AddWithValue("@StaffID", comboBoxEmpUpdID.GetItemText(comboBoxEmpUpdID.SelectedValue));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated successfully!!!");

            textBoxEmpUpdName.Text = "";
            comboBoxEmpUpdDesignation.Text = "";
            radioButtonEmpUpdMale.Checked = false;
            radioButtonEmpUpdFemale.Checked = false;
            radioButtonEmpUpdOthers.Checked = false;
            textBoxEmpUpdAge.Text = "";
            comboBoxEmpUpdBloodGroup.Text = "";
            textBoxEmpUpdAddress.Text = "";
            textBoxEmpUpdPhone.Text = "";
            textBoxEmpUpdEmail.Text = "";
            checkBoxEmpUpdIsActive.Checked = false;
            comboBoxEmpUpdID.Text = "";
            con.Close();

            
            con.Open();
            string qryStaffupd = "SELECT * FROM Staff";
            cmd = new SqlCommand(qryStaffupd, con);
            DataTable dataTableStaffUpd = new DataTable();
            SqlDataAdapter sqlDataAdapterStaffUpd = new SqlDataAdapter(cmd);
            sqlDataAdapterStaffUpd.Fill(dataTableStaffUpd);
            dataGridViewEmpUpd.DataSource = dataTableStaffUpd.DefaultView;
            con.Close();
        }
        //Update Panel Employee Exit Button
        private void buttonEmpUpdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
