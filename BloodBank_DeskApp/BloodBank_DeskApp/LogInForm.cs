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

using BloodBank_DeskApp;

namespace BloodBank_DeskApp
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
            textBoxPassWord.PasswordChar = '*';
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonLogInExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT*FROM Users WHERE UserName = '" + textBoxUserName.Text + "' AND Password = '" + textBoxPassWord.Text + "' AND IsActive = 1", con);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Form1 fr = new Form1(dt);
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect User Name or Pass Word");
                }
            }
        }
    }
}
