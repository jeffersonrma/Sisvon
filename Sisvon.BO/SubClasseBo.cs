using Sisvon.BO.Interfaces;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class SubClasseBo: BoBase<SubClasse>, ISubClasseBo
    {
        private readonly ISubClasseRepository _subClasseRepository;
        public SubClasseBo(SisvonContext context) 
            : base(context)
        {
            _subClasseRepository = new SubClasseRepository(context);
        }


        public SubClasse BuscaSubClassePelaOrdem(int ordem)
        {
           return _subClasseRepository.BuscaSubClassePelaOrdem(ordem);
        }
    }
}
