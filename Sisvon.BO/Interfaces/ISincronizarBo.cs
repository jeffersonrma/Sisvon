
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface ISincronizarBo: IBoBase<Sincronizar>
    {
        void ImportarPorSession(string sessionId);
        IEnumerable<Sincronizar> BuscarPorSession(string sessionId);
        IEnumerable<Sincronizar> ImportarProdutosAtualizados();
        void ApagarPorSession(string sessionId);
        Sincronizar ImportarPorOrdem(int ordem);

    }
}
