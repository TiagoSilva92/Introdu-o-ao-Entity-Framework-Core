using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //var db = new Data.ApplicationContext();

            //db.Database.Migrate();

            //var existe = db.Database.GetPendingMigrations().Any();

            //if (existe)
            //{
            //    Validando migrações pendentes
            //}

            //InserirDados();

            //InserirDadosEmMassa();

            //ConsultarDados();

            //CadastrarPedido

            //ConsultarPedidoCarregamentoAdiantado();

            //AtualizarDados();

            Removerregistros();
        }

        private static void Removerregistros()
        {
            var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);

            var cliente = new Cliente
            {
                Id = 3
            };

            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }
        private static void AtualizarDados()
        {
            var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado 3",
                Telefone = "0101010101"
            };

            db.Attach(cliente);
            
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            
            //db.Clientes.Update(cliente);
            
            db.SaveChanges();
        }
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            var db = new Data.ApplicationContext();

            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                 {
                     new PedidoItem
                     {
                         ProdutoId = produto.Id,
                         Desconto = 0,
                         Quantidade = 1,
                         Valor = 10,
                     }
                 }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }
        private static void ConsultarDados()
        {
            var db = new Data.ApplicationContext();

            //var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();

            //var consultaPorMetodo = db.Clientes
            //    .Where(p => p.Id > 0).ToList()
            //    .OrderBy(p=>p.Id);

            var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                db.Clientes.Find(cliente.Id);
            }
        }
        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                CodigoBarras = "1234567891231",
                Descricao = "Produto Teste",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Tiago Silva",
                Telefone = "9900001111",
                CEP = "99999000",
                Estado = "SP",
                Cidade = "Bragança Paulista",
            };

            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "Silva",
                    Telefone = "9910001111",
                    CEP = "19999000",
                    Estado = "SP",
                    Cidade = "Bragança",
                },
                new Cliente
                {
                    Nome = "Tiago",
                    Telefone = "9920001111",
                    CEP = "19999000",
                    Estado = "RJ",
                    Cidade = "Paulista",
                },
            };

            var db = new Data.ApplicationContext();

            db.Set<Cliente>().AddRange(listaClientes);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): {registros}");
        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var db = new Data.ApplicationContext();

            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}