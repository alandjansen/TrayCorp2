using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TrayCorp.Repositorio;

namespace TrayCorp.Models
{
    public class ProdutoDBContext : DbContext
    {

        public DbSet<PRODUTO> PRODUTO { get; set; }

        public ProdutoDBContext()
        {

            Database.SetInitializer<ProdutoDBContext>(null);

        }

    }
}