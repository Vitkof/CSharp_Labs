using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace ebooks
{
    public partial class Author : Form
    {
        public Author()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private void ShowData()
        {
            dataGridView1.Rows.Clear();

            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();

            string sqlcoman = "SELECT count (id) FROM автор WHERE  id >0 ";
            string sqlcoman2 = "SELECT *  FROM автор WHERE  id >0 ORDER BY фио";

            OleDbCommand comm = new OleDbCommand(sqlcoman, Connection);
            Int32 kol = (Int32)comm.ExecuteScalar();

            if (kol > 0) dataGridView1.RowCount = kol;
            else dataGridView1.RowCount = 1;

            OleDbCommand comm1 = new OleDbCommand(sqlcoman2, Connection);
            OleDbDataReader myReader = comm1.ExecuteReader(CommandBehavior.CloseConnection);

            int i = 0;
            while (myReader.Read())
            {
                for (int j = 0; j < 2; j++)
                {
                    dataGridView1[j, i].Value = Convert.ToString(myReader[j]);
                }
                i++;
            }
            Connection.Close();


        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void avtor_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nomer = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value);

            if (MessageBox.Show("Удалить автора " + nomer + " ?\n\nПродолжить?", "удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                OleDbConnection Connection = new OleDbConnection(connectionString);
                string DelId = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value);

                string TextCommand = "DELETE FROM автор WHERE id=" + DelId;
                OleDbCommand Command = new OleDbCommand(TextCommand, Connection);
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();

                MessageBox.Show("данные удалены");
            }

            ShowData();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAuthor a = new AddAuthor();
            a.ShowDialog();
            ShowData();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EditAuthor a = new EditAuthor();
                a.label_ID.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value);
                a.textBox1.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value);

                a.ShowDialog();
                ShowData();
            }
            catch (Exception) { MessageBox.Show("выберите запись", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
      
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
