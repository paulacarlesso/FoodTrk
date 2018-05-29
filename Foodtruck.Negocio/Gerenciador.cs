﻿using Foodtruck.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtruck.Negocio.Persistencia;

namespace Foodtruck.Negocio
{
    public class Gerenciador
    {
       
        private Banco banco = new Banco();

        public Validacao RemoverCliente(Cliente cliente)
        {
            Validacao validacao = new Validacao();
            banco.Clientes.Remove(cliente);
            banco.SaveChanges();
            return validacao;
        }

        public Validacao RemoverBebida(Bebida bebida)
        {
            Validacao validacao = new Validacao();
            banco.Bebidas.Remove(bebida);
            banco.SaveChanges();
            return validacao;
        }

        public Validacao RemoverLanche(Lanche lanche)
        {
            Validacao validacao = new Validacao();
            banco.Lanches.Remove(lanche);
            banco.SaveChanges();
            return validacao;
        }

        public Validacao AlterarCliente(Cliente clienteAlterado)
        {
            Validacao validacao = new Validacao();
            Cliente clienteBanco = BuscaClientePorId(clienteAlterado.Id);
            clienteBanco.Nome = clienteAlterado.Nome;
            clienteBanco.Email = clienteAlterado.Email;
            clienteBanco.CPF = clienteAlterado.CPF;
            this.banco.SaveChanges();
            return validacao;
        }

        public Validacao AlterarBebida(Bebida bebidaAlterada)
        {
            Validacao validacao = new Validacao();
            Bebida bebidaBanco = BuscaBebidaPorId(bebidaAlterada.Id);
            bebidaBanco.Nome = bebidaAlterada.Nome;
            bebidaBanco.Valor = bebidaAlterada.Valor;
            bebidaBanco.Tamanho = bebidaAlterada.Tamanho;
            this.banco.SaveChanges();
            return validacao;
        }

        public Validacao AlterarLanche(Lanche lancheAlterada)
        {
            Validacao validacao = new Validacao();
            Lanche lancheBanco = BuscaLanchePorId(lancheAlterada.Id);
            lancheBanco.Nome = lancheAlterada.Nome;
            lancheBanco.Valor = lancheAlterada.Valor;
            this.banco.SaveChanges();
            return validacao;
        }

        public Validacao AlterarPedido(Pedido pedidoAlterada)
        {
            Validacao validacao = new Validacao();
            Pedido pedidoBanco = BuscaPedidoPorId(pedidoAlterada.Id);
            pedidoBanco.Bebidas = pedidoAlterada.Bebidas;
            pedidoBanco.Lanches = pedidoAlterada.Lanches;
            pedidoBanco.DataCompra = pedidoAlterada.DataCompra;
            pedidoBanco.Cliente = pedidoAlterada.Cliente;
            this.banco.SaveChanges();
            return validacao;
        }

        public Validacao AdicionarCliente(Cliente clienteAdicionado)
        {
            Validacao validacao = new Validacao();
            if(clienteAdicionado.Id < 0)
            {
                validacao.Mensagens.Add("Id", "O indenfiticador deve constituir apenas números positivos");
            }
            //verifica se já tem alguma mensagem de erro e se tiver pula essa verificação
            if(this.banco.Clientes.Where(c => c.Id == clienteAdicionado.Id).Any() && validacao.Mensagens.Count == 0)
            {
                validacao.Mensagens.Add("Id", "Já existe um cliente com esse código");
            }

            if (String.IsNullOrEmpty(clienteAdicionado.CPF))
            {
                validacao.Mensagens.Add("CPF", "O campo CPF não pode ser nulo ou vazio");
            }

            if(this.banco.Clientes.Where(c => c.CPF == clienteAdicionado.CPF).Any() && validacao.Mensagens.Count == 0)
            {
                validacao.Mensagens.Add("CPF", "Já exite um cliente com esse CPF");
            }

            if (String.IsNullOrEmpty(clienteAdicionado.Nome))
            {
                validacao.Mensagens.Add("Nome", "O nome não pode ser nulo ou vazio");
            }

            if (String.IsNullOrEmpty(clienteAdicionado.Email))
            {
                validacao.Mensagens.Add("Email", "O email não pode ser nulo ou vazio");
            }

            if (!clienteAdicionado.Email.Contains("@") && validacao.Mensagens.Count == 0)
            {
                validacao.Mensagens.Add("Email", "Email no formato inválido");
            }

            if (validacao.Valido)
            {
                this.banco.Clientes.Add(clienteAdicionado);
                this.banco.SaveChanges();
            }

            return validacao;
        }

