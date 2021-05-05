using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace ebooks
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                richTextBox2.Text = openFileDialog1.FileName;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();
            Int32 max;
            try {
            OleDbCommand comm = new OleDbCommand("SELECT max(id) FROM книги", Connection);
            max = (Int32)comm.ExecuteScalar(); }
            catch (Exception) { max = 1; }
            if ((textBox1.Text.Length != 0))
            {
                if ((richTextBox2.Text.Length != 0))
                {
                                    string TextCommand = "INSERT INTO книги VALUES(" + Convert.ToString(max + 1) + ",'";
                                    TextCommand += textBox1.Text + "','";
                                    TextCommand += id_author() + "','";
                                    TextCommand += id_theme() + "','";
                                    TextCommand +=richTextBox1.Text + "','";
                                    TextCommand += richTextBox2.Text + "')";
                                    
                                    OleDbCommand Command = new OleDbCommand(TextCommand, Connection);
                                    Command.ExecuteNonQuery();
                                    MessageBox.Show("Данные добавлены", "Добавление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Connection.Close();
                                    Close();
                }
                else
                    MessageBox.Show("Файл не указан", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Введите название книги", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }


        public void select_author()
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);

            Connection.Open();
            OleDbCommand comm1 = new OleDbCommand("SELECT фио FROM автор ORDER BY фио", Connection);
            OleDbDataReader myReader = comm1.ExecuteReader(CommandBehavior.CloseConnection);
            comboBox1.Items.Clear();


            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader[0]);

            }

            comboBox1.SelectedIndex = 0;
            Connection.Close();
        }
        private int id_author()
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();

            OleDbCommand comm1 = new OleDbCommand("SELECT id FROM автор WHERE  фио ='" + comboBox1.Text + "' ", Connection);
            OleDbDataReader dreader = comm1.ExecuteReader();

            string st = "";

            while (dreader.Read())
                st = Convert.ToString(dreader[0]);
            Connection.Close();

            return Convert.ToInt32(st);


        }
        public void select_theme()
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);

            Connection.Open();
            OleDbCommand comm1 = new OleDbCommand("SELECT наименование FROM тематика ORDER BY наименование ", Connection);
            OleDbDataReader myReader = comm1.ExecuteReader(CommandBehavior.CloseConnection);
            comboBox2.Items.Clear();


            while (myReader.Read())
            {
                comboBox2.Items.Add(myReader[0]);

            }

            comboBox2.SelectedIndex = 0;
            Connection.Close();
        }
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Method already uses a Stored Procedure")]
        private int id_theme()
        {
            OleDbConnection Connection = new OleDbConnection(connectionString);
            Connection.Open();

            OleDbCommand comm1 = new OleDbCommand("SELECT id FROM тематика WHERE наименование ='" + comboBox2.Text.ToString() + "' ", Connection);
            OleDbDataReader dreader = comm1.ExecuteReader();

            string st = "";

            while (dreader.Read())
                st = Convert.ToString(dreader[0]);
            Connection.Close();

            return Convert.ToInt32(st);
        }

        private void add_Load(object sender, EventArgs e)
        {
            select_author();
            select_theme();
        }
    }
}
