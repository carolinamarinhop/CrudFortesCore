using System;
using System.ComponentModel.DataAnnotations;

namespace CrudFortesCore.DTO
{
    public class FornecedorDTO
    {
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
       
        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
