
using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Infra.ServiceAgent;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Model.Interfaces.ServiceAgents;

namespace Sisvon.BO
{
    public class SincronizarBo : BoBase<Sincronizar>, ISincronizarBo
    {
        private readonly ISincronizarRepository _sincronizarRepository;
        private readonly ISincronizarAgent _sincronizarAgent;
        public SincronizarBo(SisvonContext context)
            : base(context)
        {
            _sincronizarRepository = new SincronizarRepository(context);
            _sincronizarAgent = new SincronizarAgent();
        }

        public void ImportarPorSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) throw new BoException("SessionID invalida");
            ICollection<Sincronizar> listaSincronizar = null;
            try
            {
                listaSincronizar = _sincronizarAgent.SincronizarProdutos();
            }
            catch (Exception)
            {

            }


            if (listaSincronizar == null) throw new BoException("Erro de comunicação com o servidor");
            if (listaSincronizar.Count == 0) throw new BoException("Nem um produto emcontrado");

            ApagarPorSession(sessionId);

            var ordensCadastradas = new ProdutoBo(_Context).ListarOrdens();

            foreach (var sincronizar in listaSincronizar
                .Where(sincronizar => !ordensCadastradas.Contains(sincronizar.Ordem.ToString())))
            {
                sincronizar.SessionId = sessionId;
                _sincronizarRepository.Add(sincronizar);
            }

        }

        public IEnumerable<Sincronizar> BuscarPorSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) throw new BoException("SessionID invalida");

            return _sincronizarRepository.BuscarPorSessionId(sessionId);
        }

        public IEnumerable<Sincronizar> ImportarProdutosAtualizados()
        {
            ICollection<Sincronizar> listaSincronizar = null;
            try
            {
                listaSincronizar = _sincronizarAgent.SincronizarProdutos();
            }
            catch (Exception)
            {
            }
            
            if (listaSincronizar == null) throw new BoException("Erro de comunicação com o servidor");
            if (listaSincronizar.Count == 0) throw new BoException("Nem um produto emcontrado");

            var ordensCadastradas = new ProdutoBo(_Context).ListarOrdens();

            return listaSincronizar
                .Where(sincronizar => ordensCadastradas.Contains(sincronizar.Ordem.ToString()));

        }

        public void ApagarPorSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) throw new BoException("SessionID invalida");

            _sincronizarRepository.ApagarPorSession(sessionId);
        }

        public Sincronizar ImportarPorOrdem(int ordem)
        {
            return _sincronizarAgent.SincronizarProdutoPorOrdem(ordem.ToString());
        }
    }
}
