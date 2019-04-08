
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface ISincronizarRepository : IRepositoryBase<Sincronizar>
    {
        IEnumerable<Sincronizar> BuscarPorSessionId(string sessionId);
        void ApagarPorSession (string sessionId);
    }
}
