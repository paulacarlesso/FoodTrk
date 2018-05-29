using Foodtruck.Negocio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Foodtruck.Grafico
{
    public partial class TelaListaLanche : Form
    {
        public TelaListaLanche()
        {
            InitializeComponent();
        }

        private void CarregarLanche()
        {
            dgLanche.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgLanche.MultiSelect = false;
            dgLanche.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLanche.AutoGenerateColumns = false;
            List<Lanche> bebida = Program.Gerenciador.TodosOsLanches();
            dgLanche.DataSource = bebida;
        }

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            AbreTelaInclusaoAlteracao(null);
        }

        private void dgLanches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AbreTelaInclusaoAlteracao(Lanche lancheSelecionado)
        {
            ManterLanche tela = new ManterLanche();
            tela.MdiParent = this.MdiParent;
            tela.LancheSelecionado = lancheSelecionado;
            tela.FormClosed += Tela_FormClosed;
            tela.Show();
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarSelecao())
            {
                Lanche lancheSelecionado = (Lanche)dgLanche.SelectedRows[0].DataBoundItem;
                AbreTelaInclusaoAlteracao(lancheSelecionado);
            }
        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            if (VerificarSelecao())
            {
                DialogResult resultado = MessageBox.Show("Tem certeza?", "Quer remover?", MessageBoxButtons.OKCancel);
                if (resultado == DialogResult.OK)
                {
                    Lanche lancheSelecionada = (Lanche)dgLanche.SelectedRows[0].DataBoundItem;
                    var validacao = Program.Gerenciador.RemoverLanche(lancheSelecionada);
                    if (validacao.Valido)
                    {
                        MessageBox.Show("Lanche removido com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um problema ao remover o Lanche");
                    }
                    CarregarLanche();
                }
            }
        }

        private bool VerificarSelecao()
        {
            if (dgLanche.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            return true;
        }

        private void Tela_FormClosed(object sender, FormClosedEventArgs e)
        {
            CarregarLanche();
        }

        private void TelaListaLanche_Load(object sender, EventArgs e)
        {
            CarregarLanche();
        }
    }
}
