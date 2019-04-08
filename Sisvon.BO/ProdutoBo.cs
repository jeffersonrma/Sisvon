
using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class ProdutoBo : BoBase<Produto>, IProdutoBo
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoBo(SisvonContext context)
            : base(context)
        {
            _produtoRepository = new ProdutoRepository(context);
        }

        public override void Add(Produto obj)
        {
            if (obj.SubClasse == null) throw new BoException("SubClasse inválido");
            if (obj.Grupo == null) throw new BoException("Grupo inválido");
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Codigo)) throw new BoException("campo codigo inválido");
            if (obj.Preco == 0) throw new BoException("campo preço inválido");

            base.Add(obj);
        }

        public override void Remove(Produto obj)
        {
            if (obj.ProdutoId == 0) throw new BoException("Id inválido");
            if (_produtoRepository.PossuiItem(obj))
                throw new BoException("falha ao remover, existem pedidos com esse produto");
            base.Remove(obj);
        }

        public override void Update(Produto obj)
        {
            if (obj.SubClasse == null) throw new BoException("SubClasse inválido");
            if (obj.Grupo == null) throw new BoException("Grupo inválido");
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Codigo)) throw new BoException("campo codigo inválido");
            if (obj.Preco == 0) throw new BoException("campo preço inválido");
            if (obj.ProdutoId == 0) throw new BoException("Id inválido");
            if (obj.ValorPac == 0) throw new BoException("Valores do frete não podem ser 0");
            if (obj.ValorSedex == 0) throw new BoException("Valores do frete não podem ser 0");
            base.Update(obj);
        }

        public IEnumerable<string> ListarOrdens()
        {
            return _produtoRepository.ListarOrdens();
        }

        public void GravarProdutosImportados(ICollection<Sincronizar> listaSincronizar)
        {
            if (listaSincronizar == null || listaSincronizar.Count == 0)
                throw new BoException("Produto importado é invalido");

            foreach (var sincronizar in listaSincronizar)
            {
                SincronizarProdutoImportado(sincronizar);
            }
        }

        public IQueryable<Produto> BuscarPorNome(string term)
        {
            return GetAll().Where(x => (x.Nome.Contains(term) || x.Codigo.Contains(term)) && x.Ativo);
        }

        public Produto BuscarPorOrdem(int ordem)
        {
            return _produtoRepository.BuscarPorOrdem(ordem);
        }

        public IQueryable<Produto> GetBySubClasse(SubClasse subclasse)
        {
            return GetAll().Where(x => x.SubClasse.SubClasseId == subclasse.SubClasseId && x.Ativo);
        }

        public IQueryable<Produto> GetByGrupo(Grupo grupo)
        {
            return GetAll().Where(x => x.Grupo.GrupoId == grupo.GrupoId && x.Ativo);
        }

        public IEnumerable<Produto> GetVitrine()
        {
            return _produtoRepository.BuscarVitrine();
        }

        public IEnumerable<Produto> GetDestaque()
        {
            return _produtoRepository.BuscarDestaque();
        }

        public void LiberarReservaPorPedido(Pedido pedido)
        {
            foreach (var item in pedido.ItemsPedido)
            {
                item.Produto.Reserva = item.Produto.Reserva - item.Qtde;
                Update(item.Produto);
            }
        }

        public void ReservarPorPedido(Pedido pedido)
        {
            _produtoRepository.ReservarPorPedido(pedido);
        }

        public void AtualizarProdutosImportados()
        {
            var listaSincronizar = new SincronizarBo(_Context).ImportarProdutosAtualizados();

            foreach (var sincronizar in listaSincronizar)
            {
                SincronizarProdutoImportado(sincronizar);
            }
        }


        public void SincronizarProdutoImportado(Sincronizar sincronizar)
        {
            var subClasse = new SubClasseBo(_Context).BuscaSubClassePelaOrdem(sincronizar.SubClasseOrdem) ?? new SubClasse
            {
                Nome = sincronizar.SubClasseNome,
                Ordem = sincronizar.SubClasseOrdem,
            };

            var grupo = new GrupoBo(_Context).BuscaGrupoPelaOrdem(sincronizar.GrupoOrdem) ?? new Grupo
            {
                SubClasse = subClasse,
                Nome = sincronizar.GrupoNome,
                Ordem = sincronizar.GrupoOrdem
            };
            var produto = BuscarPorOrdem(sincronizar.Ordem);
            if (produto == null) Add(new Produto
            {
                Ordem = sincronizar.Ordem,
                SubClasse = subClasse,
                Nome = sincronizar.Nome,
                Grupo = grupo,
                Codigo = sincronizar.Codigo,
                Preco = sincronizar.Preco,
                Estoque = sincronizar.Estoque
            });
            else
            {
                produto.Codigo = sincronizar.Codigo;
                produto.Estoque = sincronizar.Estoque;
                produto.Preco = sincronizar.Preco;
                produto.SubClasse = subClasse;
                produto.Grupo = grupo;
                _produtoRepository.Update(produto);
            }
        }

        public bool SincronizarPorPedido(Pedido pedido)
        {
            if (pedido == null || pedido.ItemsPedido == null || pedido.ItemsPedido.Count == 0)
                throw new BoException("Pedido Inválido!");

            var sincBo = new SincronizarBo(_Context);
            bool succ = true;
            foreach (var item in pedido.ItemsPedido)
            {
                try
                {
                    SincronizarProdutoImportado(sincBo.ImportarPorOrdem(item.Produto.Ordem));
                }
                catch (Exception)
                {
                    succ = false;
                }
            }
            return succ;
        }
    }
}
