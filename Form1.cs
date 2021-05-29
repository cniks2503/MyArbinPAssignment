using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=localhost;Database=database;Uid=root;Pwd=Nikita@123";
        int ID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            GridFill();
        }

        void Clear()
        {
            txtName.Text = txtContact.Text = txtGender.Text = txtHobbies.Text = txtKnowledge.Text = "";
            ID = 0;
            btnSave.Text = "Save";
        }

        void GridFill()
        {

            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("ViewAll", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbluser_info = new DataTable();
                sqlDa.Fill(dtbluser_info);
                dgvUser.DataSource = dtbluser_info;
                dgvUser.Columns[0].Visible = false;



            }
        }

        private void DgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("AddEdit", mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_userID", ID);
                mySqlCmd.Parameters.AddWithValue("_userName", txtName.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Contact", txtContact.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Gender", txtGender.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Knowledge", txtKnowledge.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Hobbies", txtHobbies.Text.Trim());
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
                Clear();
                GridFill();
            }
        }

        private void dgvUser_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUser.CurrentRow.Index != -1)
            {
                txtName.Text = dgvUser.CurrentRow.Cells[1].Value.ToString();
                txtContact.Text = dgvUser.CurrentRow.Cells[2].Value.ToString();
                txtGender.Text = dgvUser.CurrentRow.Cells[3].Value.ToString();
                txtKnowledge.Text = dgvUser.CurrentRow.Cells[4].Value.ToString();
                txtHobbies.Text = dgvUser.CurrentRow.Cells[5].Value.ToString();
                ID = Convert.ToInt32(dgvUser.CurrentRow.Cells[0].Value.ToString());
                btnSave.Text = "Update";


            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("Search", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_Value", txtSearch.Text);
                DataTable dtbluser_info = new DataTable();
                sqlDa.Fill(dtbluser_info);
                dgvUser.DataSource = dtbluser_info;
                dgvUser.Columns[0].Visible = false;
                txtSearch.Text = "";


            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("Delete", mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_userID", ID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                Clear();
                GridFill();
                btnSave.Text = "Save";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
