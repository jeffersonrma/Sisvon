using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class SincronizarRepository : RepositoryBase<Sincronizar>, ISincronizarRepository
    {
        public SincronizarRepository(SisvonContext db)
            : base(db)
        {
        }

        public IEnumerable<Sincronizar> BuscarPorSessionId(string sessionId)
        {
            return Db.Sincronizar.Where(s => s.SessionId == sessionId);
        }

        public void ApagarPorSession(string sessionId)
        {
            Db.Sincronizar.RemoveRange(Db.Sincronizar.Where(s => s.SessionId == sessionId));
        }
    }
}
