using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace ebooks
{
    public partial class AddThematic : Form
    {
        public AddThematic()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private void add_tematik_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();
            OleDbCommand comm = new OleDbCommand("SELECT max(id) FROM тематика", Connection);
            Int32 max = (Int32)comm.ExecuteScalar();
            if ((textBox1.Text.Length != 0))
            {

                string TextCommand = "INSERT INTO тематика VALUES(" + Convert.ToString(max + 1) + ",'";
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
    }
}