        public Validacao CadastraBebida(Bebida bebidaCadastrada)
        {
            Validacao validacao = new Validacao();

            if(this.banco.Bebidas.Where(x => x.Id == bebidaCadastrada.Id).Any())
            {
                validacao.Mensagens.Add("id", "Já existem uma bebida com esse código");
            }

            if(string.IsNullOrEmpty(bebidaCadastrada.Nome))
            {
                validacao.Mensagens.Add("nome", "O nome não pode ser nulo ou vazio");
            }

            if(string.IsNullOrEmpty(Convert.ToString(bebidaCadastrada.Tamanho)))
            {
                validacao.Mensagens.Add("tamanho", "O campo tamanho não pode ser nulo ou vazio");
            }

            if (string.IsNullOrEmpty(Convert.ToString(bebidaCadastrada.Valor)))
            {
                validacao.Mensagens.Add("valor", "O campo valor não pode ser nulo ou vazio");
            }

            if (validacao.Valido)
            {
                this.banco.Bebidas.Add(bebidaCadastrada);
                this.banco.SaveChanges();
            }
            return validacao;
        }

        public Validacao CadastraLanche(Lanche lancheCadastrado)
        {
            Validacao validacao = new Validacao();

            if (this.banco.Lanches.Where(x => x.Id == lancheCadastrado.Id).Any())
            {
                validacao.Mensagens.Add("id", "Já existem uma bebida com esse código");
            }

            if (string.IsNullOrEmpty(lancheCadastrado.Nome))
            {
                validacao.Mensagens.Add("nome", "O nome não pode ser nulo ou vazio");
            }

            if (string.IsNullOrEmpty(Convert.ToString(lancheCadastrado.Valor)))
            {
                validacao.Mensagens.Add("valor", "O campo valor não pode ser nulo ou vazio");
            }

            if (validacao.Valido)
            {
                this.banco.Lanches.Add(lancheCadastrado);
                this.banco.SaveChanges();
            }
            return validacao;
        }

        public Validacao CadastraPedido(Pedido pedidoCadastrado)
        {
            Validacao validacao = new Validacao();
            if(this.banco.Pedidos.Where(x => x.Id == pedidoCadastrado.Id).Any())
            {
                validacao.Mensagens.Add("id", "Já existem um pedido com esse código");
            }

            if (string.IsNullOrEmpty(Convert.ToString(pedidoCadastrado.DataCompra)))
            {
                validacao.Mensagens.Add("datacompra", "O campo data não pode ser nulo ou vazío");
            }

            if (!(this.banco.Clientes.Where(x => x.Id == pedidoCadastrado.Cliente.Id).Any()))
            {
                validacao.Mensagens.Add("cliente", "Não existe nenhum cliente cadastrado com esse código idenfiticador");
            }

            foreach (Lanche lanche in pedidoCadastrado.Lanches)
            {
                if (!(this.banco.Lanches.Where(x => x.Id == lanche.Id).Any()))
                {
                    validacao.Mensagens.Add("lanche", "$Não existe nenhum lanche cadastrado em um dos códigos informados");
                }
            }

            foreach (Bebida bebida in pedidoCadastrado.Bebidas)
            {
                if (!(this.banco.Bebidas.Where(x => x.Id == bebida.Id).Any()))
                {
                    validacao.Mensagens.Add("bebida", "$Não existe nenhuma bebida cadastrada em um dos códigos informados");
                }
            }

            this.banco.Pedidos.Add(pedidoCadastrado);
            this.banco.SaveChanges();

            return validacao;
        }
        
        public Cliente BuscaClientePorId(long id)
        {
            return this.banco.Clientes.Where(c => c.Id == id).FirstOrDefault();
        }

        public Pedido BuscaPedidoPorId(long id)
        {
            return this.banco.Pedidos.Where(c => c.Id == id).FirstOrDefault();
        }

        public Bebida BuscaBebidaPorId(long id)
        {
            return this.banco.Bebidas.Where(c => c.Id == id).FirstOrDefault();
        }

        public Lanche BuscaLanchePorId(long id)
        {
            return this.banco.Lanches.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Cliente> TodosOsClientes()
        {
            return this.banco.Clientes.ToList();
        }

        public List<Bebida> TodasAsBebidas()
        {
            return this.banco.Bebidas.ToList();
        }

        public List<Lanche> TodosOsLanches()
        {
            return this.banco.Lanches.ToList();
        }

        public List<Pedido> TodosOsPedidos()
        {
            return this.banco.Pedidos.ToList();
        }
    }
}
