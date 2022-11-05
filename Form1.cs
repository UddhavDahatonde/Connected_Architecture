using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Xml.Linq;
using System.Data;

namespace Connected_Architecture
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            conn = new SqlConnection(constr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Employee values(@name,@salary)";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@name", txtempname.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtempsalary.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Employee set name=@name,salary=@salary where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@name", txtempname.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtempsalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtempid.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Employee where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtempid.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtempid.Text));
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtempname.Text = reader["name"].ToString();
                        txtempsalary.Text = reader["salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee";
                cmd = new SqlCommand(qry, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
