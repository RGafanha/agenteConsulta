using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConsultaAlerta
{
    public partial class relatorio : Form
    {
        public relatorio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string queryup = "UPDATE alerta SET relatorio=@txt WHERE relatorio='relatorio'";
                string MySQLConnectionStringup = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@;database=tcrg";

                MySqlConnection conn = new MySqlConnection(MySQLConnectionStringup);

                MySqlCommand comm = new MySqlCommand(queryup, conn);
                comm.Parameters.AddWithValue("@txt", txtrel.Text);

                conn.Open();

                    comm.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                string MySQLConnectionStringup = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@;database=tcrg";

                MySqlConnection conn = new MySqlConnection(MySQLConnectionStringup);
                conn.Close();
            }
            MessageBox.Show("Enviado com sucesso", "Atenção", MessageBoxButtons.OK,MessageBoxIcon.Information);
            ActiveForm.Close();
        }
    }
}
