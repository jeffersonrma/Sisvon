
using System.ComponentModel;

namespace Sisvon.Model.Entities.Enum
{
    public enum Status
    {
        [Description("Aguardando Pagamento")]
        AguardandoPagamento = 1,
        [Description("Pagamento Confirmado")]
        PagamentoConfirmado = 2,
        [Description("Produto Postado")]
        ProdutoPostado = 3,
        [Description("Cancelado")]
        Cancelado = 4
    }
}
