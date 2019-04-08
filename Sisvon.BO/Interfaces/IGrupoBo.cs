
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IGrupoBo:IBoBase<Grupo>
    {
        Grupo BuscaGrupoPelaOrdem(int ordem);

    }
}
