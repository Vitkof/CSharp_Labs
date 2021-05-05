using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace ebooks
{
    public partial class AddAuthor : Form
    {
        public AddAuthor()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();
            OleDbCommand comm = new OleDbCommand("SELECT max(id) FROM автор", Connection);
            Int32 max = (Int32)comm.ExecuteScalar();
            if ((textBox1.Text.Length != 0))
            {

                string TextCommand = "INSERT INTO автор VALUES(" + Convert.ToString(max + 1) + ",'";
                TextCommand += textBox1.Text + "')";

                OleDbCommand Command = new OleDbCommand(TextCommand, Connection);
                Command.ExecuteNonQuery();
                MessageBox.Show("Данные добавлены", "Добавление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Пустое значение", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Connection.Close();
            Close();
        }

        private void add_avtor_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
