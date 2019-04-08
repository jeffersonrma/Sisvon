
using System.Collections.Generic;

namespace Sisvon.Model.Entities
{
    public class SubClasse
    {
        public int SubClasseId { get; set; }
        public int Ordem { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }

    }
}
