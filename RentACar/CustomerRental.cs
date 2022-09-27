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
    public partial class CustomerRental : Form
    {
        public CustomerRental()
        {
            InitializeComponent();
            carload();
        }

        SqlConnection con = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog= carrental; User ID=sa; Password=1234;");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string proid;
        string sql;
        string sql1;
        bool Mode = true;
        string id;

        public void carload()
        {
            cmd = new SqlCommand("select * from carreg", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcarid.Items.Add(dr["regno"].ToString());
            }
            con.Close();
        }


        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarid.SelectedItem.ToString();
            string custid = txtcustid.Text;
            string custname = txtcustname.Text;
            string fee = txtfee.Text;
            string date = txtdate.Value.Date.ToString("yyyy-MM-dd");
            string due = txtdue.Value.Date.ToString("yyyy-MM-dd");

            //id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "insert into rental(car_id,cust_id,custname,fee,date,due)values(@car_id,@cust_id,@custname,@fee,@date,@due)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@car_id", carid);
            cmd.Parameters.AddWithValue("@cust_id", custid);
            cmd.Parameters.AddWithValue("@custname", custname);
            cmd.Parameters.AddWithValue("@fee", fee);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@due", due);

            cmd.ExecuteNonQuery();



            sql1 = "update carreg set available = 'No' where regno = @regno ";

            cmd1 = new SqlCommand(sql1, con);
            cmd1.Parameters.AddWithValue("@regno", carid);
            cmd1.ExecuteNonQuery();

            MessageBox.Show("Record Added");

            con.Close();
            this.Close();
        }

        private void CustomerRental_Load(object sender, EventArgs e)
        {

        }

        private void txtcarmodel_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtcarid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtcustid_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from customer where custid = '" + txtcustid.Text + "' ", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcustname.Text = dr["custname"].ToString();
                }
                else
                {
                    MessageBox.Show("Customer ID not found");
                }
                con.Close();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtcarid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from carreg where regno = '" + txtcarid.Text + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string aval;
                string model;
                string make;
                aval = dr["available"].ToString();
                label10.Text = aval;
                model = dr["model"].ToString();
                label12.Text = model;
                make = dr["make"].ToString();   
                label14.Text = make;

                //if(aval == "No")
                //{
                //    txtcustid.Enabled=false;
                //    txtcustname.Enabled=false;
                //    txtfee.Enabled=false;
                //    txtdate.Enabled=false;
                //    txtdue.Enabled=false;
                //}
            }
            else
            {
                label10.Text = "Car Not Available";
                label12.Text = "N/A";
                label14.Text = "N/A";
            }
            con.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtdue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
                //cmd = new SqlCommand("select * from rental where custid = '" + txtcustid.Text + "' ", con);
                //sql = "select * from rental where date ='" + txtdate.Text + "'  and due ='" + txtdue.Text + "' ";
                //con.Open();
                //dr = cmd.ExecuteReader();
                //if(dr.Read())
                //{
                //    string StartDate = txtdate.Text;
                //    string EndDate = txtdue.Text;
                //    string Day

                //    if(StartDate == EndDate)
                //    {
                //    MessageBox.Show("You must select 01 more days for reserving...");
                //    }
                //    else(EndDate - StartDate).Day)
                //    {
                //    txtfee.Text = Day * 5000;
                //    }

                //}

                //if (dr.Read())
                //{
                //    txtcustname.Text = dr["custname"].ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Customer ID not found");
                //}
                //con.Close();
            }
    }
}
