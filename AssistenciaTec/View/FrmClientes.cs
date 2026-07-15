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
            dataGridView1.Columns.Add(colunaId);

            DataGridViewTextBoxColumn colunaNome = new DataGridViewTextBoxColumn();
            colunaNome.DataPropertyName = "Name";
            colunaNome.HeaderText = "Nomes";
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


        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            HabilitarBotoesCancelarSalvar();
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DesabilitarBotoesCancelarSalvar();
        }

        private void toolStripButtonSalvar_Click(object sender, EventArgs e)
        {
            //criar um cliente 
            Cliente cliente = new Cliente();
            cliente.Name = TxtNome.Text;
            cliente.Email = TxtEmail.Text;
            cliente.Telefone = TxtTelefone.Text;
            cliente.Endereco = TxtEndereco.Text;

            //criar repositorio cliente
            ClienteRepository clienteRepository = new ClienteRepository();
            var clienteId = clienteRepository.Salvar(cliente);

            //mostrar o Id do novo cliente
            LabelId.Text = clienteId.ToString();


            DesabilitarBotoesCancelarSalvar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
