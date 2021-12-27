using System;
using System.Windows.Forms;
using System.Media;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security.Principal;

namespace ConsultaAlerta
{
    public partial class AlertForm : Form
    {
        public static string basepath = Application.StartupPath;
        string query = "SELECT `id`, `TT`, `data` FROM tcrg.alerta WHERE status='1'";
        string MySQLConnectionString = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@";
        SoundPlayer simplesound = new SoundPlayer( basepath + @"alarme/alert2.wav");
        relatorio rel = new relatorio();
        public AlertForm()
        {
            InitializeComponent();
            simplesound.PlayLooping();
            GetDados();
        }
        public void GetDados()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySQLConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                conn.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds,"alerta");
                dgvDados.DataSource = ds.Tables["alerta"];
                conn.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error : " + e.Message);
            }
        }

        private void AlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f = new Form1();
            simplesound.Stop();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer simplesound = new SoundPlayer(basepath + @"alarme/alert2.wav");
            simplesound.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string queryup = "UPDATE alerta SET status=0, relatorio='relatorio', resolvidodata=@resolvidodata, usuario=@usuario WHERE id=@id";
            string MySQLConnectionStringup = "datasource=10.56.140.17;port=3306;username=tecon;password=tecon123@;database=tcrg";

            MySqlConnection conn = new MySqlConnection(MySQLConnectionStringup);

            MySqlCommand comm = new MySqlCommand(queryup, conn);
            
            

            conn.Open();

            for (int i = 0; i < dgvDados.Rows.Count; i++)
            {
                bool cellselect = Convert.ToBoolean(dgvDados.Rows[i].Cells[0].Value);
                if (cellselect == true)
                {
                    int id = Convert.ToInt32(dgvDados.Rows[i].Cells[1].Value);
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("resolvidodata", DateTime.Now);
                    comm.Parameters.AddWithValue("@usuario", WindowsIdentity.GetCurrent().Name);
                    comm.ExecuteNonQuery();
                    rel.ShowDialog()
;                    GetDados();
                    SoundPlayer simplesound = new SoundPlayer(basepath + @"alarme/alert2.wav");
                    simplesound.Stop();
                }
                
            }
            conn.Close(); 
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            GetDados();
        }
    }
    
}
