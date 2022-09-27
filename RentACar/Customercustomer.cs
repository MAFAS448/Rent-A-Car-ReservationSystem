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
    public partial class Customercustomer : Form
    {
        public Customercustomer()
        {
            InitializeComponent();
            Autono();
        }

        SqlConnection con = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog= carrental; User ID=sa; Password=1234;");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;


        public void Autono()
        {
            sql = "select custid from customer order by custid desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("00001");
            }
            else
            {
                proid = ("00001");
            }

            txtid.Text = proid.ToString();

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = txtid.Text;
            string custname = txtname.Text;
            string address = txtaddress.Text;
            string mobile = txtmobile.Text;

            //id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
            {
                sql = "insert into customer(custid,custname,address,mobile)values(@custid,@custname,@address,@mobile)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@custid", id);
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added & Your ID is '" + id + "' Use the ID for reserve the Car ");

                txtname.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtname.Focus();





            }
            else
            {
                sql = "update customer set custname=@custname,address = @address, mobile =@mobile where custid = @custid)";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@custid", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated");
                txtid.Enabled = true;
                Mode = true;

                txtname.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtname.Focus();

            }

            con.Close();
            this.Close();
        }

        private void txtmobile_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtmobile.Text.Length < 10)
            {
                LblErr.Text = "Enter Correct Mobile Number";
                return;
            }
        }
    }
}
