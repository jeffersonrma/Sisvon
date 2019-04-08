using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class GrupoRepository: RepositoryBase<Grupo>, IGrupoRepository
    {
        public GrupoRepository(SisvonContext db) 
            : base(db)
        {
        }

        public ICollection<Grupo> BuscarPorSubClasse(SubClasse obj)
        {
            return Db.Grupos
                .Where(g => g.SubClasse.SubClasseId == obj.SubClasseId).ToList();
        }
        
        public Grupo BuscaGrupoPelaOrdem(int ordem)
        {
            return Db.Grupos.FirstOrDefault(grupo => grupo.Ordem == ordem);
        }
    }
}
