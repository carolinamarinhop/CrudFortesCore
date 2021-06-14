using CrudFortesCore.DTO;
using CrudFortesCore.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class FornecedorHandler : IRequestHandler<ListFornecedorCommand, List<FornecedorDTO>>,
                                     IRequestHandler<CreateFornecedorCommand>,
                                     IRequestHandler<DeleteFornecedorCommand>,
                                     IRequestHandler<EditFornecedorCommand>,
                                     IRequestHandler<ConsultFornecedorCommand, FornecedorDTO>
    {
        private readonly IRepository<Models.Fornecedor> _fornecedorRepository;

        public FornecedorHandler(IRepository<Models.Fornecedor> fornecedorRepository) => _fornecedorRepository = fornecedorRepository;

        public Task<Unit> Handle(DeleteFornecedorCommand request, CancellationToken cancellationToken)
        {
            _fornecedorRepository.Delete(_fornecedorRepository.Find(request.FornecedorId));
            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(EditFornecedorCommand request, CancellationToken cancellationToken)
        {
            var fornecedor = _fornecedorRepository.Find(request.FornecedorDTO.IdFornecedor);

            fornecedor.RazaoSocial = request.FornecedorDTO.RazaoSocial;
            fornecedor.Cnpj = request.FornecedorDTO.Cnpj;
            fornecedor.EmailContato = request.FornecedorDTO.EmailContato;
            fornecedor.NomeContato = request.FornecedorDTO.NomeContato;
            fornecedor.Uf = request.FornecedorDTO.Uf;
            fornecedor.DataAlteracao = DateTime.Now;

            _fornecedorRepository.Edit(fornecedor);

            return Task.FromResult(Unit.Value);
        }

        public Task<FornecedorDTO> Handle(ConsultFornecedorCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fornecedorRepository.Query().Where(f => f.IdFornecedor == request.FornecedorId).Select(fornecedor => new FornecedorDTO
            {
                IdFornecedor = fornecedor.IdFornecedor,
                RazaoSocial = fornecedor.RazaoSocial,
                Cnpj = fornecedor.Cnpj,
                Uf = fornecedor.Uf,
                EmailContato = fornecedor.EmailContato,
                NomeContato = fornecedor.NomeContato,
                DataAlteracao = fornecedor.DataAlteracao
            }).FirstOrDefault());
        }

        public Task<List<FornecedorDTO>> Handle(ListFornecedorCommand request, CancellationToken cancellationToken)
        {
            var fornecedores = _fornecedorRepository.Query().Select(fornecedor => new FornecedorDTO
            {
                IdFornecedor = fornecedor.IdFornecedor,
                RazaoSocial = fornecedor.RazaoSocial,
                Cnpj = fornecedor.Cnpj,
                Uf = fornecedor.Uf,
                EmailContato = fornecedor.EmailContato,
                NomeContato = fornecedor.NomeContato
            }).ToList();
            return Task.FromResult(fornecedores);
        }

        public Task<Unit> Handle(CreateFornecedorCommand request, CancellationToken cancellationToken)
        {
            var formatCNPJ = Regex.Replace(request.FornecedorDTO.Cnpj, "[^0-9]", "");

            _fornecedorRepository.Create(new Models.Fornecedor(request.FornecedorDTO.RazaoSocial,
                  request.FornecedorDTO.Cnpj = formatCNPJ,
                  request.FornecedorDTO.Uf,
                  request.FornecedorDTO.EmailContato,
                  request.FornecedorDTO.NomeContato));

            return Task.FromResult(Unit.Value);
        }
    }
}
