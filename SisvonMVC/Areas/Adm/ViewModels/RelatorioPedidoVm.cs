
using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Entities.Enum;

namespace SisvonMVC.Areas.Adm.ViewModels
{
    public class RelatorioPedidoVm
    {
        public RelatorioPedidoVm(ICollection<Pedido>listaPedidos)
        {
            ConfirmadoBoleto = listaPedidos
                .Count(p => (p.Status == Status.PagamentoConfirmado ||
                            p.Status == Status.ProdutoPostado) &&
                            p.FormaPagamento == FormaPagamento.Boleto);
            CanceladoBoleto = listaPedidos
                .Count(p => p.Status == Status.Cancelado &&
                            p.FormaPagamento == FormaPagamento.Boleto);
            ConfirmadoDeposito = listaPedidos
                .Count(p => (p.Status == Status.PagamentoConfirmado ||
                            p.Status == Status.ProdutoPostado) &&
                            p.FormaPagamento == FormaPagamento.Deposito);
            CanceladoDeposito = listaPedidos
                .Count(p => p.Status == Status.Cancelado &&
                            p.FormaPagamento == FormaPagamento.Deposito);
            ConfirmadoTotalDeposito = listaPedidos
                .Where(p => (p.Status == Status.PagamentoConfirmado ||
                            p.Status == Status.ProdutoPostado) &&
                            p.FormaPagamento == FormaPagamento.Deposito)
                .Sum(p => p.ValorTotal);
            CanceladoTotalDeposito = listaPedidos
                .Where(p => p.Status == Status.Cancelado &&
                            p.FormaPagamento == FormaPagamento.Deposito)
                .Sum(p => p.ValorTotal);
            ConfirmadoTotalBoleto = listaPedidos
                .Where(p => (p.Status == Status.PagamentoConfirmado ||
                            p.Status == Status.ProdutoPostado) &&
                            p.FormaPagamento == FormaPagamento.Boleto)
                .Sum(p => p.ValorTotal);
            CanceladoTotalBoleto = listaPedidos
                .Where(p => p.Status == Status.Cancelado &&
                            p.FormaPagamento == FormaPagamento.Boleto)
                .Sum(p => p.ValorTotal);
        }

        public DateTime DataInicial { get; set; }
        public DateTime Datafinal { get; set; }
        public int ConfirmadoBoleto { get; set; }
        public int ConfirmadoDeposito { get; set; }
        public decimal ConfirmadoTotalBoleto { get; set; }
        public decimal ConfirmadoTotalDeposito { get; set; }
        public int CanceladoBoleto { get; set; }
        public int CanceladoDeposito { get; set; }
        public decimal CanceladoTotalBoleto { get; set; }
        public decimal CanceladoTotalDeposito { get; set; }

        public int Confirmado
        {
            get { return ConfirmadoBoleto + ConfirmadoDeposito; }
        }
        public int Cancelado
        {
            get { return CanceladoDeposito + CanceladoBoleto; }
        }
        public decimal ConfirmadoTotal
        {
            get { return ConfirmadoTotalBoleto + ConfirmadoTotalDeposito; }
        }
        public decimal CanceladoTotal
        {
            get { return CanceladoTotalDeposito + CanceladoTotalBoleto; }
        }
        public decimal Total
        {
            get { return CanceladoTotal + ConfirmadoTotal; }
        }
        public decimal PorcentagemConfirmadoTotal
        {
            get { return decimal.Round(Total == 0 ? 0 : ConfirmadoTotal / Total * 100, 2); }
        }
        public decimal PorcentagemCanceladoTotal
        {
            get { return decimal.Round(Total == 0 ? 0 : CanceladoTotal / Total * 100, 2); }
        }
    }
}