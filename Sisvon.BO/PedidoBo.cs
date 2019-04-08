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
    public class PedidoBo : BoBase<Pedido>, IPedidoBo
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoBo(SisvonContext context)
            : base(context)
        {
            _pedidoRepository = new PedidoRepository(context);
        }

        public override void Add(Pedido pedido)
        {
            if (pedido.Cliente == null) throw new BoException("Cliente inválido");
            if (pedido.ItemsPedido == null) throw new BoException("Items inválidos");
            if (pedido.ValorTotal == 0) throw new BoException("Valor inválido");
            if (pedido.Status == 0) throw new BoException("Status inválido");
            if (pedido.FormaPagamento == 0) throw new BoException("Forma de pagamento inválida");

            if(!new ProdutoBo(_Context).SincronizarPorPedido(pedido))
                throw new BoException("Falha na comunicação com o servidor. Tente novamente mais tarde");

            var produtobo = new ProdutoBo(_Context);


            if (pedido.ItemsPedido
                .Any(itemPedido =>
                    produtobo.GetById(itemPedido.Produto.ProdutoId).EstoqueDisponivel < itemPedido.Qtde))
                throw new BoException("Estoque insuficiente");

            produtobo.ReservarPorPedido(pedido);
            base.Add(pedido);
        }

        public override void Remove(Pedido obj)
        {
            if (obj.PedidoId == 0) throw new BoException("Id inválida");
            if (_pedidoRepository.PossuiItem(obj))
                throw new BoException("O Pedido não pode ser removido enquanto possuir Itens");
            base.Remove(obj);
        }

        public override void Update(Pedido obj)
        {
            if (obj.Cliente == null) throw new BoException("Cliente inválido");
            if (obj.ItemsPedido == null) throw new BoException("Items inválidos");
            if (obj.ValorTotal == 0) throw new BoException("Valor inválido");
            if (obj.Status == 0) throw new BoException("Status inválido");
            if (obj.FormaPagamento == 0) throw new BoException("Forma de pagamento inválida");
            if (obj.PedidoId == 0) throw new BoException("Id inválida");
            if (!obj.Aprovado) throw new BoException("Produto deve ser aprovado");

            base.Update(obj);
        }

        public ICollection<Pedido> BuscarPorCliente(Cliente cliente)
        {
            return _pedidoRepository.GetByCliente(cliente);
        }

        public Pedido GetOneByCliente(Cliente cliente, int pedidoId)
        {
            Pedido pedido = _pedidoRepository.GetById(pedidoId);
            if (pedido.Cliente.ClienteId != cliente.ClienteId) throw new BoException("Esse pedido não pertece a seu usuario");
            return pedido;
        }

        public IQueryable<Pedido> GetNaoAprovado()
        {
            return _pedidoRepository.GetNaoAprovado();
        }

        public IQueryable<Pedido> GetAprovado()
        {
            return _pedidoRepository.GetAprovado();

        }


        public void Aprovar(Pedido pedido)
        {
            if (pedido.Aprovado) throw new BoException("Pedido já está aprovado");
            new ProdutoBo(_Context).LiberarReservaPorPedido(pedido);
            pedido.Aprovado = true;
            Update(pedido);
        }

        public Pedido GetPedidoPendente(int id)
        {
            var pedido = GetById(id);
            if (pedido.Aprovado) throw new BoException("Pedido já foi aprovado");
            return pedido;
        }

        public ICollection<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return _pedidoRepository.BuscarPorData(dataInicial, dataFinal);
        }
    }
}
