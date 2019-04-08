using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(SisvonContext db) : base(db)
        {
        }

        public Funcionario Logar(string login)
        {
            return Db.Funcionarios.FirstOrDefault(x => x.Login == login);
        }

        public bool LoginEmUso(string login)
        {
            return Db.Funcionarios.FirstOrDefault(x => x.Login == login) != null;

        }
    }
}
