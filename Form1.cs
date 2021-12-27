using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Principal;

namespace ConsultaAlerta
{
    public partial class Form1 : Form
    {
        string query = "SELECT `TT`, `usuario`, `data`, `resolvidodata`, `relatorio` FROM tcrg.alerta WHERE status='0'";
        string MySQLConnectionString = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@";
        AlertForm alert;
        Control control = null;
        public Form1()
        {
            InitializeComponent();
            getRelatorio();
        }
        public void getRelatorio()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySQLConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                conn.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "alerta");
                dgvrel.DataSource = ds.Tables["alerta"];
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error : " + e.Message);
            }
        }
            public void consultaAlerta()
        {
            string query = "SELECT * FROM alerta";

            string MySQLConnectionString = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@;database=tcrg";

            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            

            try
            {
                databaseConnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                while (myReader.Read())
                {
                    if (alert == null)
                    {
                        if (myReader.GetInt32(3) == 1)
                            alert = new AlertForm();
                        else
                        { 
                            continue;
                        }
                    }
                    else
                    {
                        myReader.Close();
                        databaseConnection.Close();
                        return;
                    }                  
                    alert.ShowDialog();
                }
                myReader.Close();
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Query error : " + e.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            consultaAlerta();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (control == null)
                Hide();
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            getRelatorio();
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            notifyIcon1.Visible = true;
        }
    }
}
