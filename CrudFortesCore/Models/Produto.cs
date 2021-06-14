using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudFortesCore.Models
{
    public class Produto
    {
        public Produto(string descricao, decimal valor)
        {
            Descricao = descricao;
            Valor = valor;
            DataCadastro = DateTime.Now;
        }

        [Key]
        public int IdProduto { get; set; }

        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
