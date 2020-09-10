using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ConsultaPreco
{
    public partial class frmProdutos : Form
    {
        public frmProdutos()
        {
            InitializeComponent();
        }

        private MySqlConnectionStringBuilder ConectaBanco()
        {
            //Criação da estrutura da conexão com o banco e passa os parametros
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "consulta_preco";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            return conexaoBD;
        }

        private void ExecutaComandoBanco(string pComando)
        {
            //Realizando a conexão com o banco
            MySqlConnectionStringBuilder conexaoBD = ConectaBanco();
            MySqlConnection abreConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                abreConexaoBD.Open(); // Inicia (abertura) conexão com o banco de dados

                MySqlCommand executacomandoMySql = abreConexaoBD.CreateCommand(); //Cria um comando SQL
                executacomandoMySql.CommandText = pComando;
                executacomandoMySql.ExecuteNonQuery();

                abreConexaoBD.Close(); //Finaliza (fechamento) conexão com o banco de dados
            }
            catch (Exception)
            {
                MessageBox.Show("Atenção! Não foi possivel realizar a conexão com o banco de dados! ");

            }
        }

        private void Pesquisar(string pComando)
        {
            //Realizando a conexão com o banco
            MySqlConnectionStringBuilder conexaoBD = ConectaBanco();
            MySqlConnection abreConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                abreConexaoBD.Open(); //Inicia (abertura) conexão com o banco de dados

                MySqlCommand executacomandoMySql = abreConexaoBD.CreateCommand(); //Crio um comando SQL
                executacomandoMySql.CommandText = pComando;
                MySqlDataReader reader = executacomandoMySql.ExecuteReader();  //Executa comando no banco de dados para localizar registros

                dataGridViewPesquisar.Rows.Clear(); // Limpa o grid

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewPesquisar.Rows[0].Clone(); //Realiza um cast e clona o registro
                    row.Cells[0].Value = reader.GetInt32(0); // idProduto
                    row.Cells[1].Value = reader.GetString(4); // descricaoProduto
                    row.Cells[2].Value = reader.GetString(3); // codigobarrasProduto
                    row.Cells[3].Value = reader.GetString(5); // unidadeMedidaProduto
                    row.Cells[4].Value = reader.GetString(6); // aplicacaoProduto
                    row.Cells[5].Value = reader.GetString(8); // precoProduto
                    dataGridViewPesquisar.Rows.Add(row); // Adiciona a linha no grid
                }

                abreConexaoBD.Close(); // Finaliza (fechamento) conexão com o banco de dados
            }

            catch (Exception)
            {
                MessageBox.Show("Atenção! Não foi possivel realizar a conexão com o banco de dados! ");
            }

        }

        private void LimpaCampos()
        {
            txtCodigo.Text = "";
            txtDescricao.Text = "";
            txtCodBar.Text = "";
            txtUn.Text = "";
            txtAplicacao.Text = "";
            mtxtPreco.Text = "";

            dataGridViewPesquisar.Rows.Clear(); // Limpa grade de pesquisa

            txtDescricao.Focus();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
            string comandoSQL = "";

            if (String.IsNullOrEmpty(txtDescricao.Text))
            {
                MessageBox.Show("Atenção! É obrigatório o preenchimento da Descrição!"); // Valida o preenchimento do campo Descrição
                
                txtDescricao.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                // Comando para gravar inclusão (INSERT)
                comandoSQL = "INSERT INTO produto (descricaoProduto, codigobarrasProduto, unidadeMedidaProduto, aplicacaoProduto, precoProduto) " +
                               "VALUES('" + txtDescricao.Text + "', '" +
                                            txtCodBar.Text + "', '" +
                                            txtUn.Text + "', '" +
                                            txtAplicacao.Text + "', '" +
                                            mtxtPreco.Text + "')";
                
                ExecutaComandoBanco(comandoSQL); // Executa comando no banco de dados para gravação de registro

                MessageBox.Show("Produto incluído com sucesso!");

                LimpaCampos();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string comandoSQL = "";

            if (!String.IsNullOrEmpty(txtCodigo.Text))
            {
                // Solicita confirmação ao usuário para alteração do registro
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja alterar o produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    // Comando para gravar alteração (UPDATE)
                    comandoSQL = "UPDATE produto SET " +
                                "descricaoProduto = '" + txtDescricao.Text + "', " +
                                "codigobarrasProduto = '" + txtCodBar.Text + "', " +
                                "unidadeMedidaProduto = '" + txtUn.Text + "', " +
                                "aplicacaoProduto = '" + txtAplicacao.Text + "', " +
                                "precoProduto = '" + mtxtPreco.Text + "' " +
                        " WHERE idProduto = '" + txtCodigo.Text + "'";


                    ExecutaComandoBanco(comandoSQL); // Executa comando no banco de dados para gravação de registro

                    MessageBox.Show("Produto alterado com sucesso!");

                    LimpaCampos();
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string comandoSQL = "";

            // Realização seleção de todos os registros caso não for informado a pesquisa
            if (String.IsNullOrEmpty(txtPesquisar.Text))
            {
                comandoSQL = "SELECT * FROM produto";

                Pesquisar(comandoSQL);
            }

            // Realiza a seleção dos registros através aplicando o filtro pelo código ou descrição
            else
            {
                comandoSQL = "SELECT * FROM produto WHERE idProduto = '" + txtPesquisar.Text + "' OR descricaoProduto LIKE '%" + txtPesquisar.Text + "%'";

                Pesquisar(comandoSQL);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodigo.Text))
            {
                // Solicita confirmação ao usuário para exclusão do registro
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir o produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    // Comando para gravar exclusão (DELETE)
                    string comandoSQL = "DELETE FROM produto WHERE idProduto = '" + txtCodigo.Text + "'";

                    ExecutaComandoBanco(comandoSQL); // Executa comando no banco de dados para exclusão do registro

                    MessageBox.Show("Produto excluído com sucesso!");

                    LimpaCampos(); //Limpa os campos
                }
            }
        }

        private void dataGridViewPesquisar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ao selecionar o registro no grid, habilita na aba principal os dados para edição
            if (dataGridViewPesquisar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridViewPesquisar.CurrentRow.Selected = true;
                //preenche os textbox com as células da linha selecionada
                txtCodigo.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnCodigo"].FormattedValue.ToString();
                txtDescricao.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnDescricao"].FormattedValue.ToString();
                txtCodBar.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnCodBar"].FormattedValue.ToString();
                txtUn.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnUN"].FormattedValue.ToString();
                txtAplicacao.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnAplicacao"].FormattedValue.ToString();
                mtxtPreco.Text = dataGridViewPesquisar.Rows[e.RowIndex].Cells["ColumnPreco"].FormattedValue.ToString();
            }
        }
    }
}
