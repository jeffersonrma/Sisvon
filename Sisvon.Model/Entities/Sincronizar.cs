
namespace Sisvon.Model.Entities
{
    public class Sincronizar
    {
        public int SincronizarId { get; set; }
        public int Ordem { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string GrupoNome { get; set; }
        public int GrupoOrdem { get; set; }
        public string SubClasseNome { get; set; }
        public int SubClasseOrdem { get; set; }
        public string SessionId { get; set; }
    }
}
