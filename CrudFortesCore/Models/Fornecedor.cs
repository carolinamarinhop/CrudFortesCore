using System;
using System.ComponentModel.DataAnnotations;

namespace CrudFortesCore.Models
{
    public class Fornecedor
    {
        public Fornecedor(string razaoSocial, string cnpj,string uf, string emailContato, string nomeContato)
        {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Uf = uf;
            EmailContato = emailContato;
            NomeContato = nomeContato;
        }

        [Key]
        public int IdFornecedor { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Display(Name = "Email Contato")]
        public string EmailContato { get; set; }

        [Display(Name = "Nome Contato")]
        public string NomeContato { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
