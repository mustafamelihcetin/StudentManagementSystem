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

namespace StudentManagementSystem
{
    public partial class FrmStudentDetail : Form
    {
        public FrmStudentDetail()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=DPCM;Initial Catalog=DbNotKayıt;Integrated Security=True;");
        public string number;
        private void FrmStudentDetail_Load(object sender, EventArgs e)
        {
            LblStudentNumber.Text = number;
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLDERS WHERE OGRNUMARA=@p1", connection);
            cmd.Parameters.AddWithValue("@p1", number);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                LblNameSurname.Text = reader[2].ToString() + " " + reader[3].ToString();
                LblExam1.Text = reader[4].ToString();
                LblExam2.Text = reader[5].ToString();
                LblExam3.Text = reader[6].ToString();
                LblAverage.Text = reader[7].ToString(); 
                LblStatus.Text = reader[8].ToString();
            }
            connection.Close();
        }
    }
}
