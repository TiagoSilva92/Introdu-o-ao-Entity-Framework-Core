using System;
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

            ConsultarDados();
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
                TipoProduto = TipoProduto.MercadoriaPararevenda,
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
                TipoProduto = TipoProduto.MercadoriaPararevenda,
                Ativo = true
            };

            var db = new Data.ApplicationContext();

            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}