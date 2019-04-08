

using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.ServiceAgents
{
    public interface ISincronizarAgent
    {
        Sincronizar SincronizarProdutoPorOrdem(string cod);
        ICollection<Sincronizar> SincronizarProdutos();
    }
}
