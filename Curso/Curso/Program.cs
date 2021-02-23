using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Data.ApplicationContext();

            //db.Database.Migrate();

            var existe = db.Database.GetPendingMigrations().Any();

            if (existe)
            {
                //Validando migrações pendentes
            }

            Console.WriteLine("Hello World!");
        }
    }
}