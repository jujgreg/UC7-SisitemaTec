using AssistenciaTec.Data;
using AssistenciaTec.View;

namespace AssistenciaTec
{
    public partial class FrmPrincipal : Form
    {

        private FrmClientes frmClientes = null;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void MenuItemConectar_Click(object sender, EventArgs e)
        {
            Conexao.GetConexao();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frmClientes == null || frmClientes.IsDisposed)
            {
                frmClientes = new FrmClientes();
                frmClientes.MdiParent = this;
                frmClientes.Show();
            }
            else
            {
                if (frmClientes.WindowState == FormWindowState.Minimized)
                {
                    frmClientes.WindowState = FormWindowState.Normal;
                }
            }

        }

        private void FrmPrincipal_Leave(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var resposta = MessageBox.Show("Deseja Realmente fechar a aplicação",
                "Sair",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                Application.ExitThread();
            }else
            {
                e.Cancel = true;
            }
        }
    }
}
