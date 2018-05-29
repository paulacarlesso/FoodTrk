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
    public partial class TelaListaBebida : Form
    {
        public TelaListaBebida()
        {
            InitializeComponent();
        }

        private void TelaListaBebida_Load(object sender, EventArgs e)
        {
            CarregarBebidas();
        }

        private void AbreTelaInclusaoAlteracao(Bebida bebidaSelecionado)
        {
            ManterBebida tela = new ManterBebida();
            tela.MdiParent = this.MdiParent;
            tela.BebidaSelecionada = bebidaSelecionado;
            tela.FormClosed += Tela_FormClosed;
            tela.Show();
        }

        private void Tela_FormClosed(object sender, FormClosedEventArgs e)
        {
            CarregarBebidas();
        }

        private void CarregarBebidas()
        {
            dgBebida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgBebida.MultiSelect = false;
            dgBebida.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBebida.AutoGenerateColumns = false;
            List<Bebida> bebida = Program.Gerenciador.TodasAsBebidas();
            dgBebida.DataSource = bebida;
        }

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            AbreTelaInclusaoAlteracao(null);
        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            if (VerificarSelecao())
            {
                DialogResult resultado = MessageBox.Show("Tem certeza?", "Quer remover?", MessageBoxButtons.OKCancel);
                if (resultado == DialogResult.OK)
                {
                    Bebida bebidaSelecionada = (Bebida)dgBebida.SelectedRows[0].DataBoundItem;
                    var validacao = Program.Gerenciador.RemoverBebida(bebidaSelecionada);
                    if (validacao.Valido)
                    {
                        MessageBox.Show("Cliente removido com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um problema ao remover o cliente");
                    }
                    CarregarBebidas();
                }
            }
        }
        private bool VerificarSelecao()
        {
            if (dgBebida.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            return true;
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarSelecao())
            {
                Bebida bebidaSelecionada = (Bebida)dgBebida.SelectedRows[0].DataBoundItem;
                AbreTelaInclusaoAlteracao(bebidaSelecionada);
            }
        }

        private void dgBebida_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
