using AssistenciaTec.Model;
using AssistenciaTec.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistenciaTec.View
{
    public partial class FrmClientes : Form
    {

        private List<Cliente> clientes = new List<Cliente>();


        public FrmClientes()
        {
            InitializeComponent();
            DesabilitarBotoesCancelarSalvar();
            CarregarGridClientes();
        }

        private void CarregarGridClientes()
        {
            //criar o repositorio do get clientes
            ClienteRepository clienteRepository = new ClienteRepository();

            clientes = clienteRepository.GetAll();

            //carregar os dados
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colunaId = new DataGridViewTextBoxColumn();
            colunaId.DataPropertyName = "Id";
            colunaId.HeaderText = "Identidade";
            colunaId.Width = 80;
            dataGridView1.Columns.Add(colunaId);

            DataGridViewTextBoxColumn colunaNome = new DataGridViewTextBoxColumn();
            colunaNome.DataPropertyName = "Name";
            colunaNome.HeaderText = "Nomes";
            colunaNome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(colunaNome);

            //informar de onde vem os dados da tabela
            dataGridView1.DataSource = clientes;



        }
        private void CarregarGridClientes(string nome)
        {
            //criar o repositorio do get clientes
            ClienteRepository clienteRepository = new ClienteRepository();

            clientes = clienteRepository.pesquisarNome(nome);

            //carregar os dados
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colunaId = new DataGridViewTextBoxColumn();
            colunaId.DataPropertyName = "Id";
            colunaId.HeaderText = "Identidade";
            colunaId.Width = 80;
            dataGridView1.Columns.Add(colunaId);

            DataGridViewTextBoxColumn colunaNome = new DataGridViewTextBoxColumn();
            colunaNome.DataPropertyName = "Name";
            colunaNome.HeaderText = "Nomes";
            colunaNome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(colunaNome);

            //informar de onde vem os dados da tabela
            dataGridView1.DataSource = clientes;



        }

        private void DesabilitarBotoesCancelarSalvar()
        {
            toolStripButtonNovo.Enabled = true;
            toolStripButtonEditar.Enabled = true;
            toolStripButtonExcluir.Enabled = true;
            toolStripButtonSalvar.Enabled = false;
            toolStripButtonCancelar.Enabled = false;
            GroupBoxDadosCliente.Enabled = false;
        }

        private void HabilitarBotoesCancelarSalvar()
        {
            toolStripButtonNovo.Enabled = false;
            toolStripButtonEditar.Enabled = false;
            toolStripButtonExcluir.Enabled = false;
            toolStripButtonSalvar.Enabled = true;
            toolStripButtonCancelar.Enabled = true;
            GroupBoxDadosCliente.Enabled = true;
        }
        private void LimparCampos()
        {
            TxtNome.Clear();
            TxtEmail.Clear();
            TxtTelefone.Clear();
            TxtEndereco.Clear();
            LabelId.Text = string.Empty;

            TxtNome.Focus();
        }

        private void toolStripButtonNovo_Click(object sender, EventArgs e)
        {

            HabilitarBotoesCancelarSalvar();
            LimparCampos();
        }



        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DesabilitarBotoesCancelarSalvar();
        }

        private void toolStripButtonSalvar_Click(object sender, EventArgs e)
        {
            //criar um cliente 
            Cliente cliente = new Cliente();


            try
            {
                cliente.Name = TxtNome.Text;
                cliente.Email = TxtEmail.Text;
                cliente.Telefone = TxtTelefone.Text;
                cliente.Endereco = TxtEndereco.Text;
            }
            catch (ArgumentException Erro)
            {
                MessageBox.Show(Erro.Message);
                return;
            }

            //criar repositorio cliente
            ClienteRepository clienteRepository = new ClienteRepository();


            //validar se é salvar ou editar
            if (LabelId.Text == string.Empty)
            {
                var clienteId = clienteRepository.Salvar(cliente);


                //mostrar o Id do novo cliente
                LabelId.Text = clienteId.ToString();
            }

            else
            {
                cliente.Id = int.Parse(LabelId.Text);
                clienteRepository.atualizar(cliente);
            }





            DesabilitarBotoesCancelarSalvar();
            CarregarGridClientes();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            detalhesdoCliente(sender, e);

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            detalhesdoCliente(sender, e);

        }

        private void detalhesdoCliente(object sender, DataGridViewCellEventArgs e)
        {
            var linha = e.RowIndex;

            if (linha < 0)
            {
                linha = 0;
            }


            //pegar os dados da linha selecionada
            var linhaSelect = dataGridView1.Rows[linha];
            var clienteSelect = linhaSelect.DataBoundItem as Cliente;

            LabelId.Text = clienteSelect.Id.ToString();
            TxtNome.Text = clienteSelect.Name;
            TxtEmail.Text = clienteSelect.Email;
            TxtTelefone.Text = clienteSelect.Telefone;
            TxtEndereco.Text = clienteSelect.Endereco;
        }

        private void toolStripButtonExcluir_Click(object sender, EventArgs e)
        {

            var resposta = MessageBox.Show(
                 "deseja realmente excluir o cliente " + TxtNome.Text + " selecionado?",
                 "Exclusão cliente",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question


                 );
            if (resposta == DialogResult.Yes)
            {
                var clienteRepository = new ClienteRepository();
                var idSelecionado = int.Parse(LabelId.Text);

                var excluidos = clienteRepository.excluir(idSelecionado);
                if (excluidos > 0)
                {
                    MessageBox.Show(
                        "Cliente " + TxtNome.Text + " excluido com sucesso",
                        "Exclusao de cliente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
            }

            CarregarGridClientes();
        }
        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {

            HabilitarBotoesCancelarSalvar();


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregarGridClientes(txtBuscarNome.Text);
        }
    }
}
