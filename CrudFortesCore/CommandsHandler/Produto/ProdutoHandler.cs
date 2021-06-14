using CrudFortesCore.DTO;
using CrudFortesCore.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class ProdutoHandler : IRequestHandler<ListProdutoCommand, List<ProdutoDTO>>,
                                  IRequestHandler<CreateProdutoCommand>,
                                  IRequestHandler<ConsultProdutoCommand, ProdutoDTO>,
                                  IRequestHandler<DeleteProdutoCommand>,
                                  IRequestHandler<EditProdutoCommand>


    {
        private readonly IRepository<Models.Produto> _produtoRepository;
        public ProdutoHandler(IRepository<Models.Produto> produtoRepository) => _produtoRepository = produtoRepository;

        public Task<List<ProdutoDTO>> Handle(ListProdutoCommand request, CancellationToken cancellationToken)
        {
            var produtos = _produtoRepository.Query().Select(produto => new ProdutoDTO
            {
                IdProduto = produto.IdProduto,
                Descricao = produto.Descricao,
                Valor = produto.Valor,
                DataCadastro = produto.DataCadastro
            }).ToList();
            return Task.FromResult(produtos);
        }

        public Task<Unit> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            var valorAux = Convert.ToDecimal(request.ProdutoDTO.ValorAux, new CultureInfo("pt-BR"));

            _produtoRepository.Create(new Models.Produto(
                request.ProdutoDTO.Descricao,
                request.ProdutoDTO.Valor = valorAux,
                request.ProdutoDTO.DataCadastro = DateTime.Now));

            return Task.FromResult(Unit.Value);
        }

        public Task<ProdutoDTO> Handle(ConsultProdutoCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_produtoRepository.Query().Where(p => p.IdProduto == request.ProdutoId).Select(produto => new ProdutoDTO
            {
                IdProduto = produto.IdProduto,
                Descricao = produto.Descricao,
                ValorAux = produto.Valor.ToString("N2"),
                DataCadastro = produto.DataCadastro,
                DataAlteracao = produto.DataAlteracao
            }).FirstOrDefault());
        }

        public Task<Unit> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            _produtoRepository.Delete(_produtoRepository.Find(request.ProdutoId));
            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(EditProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _produtoRepository.Find(request.ProdutoDTO.IdProduto);
            var valorAux = Convert.ToDecimal(request.ProdutoDTO.ValorAux, new CultureInfo("pt-BR"));

            produto.Descricao = request.ProdutoDTO.Descricao;
            produto.Valor = valorAux;
            produto.DataAlteracao = DateTime.Now;
            _produtoRepository.Edit(produto);

            return Task.FromResult(Unit.Value);
        }
    }
}
