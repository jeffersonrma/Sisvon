using System.Collections.Generic;
using Newtonsoft.Json;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.ServiceAgents;

namespace Sisvon.Infra.ServiceAgent
{
    public class SincronizarAgent : ISincronizarAgent
    {
        public Sincronizar SincronizarProdutoPorOrdem(string ordem)
        {
            var json = new HttpConnect().HttpRequest("getprodutosinc/" + ordem);
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<Sincronizar>(json);
        }

        public ICollection<Sincronizar> SincronizarProdutos()
        {
            var json = new HttpConnect().HttpRequest("getallproduto");
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<ICollection<Sincronizar>>(json);
        }
    }
}
