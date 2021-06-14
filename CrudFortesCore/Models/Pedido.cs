using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudFortesCore.Models
{
    public class Pedido
    {
        public Pedido(int idFornecedor, int idProduto, int qtdPedido, decimal valorTotal, DateTime dataPedido)
        {
            IdFornecedor = idFornecedor;
            IdProduto = idProduto;
            QtdPedido = qtdPedido;
            ValorTotal = valorTotal;
            DataPedido = dataPedido;
        }

        [Key]
        public int IdPedido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }

        public virtual Produto Produto { get; set; }

        [Display(Name = "Quantidade Pedido")]
        public int QtdPedido { get; set; }

        [ForeignKey("Fornecedor")]
        public int IdFornecedor { get; set; }

        public Fornecedor Fornecedor { get; set; }

        //[DisplayFormat(DataFormatString = "{0:C}")]
        //[DataType(DataType.Currency)]
        //[Column(TypeName = "money")]
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
