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
    public partial class TelaListaPedidos : Form
    {
        Pedido pedido = new Pedido();
        public TelaListaPedidos()
        {
            InitializeComponent();
        }

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            AbreTelaInclusaoAlteracao(null);
        }

        private void AbreTelaInclusaoAlteracao(Pedido pedidoSelecionado)
        {
            AdicionaPedido tela = new AdicionaPedido();
            tela.MdiParent = this.MdiParent;
            tela.PedidoSelecionado = pedidoSelecionado;
            tela.FormClosed += Tela_FormClosed;
            tela.Show();
        }

        private void Tela_FormClosed(object sender, FormClosedEventArgs e)
        {
            CarregarPedidos();
        }

        private void CarregarPedidos()
        {
            dgPedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPedido.MultiSelect = false;
            dgPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPedido.AutoGenerateColumns = false;
            List<Pedido> pedido = Program.Gerenciador.TodosOsPedidos();
            dgPedido.DataSource = pedido;
        }

        private void TelaListaPedidos_Load(object sender, EventArgs e)
        {
            CarregarPedidos();
            this.WindowState = FormWindowState.Maximized;
        }
        private bool VerificarSelecao()
        {
            if (dgPedido.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            return true;
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            if(VerificarSelecao())
            {
                Pedido pedidoSelecionado = (Pedido)dgPedido.SelectedRows[0].DataBoundItem;
                AbreTelaInclusaoAlteracao(pedidoSelecionado);
            }
        }

        private void dgPedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
