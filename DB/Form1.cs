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

namespace DB
{
    public partial class Form1 : Form
    {

        SqlConnection sqlConn;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string Connection = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\DB\DB\Database1.mdf;Integrated Security=True";
                sqlConn = new SqlConnection(Connection);
                await sqlConn.OpenAsync();
                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM [Book]", sqlConn);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns.Add("Title", "Title");
                    dataGridView1.Columns.Add("Author", "Author");
                    while (await sqlReader.ReadAsync())
                    {
                        int rowNumber = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowNumber].Cells["Id"].Value = Convert.ToString(sqlReader["Id"]);
                        dataGridView1.Rows[rowNumber].Cells["Title"].Value = Convert.ToString(sqlReader["Title"]);
                        dataGridView1.Rows[rowNumber].Cells["Author"].Value = Convert.ToString(sqlReader["Author"]);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }

                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
            {
                sqlConn.Close();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                    && (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)))
                {

                    SqlCommand command = new SqlCommand("INSERT INTO [book] (Title, Author) VALUES (@Title, @Author)", sqlConn);
                    command.Parameters.AddWithValue("Title", textBox1.Text);
                    command.Parameters.AddWithValue("Author", textBox2.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else {
                    MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void updateTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM [Book]", sqlConn);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns.Add("Title", "Title");
                    dataGridView1.Columns.Add("Author", "Author");
                    while (await sqlReader.ReadAsync())
                    {
                        int rowNumber = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowNumber].Cells["Id"].Value = Convert.ToString(sqlReader["Id"]);
                        dataGridView1.Rows[rowNumber].Cells["Title"].Value = Convert.ToString(sqlReader["Title"]);
                        dataGridView1.Rows[rowNumber].Cells["Author"].Value = Convert.ToString(sqlReader["Author"]);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }

                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
                    && (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                    && (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)))
                {
                    SqlCommand command = new SqlCommand("UPDATE [book] SET [Title]=@Title, [Author]=@Author WHERE [Id]=@Id", sqlConn);
                    command.Parameters.AddWithValue("Title", textBox4.Text);
                    command.Parameters.AddWithValue("Author", textBox3.Text);
                    command.Parameters.AddWithValue("Id", textBox5.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if(!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)){
                    MessageBox.Show("Заполните поле ID!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else{
                    MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [book] WHERE [Id]=@Id", sqlConn);
                    command.Parameters.AddWithValue("Id", textBox6.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else
                {
                    MessageBox.Show("Заполните одно поле для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Добавлять можно только полностью заполнив поля.\n2. Обновление происходит по ID. Также нужно полностью заполнить поля.\n3. Удаление происходит по ID.\n4. НЕ ЗАБЫВАЙТЕ ОБНОВЛЯТЬ БАЗУ ПОСЛЕ ВНЕСЕНИЯ ИЗМЕНЕНИЙ");
        }
    }
}
