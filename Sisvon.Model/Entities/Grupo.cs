namespace Sisvon.Model.Entities
{
    public class Grupo
    {
        public int GrupoId { get; set; }
        public int Ordem { get; set; }
        public string Nome { get; set; }
        public virtual SubClasse SubClasse { get; set; }

    }
}
