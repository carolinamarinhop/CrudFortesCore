using CrudFortesCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudFortesCore.DTO
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; }

        public int IdProduto { get; set; }

        public virtual Produto Produto { get; set; }

        [Display(Name = "Quantidade")]
        public int QtdPedido { get; set; }

        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
