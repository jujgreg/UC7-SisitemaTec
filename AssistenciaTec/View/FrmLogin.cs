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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //criar o repository
            UserRepository repository = new UserRepository();



            User usuario = repository.ConfirmarUsuario(txtEmail.Text, txtSenha.Text);

            if (usuario != null)
            {
                new FrmPrincipal().Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "Acesso negado, Email ou senha Incorreta.",
                    "Acesso Negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error

                    );
                txtEmail.Focus();


            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
