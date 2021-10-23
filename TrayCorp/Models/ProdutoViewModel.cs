using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TrayCorp.Models
{
    public class ProdutoViewModel
    {

        [Display(Name = "ID do produto")]
        public int PROD_ID { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório!")]
        [Display(Name ="Nome do produto")]
        public String PROD_NOME { get; set; }

        [Display(Name = "Quantidade em estoque")]
        public int? PROD_ESTOQUE { get; set; }

        [Display(Name = "Preço")]
        public Decimal? PROD_VALOR { get; set; }

    }
}