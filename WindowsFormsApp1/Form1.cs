using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Rootobject
        {
            public Competidores[] Property1 { get; set; }

        }

        private void Inserir()
        {
            //Instância da conexão onde passo a
            //ConnectionString
            var conn = new SqlConnection(@"Server =.\sqlexpress; Database = luta; Trusted_Connection = True;");
            //sql que será executado na tabela cliente
            var sql = "INSERT INTO tbnomes (Id, Nome,Idade, ArtesMarciais, TotalLutas, Derrotas, Vitorias) " +
                      "VALUES (@Id, @Nome, @Idade,@ArtesMarciais, @TotalLutas, @Derrotas, @Vitorias)";
            //instância do comando onde passo
            //o sql e a conexão como parâmetro
            var cmd = new SqlCommand(sql, conn);
            //abro a conexão
            conn.Open();

            //percorro o DataGridView
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //limpo os parâmetros
                cmd.Parameters.Clear();
                //crio os parâmetro do comando
                //e passo as linhas do dgv para eles
                //onde a célula indica a coluna do dgv
                cmd.Parameters.AddWithValue("@Id",
                    dataGridView1.Rows[i].Cells[0].Value);
                cmd.Parameters.AddWithValue("@Nome",
                    dataGridView1.Rows[i].Cells[1].Value);
                cmd.Parameters.AddWithValue("@Idade",
                    dataGridView1.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@ArtesMarciais",
                    dataGridView1.Rows[i].Cells[3].Value);
                cmd.Parameters.AddWithValue("@TotalLutas",
                    dataGridView1.Rows[i].Cells[3].Value);
                cmd.Parameters.AddWithValue("@Derrotas",
                    dataGridView1.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@Vitorias",
                    dataGridView1.Rows[i].Cells[5].Value);
                //executo o comando
                cmd.ExecuteNonQuery();
            }
            //Fecho conexão
            conn.Close();
        }
        public class Competidores        {
            public int id { get; set; }
            public string nome { get; set; }
            public int idade { get; set; }
            public string[] artesMarciais { get; set; }
            public int lutas { get; set; }
            public int derrotas { get; set; }
            public int vitorias { get; set; }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://177.36.237.87/lutadores/api/competidores");
                var resposta = await client.GetAsync("");
                string dados = await resposta.Content.ReadAsStringAsync();
                List<Competidores> cliente = new JavaScriptSerializer().Deserialize<List<Competidores>>(dados);
                dataGridView1.DataSource = cliente;
                   
            }

            if (MessageBox.Show("Lista de lutadores Completa, Agora selecione Lutadores para Competição", "Competição", MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                Inserir();
                this.Hide();
                Form2 formu = new Form2();
                formu.Show();
            }
            else
            {
                this.Close();
            }
            

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 formu = new Form2();
            formu.Show();
        }
    }
}
