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
    public partial class rental : Form
    {
        public rental()
        {
            InitializeComponent();
            carload();
            rentalload();
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

        public void rentalload()
        {
            sql = "select * from rental";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
            }
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from carreg where regno = '" + txtcarid.Text + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string aval;
                aval = dr["available"].ToString();
                label9.Text = aval;

                //if(aval == "No")
                //{
                //    txtcustid.Enabled = false;
                //    txtcustname.Enabled = false;
                //    txtfee.Enabled = false;
                //    txtdate.Enabled = false;
                //    txtdue.Enabled = false;

                //}
                //else
                //{
                //    label9.Text = "Car Not Available";
                //}
                con.Close();
            }
        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarid.SelectedItem.ToString();
            string custid = txtcustid.Text;
            string custname = txtcustname.Text;
            string fee = txtfee.Text;
            string date = txtdate.Value.Date.ToString("yyyy-MM-dd");
            string due = txtdue.Value.Date.ToString("yyyy-MM-dd");

            
            if (Mode == true)
            {


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
            }
            else
            {
                sql = "update rental set custname = @custname,fee=@fee,date=@date,due=@due where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);

                
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@due", due);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated");
               

                Mode = true;

                //txtcustname.Clear();
                //txtfee.Clear();
                //txtdate.Items.Clear();
                //txtmake.Focus();
            }

            con.Close();


        }

        public void getid(String id)
        {
            sql = "select * from rental where id = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                //txtcarid.Text = dr[1].ToString();
                //txtcustid.Text = dr[2].ToString();
                txtcustname.Text = dr[3].ToString();
                txtfee.Text = dr[4].ToString();
                txtdate.Text = dr[5].ToString();
                txtdue.Text = dr[6].ToString();
            }
            con.Close();
        }

        private void rental_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtcarid.Enabled = false;
                txtcustid.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from rental where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recorded deleted");
                con.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            rentalload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcustid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
