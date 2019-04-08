
using Sisvon.BO.Interfaces;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class GrupoBo:BoBase<Grupo>, IGrupoBo
    {
        private readonly IGrupoRepository _grupoRepository;
        public GrupoBo(SisvonContext context) 
            : base(context)
        {
            _grupoRepository = new GrupoRepository(context);
        }
        
        public Grupo BuscaGrupoPelaOrdem(int ordem)
        {
            return _grupoRepository.BuscaGrupoPelaOrdem(ordem);
        }
    }
}
