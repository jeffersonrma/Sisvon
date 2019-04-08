
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IRepositoryBase<Funcionario>
    {
        Funcionario Logar(string login);

        bool LoginEmUso(string p);
    }
}
