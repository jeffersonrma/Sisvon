using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class SubClasseRepository:RepositoryBase<SubClasse>,ISubClasseRepository
    {
        public SubClasseRepository(SisvonContext db) 
            : base(db)
        {
        }

        public bool PossuiGrupo(SubClasse obj)
        {
            return new GrupoRepository(Db).BuscarPorSubClasse(obj).Count != 0;
        }
        public SubClasse BuscaSubClassePelaOrdem(int ordem)
        {
            return Db.SubClasses.FirstOrDefault(subClasse => subClasse.Ordem == ordem);
        }
    }
}
