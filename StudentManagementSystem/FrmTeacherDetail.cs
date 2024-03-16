using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class FrmTeacherDetail : Form
    {
        public FrmTeacherDetail()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DPCM;Initial Catalog=DbNotKayıt;Integrated Security=True;");
        private void FrmTeacherDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayıtDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT SUM (CASE WHEN DURUM = 1 THEN 1 ELSE 0 END) FROM TBLDERS", connection);
            cmd1.ExecuteNonQuery();
            LblPasses.Text = Convert.ToString(cmd1.ExecuteScalar());

            SqlCommand cmd2 = new SqlCommand("SELECT SUM (CASE WHEN DURUM = 0 THEN 1 ELSE 0 END) FROM TBLDERS", connection);
            cmd2.ExecuteNonQuery();
            LblFailures.Text = Convert.ToString(cmd2.ExecuteScalar());
            connection.Close();

        }
        private void btnStuRegister_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TBLDERS (OGRNUMARA, OGRAD, OGRSOYAD) VALUES(@p1, @p2, @p3);", connection);
            cmd.Parameters.AddWithValue("@p1", MtbStuNum.Text);
            cmd.Parameters.AddWithValue("@p2", TbRegName.Text);
            cmd.Parameters.AddWithValue("@p3", TbRegSurname.Text);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("The student has registered to system successfully!");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }

        private void btnStuUpdate_Click(object sender, EventArgs e)
        {
            double averageNote, exam1, exam2, exam3;
            exam1 = Convert.ToDouble(TbExam1.Text);
            exam2 = Convert.ToDouble(TbExam2.Text);
            exam3 = Convert.ToDouble(TbExam3.Text);
            connection.Open();
            averageNote = (exam1 + exam2 + exam3) / 3;
            LblAvg.Text = averageNote.ToString();
            SqlCommand cmd = new SqlCommand("UPDATE TBLDERS SET OGRS1 = @p1, OGRS2 = @p2, OGRS3 = @p3, ORTALAMA=@p4, DURUM=@p5 WHERE OGRNUMARA=@p6",connection);
            cmd.Parameters.AddWithValue("@p1", TbExam1.Text);
            cmd.Parameters.AddWithValue("@p2", TbExam2.Text);
            cmd.Parameters.AddWithValue("@p3", TbExam3.Text);
            cmd.Parameters.AddWithValue("@p4", averageNote);
            cmd.Parameters.AddWithValue("@p5", averageNote > 50 ? 1 : 0);
            cmd.Parameters.AddWithValue("@p6", MtbStuNum.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("The student's entries has updated to system successfully!");

            SqlCommand cmd1 = new SqlCommand("SELECT SUM (CASE WHEN DURUM = 1 THEN 1 ELSE 0 END) FROM TBLDERS", connection);
            cmd1.ExecuteNonQuery();
            LblPasses.Text = Convert.ToString(cmd1.ExecuteScalar());

            SqlCommand cmd2 = new SqlCommand("SELECT SUM (CASE WHEN DURUM = 0 THEN 1 ELSE 0 END) FROM TBLDERS", connection);
            cmd2.ExecuteNonQuery();
            LblFailures.Text = Convert.ToString(cmd2.ExecuteScalar());

            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            MtbStuNum.Text = dataGridView1.Rows[selectedRow].Cells[1].Value.ToString();
            TbRegName.Text= dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
            TbRegSurname.Text = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
            TbExam1.Text = dataGridView1.Rows[selectedRow].Cells[4].Value.ToString();
            TbExam2.Text = dataGridView1.Rows[selectedRow].Cells[5].Value.ToString();
            TbExam3.Text = dataGridView1.Rows[selectedRow].Cells[6].Value.ToString();
        }
    }
}
