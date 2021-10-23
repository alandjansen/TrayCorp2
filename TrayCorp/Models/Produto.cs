using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrayCorp.Repositorio;

namespace TrayCorp.Models
{

    [Table("PRODUTO")]
    public class Produto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PROD_ID { get; set; }

        [Required(ErrorMessage ="O nome do produto é obrigatório!")]
        public String PROD_NOME { get; set; }

        public int? PROD_ESTOQUE { get; set; }

        public Decimal? PROD_VALOR { get; set; }


    }
}