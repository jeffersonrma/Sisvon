
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface ISubClasseBo:IBoBase<SubClasse>
    {
        SubClasse BuscaSubClassePelaOrdem(int ordem);


    }
}
