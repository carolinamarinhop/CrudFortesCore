using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudFortesCore.DTO
{
    public class ProdutoDTO
    {
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
