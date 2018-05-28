using System;
using Foodtruck.Negocio.Models;
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

        private void AbreTelaInclusaoAlteracao(Bebida bebidaSelecionada)
        {
            ManterBebida tela = new ManterBebida();
            tela.MdiParent = this.MdiParent;
            tela.BebidaSelecionada = bebidaSelecionada;
            tela.FormClosed += Tela_FormClosed;
            tela.Show();
        }

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            AbreTelaInclusaoAlteracao(null);
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
            List<Bebida> clientes = Program.Gerenciador.TodasAsBebidas();
            dgBebida.DataSource = clientes;
        }

        private void TelaListaBebidas_Load(object sender, EventArgs e)
        {
            CarregarBebidas();
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
                        MessageBox.Show("Bebida removido com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um problema ao remover o bebida");
                    }
                    CarregarBebidas();
                }
            }
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
