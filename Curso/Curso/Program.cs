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

            InserirDAdos();
        }

        private static void InserirDAdos()
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