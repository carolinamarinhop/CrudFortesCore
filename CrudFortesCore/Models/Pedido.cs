using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudFortesCore.Models
{
    public class Pedido
    {
        public Pedido(int idFornecedor, int idProduto, int qtdPedido, decimal valorTotal)
        {
            IdFornecedor = idFornecedor;
            IdProduto = idProduto;
            QtdPedido = qtdPedido;
            ValorTotal = valorTotal;
            DataPedido = DateTime.Now;
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

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
