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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private DataSet _DataSet;
        private SqlConnection _Conn;
        private SqlDataAdapter _DataAdapterProducts;

        private void inicializaListView()
        {
            // exibe detalhes
            listView1.View = View.Details;
            // permite ao usuário editar o texto
            listView1.LabelEdit = true;
            // permite ao usuário rearranjar as colunas
            listView1.AllowColumnReorder = true;
            // Selecione o item e subitem quando um seleção for feita
            listView1.FullRowSelect = true;
            // Exibe as linhas no ListView
            listView1.GridLines = true;

            // Anexa Subitems no ListView
            listView1.Columns.Add("ID", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Nome", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Idade", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("ArteMarciais", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Lutas", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Vitorias", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Derrotas", 70, HorizontalAlignment.Left);
        }

       
        // Carrega dados de um DataSet no ListView
        private void carregaLista()
        {
            // Obtem a tabela do dataset
            DataTable dtable = _DataSet.Tables["tbnomes"];

            // limpa o ListView
            listView1.Items.Clear();

            // exibe os itens no controle ListView 
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                // Somente as linhas que não foram deletadas
                if (drow.RowState != DataRowState.Deleted)
                {
                    // Define os itens da lista
                    ListViewItem lvi = new ListViewItem(drow["Nome"].ToString());
                    lvi.SubItems.Add(drow["id"].ToString());
                    lvi.SubItems.Add(drow["idade"].ToString());
                    lvi.SubItems.Add(drow["ArtesMarciais"].ToString());
                    lvi.SubItems.Add(drow["TotalLutas"].ToString());
                    lvi.SubItems.Add(drow["Derrotas"].ToString());
                    lvi.SubItems.Add(drow["Vitorias"].ToString());

                    // Inclui os itens no ListView
                    listView1.Items.Add(lvi);
                }
            }
        } //fim carregaLista
        public void GetDaDos()
        {
            string strConn = "Server =.\\sqlexpress; Database = luta; Trusted_Connection = True;";
            try
            {
                _Conn = new SqlConnection(strConn);

                // preenche o dataset 
                string strSQL = "SELECT id,Nome,Idade,ArtesMarciais,TotalLutas,Derrotas,Vitorias FROM tbnomes"; 

                _DataSet = new DataSet();
                _DataAdapterProducts = new SqlDataAdapter(strSQL, _Conn);
                _DataAdapterProducts.Fill(_DataSet, "tbnomes");
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();

                MessageBox.Show(msg, "Não foi possivel acessar os dados.",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            inicializaListView();
            GetDaDos();
            carregaLista();
                                             
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = listView1.SelectedItems[1].Text;
        }
    }
    
    }

 
    
    


