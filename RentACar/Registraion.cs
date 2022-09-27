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

namespace RentACar
{
    public partial class Registraion : Form
    {
        public Registraion()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog= carrental; User ID=sa; Password=1234;");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            
            string uname = txtuname.Text;
            string email = txtemail.Text;
            string pswd = txtpswd.Text;
            string cpswd = txtcpswd.Text;

            if(uname =="" || email =="")
            {
                MessageBox.Show("Please fill the fields..");
            }

            else if(pswd == cpswd)
            {
                sql = "insert into custreg(UserName,Email,Password,ConfirmPassword)values(@UserName,@Email,@Password,@ConfirmPassword)";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserName", uname);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", pswd);
                cmd.Parameters.AddWithValue("@ConfirmPassword", cpswd);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Regitered Successfuly!!!");
                Mode = true;
                this.Close();


                txtuname.Clear();
                txtemail.Clear();
                txtpswd.Clear();
                txtcpswd.Clear();
                txtuname.Focus();
            }
            
            else
            {
                MessageBox.Show("Password doesn't match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
            
        }

        private void txtcpswd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
