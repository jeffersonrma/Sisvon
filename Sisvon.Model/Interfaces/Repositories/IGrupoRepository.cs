
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IGrupoRepository:IRepositoryBase<Grupo>
    {
        ICollection<Grupo> BuscarPorSubClasse(SubClasse obj);
        Grupo BuscaGrupoPelaOrdem(int ordem);

    }
}
