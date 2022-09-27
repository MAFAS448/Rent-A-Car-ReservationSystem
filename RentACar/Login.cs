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
    public partial class Login : Form
    {
        public Login()
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


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registraion registraion = new Registraion();
            registraion.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            

            if (txtuname.Text != "" && txtpswd.Text != "")
            {
                sql = "select * from custreg where UserName ='" + txtuname.Text + "'  and Password ='" + txtpswd.Text + "' ";
                cmd = new SqlCommand(sql, con);
                con.Open();
                dr = cmd.ExecuteReader();

               
               if(dr.Read())
                {
                    dr.Close();
                    this.Hide();
                    CustomerMain customerMain = new CustomerMain();
                    customerMain.Show();

                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Invalid username & password","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please to fill all field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            con.Close();
        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
    
}
