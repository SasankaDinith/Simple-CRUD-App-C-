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

namespace Demoform02
{
    public partial class Form1 : Form
    {

        SqlConnection conn=new SqlConnection("Data Source=DESKTOP-AORRCTR\\SQLEXPRESS;Initial Catalog=Demoform02;Integrated Security=True;Encrypt=False");
        SqlDataAdapter adp;
        SqlCommand cmd;    
        DataTable dt;
        


        public Form1()
        {
            InitializeComponent();
        }

        private void load_data()
        {
            conn.Open();
                       
             cmd = new SqlCommand("Select * from [Details]",conn);
            adp=new SqlDataAdapter();
            adp.SelectCommand = cmd;    
            dt=new DataTable();
            //dt.Clear();
            adp.Fill(dt);   
           dataGridView1.DataSource = dt;  
            dataGridView1.ReadOnly = true;  
            conn.Close();
        }

        private void clear_data()
        {
            txtid.Text = txtName.Text = txtaddress.Text = " ";
        }
        


        private void Form1_Load(object sender, EventArgs e)
        {
            load_data();
        }



        private void parameters()
        {
            cmd.Parameters.AddWithValue("Id",txtid.Text);
            cmd.Parameters.AddWithValue("Name",txtName.Text);
            cmd.Parameters.AddWithValue("Address",txtaddress.Text);
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtaddress.Text) )
            {
                MessageBox.Show("Please fill all the records","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            else
            {
                cmd = new SqlCommand("Insert into [Details] values (@ID,@Name,@Address)", conn);

                parameters();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                load_data();
                MessageBox.Show("Data added sucessfully", "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                clear_data();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index=e.RowIndex;

            DataGridViewRow selectRow = dataGridView1.Rows[index];
            
            txtid.Text = selectRow.Cells[0].Value.ToString();
            txtName.Text = selectRow.Cells[1].Value.ToString();
            txtaddress.Text = selectRow.Cells[2].Value.ToString();  
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtid.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtaddress.Text))
            {
                MessageBox.Show("Please fill the all recods", "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {
                cmd = new SqlCommand("Update [Details] set Id=@Id,Name=@Name,Address=@Address where Id=@Id",conn);
                parameters();
                conn.Open();
              cmd.ExecuteNonQuery();
                conn.Close();
                load_data() ;
                MessageBox.Show("Data updated sucessfully", "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                clear_data() ;
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            
                cmd = new SqlCommand("Delete from [Details] where Id=@Id", conn);
            parameters();
            conn.Open();
            int i = cmd.ExecuteNonQuery();  
            conn.Close();
            load_data();
            MessageBox.Show("Data deleted sucessfully", "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
           
            clear_data();   

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT * FROM [Details] WHERE Id LIKE @Search + '%'  OR Name LIKE @Search + '%' OR Address Like @Search + '%'", conn);
            cmd.Parameters.AddWithValue("Search", txtsearch.Text);
            adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            dt = new DataTable();
            dt.Clear();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
           

        }
    }
}
