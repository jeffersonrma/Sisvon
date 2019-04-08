
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface ISubClasseRepository: IRepositoryBase<SubClasse>
    {
        bool PossuiGrupo(SubClasse obj);
        SubClasse BuscaSubClassePelaOrdem(int ordem);


    }
}
