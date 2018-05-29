using Foodtruck.Negocio;
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
    public partial class ManterBebida : Form
    {
        public Bebida BebidaSelecionada { get; set; }

        public ManterBebida()
        {
            InitializeComponent();
        }

        private void ManterBebi_Shown(object sender, EventArgs e)
        {
            if (BebidaSelecionada != null)
            {
                this.tbId.Text = BebidaSelecionada.Id.ToString();
                this.tbNome.Text = BebidaSelecionada.Nome;
                this.tbTamanho.Text = Convert.ToString(BebidaSelecionada.Tamanho);
                this.tbValor.Text = Convert.ToString(BebidaSelecionada.Valor);
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            Bebida bebida = new Bebida();
            if (Int64.TryParse(tbId.Text, out long value))
            {
                bebida.Id = value;
            }
            else
            {
                bebida.Id = -1;
                //passa indentificador com valor negativo se não conseguir converter
            }
            bebida.Tamanho = Convert.ToSingle(tbTamanho.Text);
            bebida.Nome = tbNome.Text;
            bebida.Valor = Convert.ToDecimal(tbValor.Text);

            Validacao validacao;
            if (BebidaSelecionada == null)
            {
                validacao = Program.Gerenciador.CadastraBebida(bebida);
            }
            else
            {
                validacao = Program.Gerenciador.AlterarBebida(bebida);
            }



            if (!validacao.Valido)
            {
                String mensagemValidacao = "";
                foreach (var chave in validacao.Mensagens.Keys)
                {
                    String msg = validacao.Mensagens[chave];
                    mensagemValidacao += msg;
                    mensagemValidacao += Environment.NewLine;
                }
                MessageBox.Show(mensagemValidacao);
            }
            else
            {
                MessageBox.Show("Bebida salva com sucesso");
            }

            this.Close();
        }

        private void btCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManterBebida_Load(object sender, EventArgs e)
        {

        }
    }
}
