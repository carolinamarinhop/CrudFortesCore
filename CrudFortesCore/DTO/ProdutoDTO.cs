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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [RegularExpression(@"^[\d,.?!]+$")]
        [Display(Name ="Valor")]
        [DataType(DataType.Currency)]
        public string ValorAux { get; set; }
    }
}
