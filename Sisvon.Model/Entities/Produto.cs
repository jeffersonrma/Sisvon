
using System.ComponentModel.DataAnnotations;

namespace Sisvon.Model.Entities
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public decimal ValorPac { get; set; }
        public string Codigo { get; set; }
        public decimal ValorSedex { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        public bool Destaque { get; set; }
        public int Estoque { get; set; }
        public int Reserva { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Vitrine { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual SubClasse SubClasse { get; set; }

        public int EstoqueDisponivel
        {
            get { return Estoque - Reserva; }
        }

    }
}
