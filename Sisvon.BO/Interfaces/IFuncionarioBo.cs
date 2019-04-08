
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IFuncionarioBo : IBoBase<Funcionario>
    {
        bool TrocarSenha(Funcionario funcionario, string senha, string senhaNova);
        Funcionario Logar(string login, string senha);
        void PossuiFuncionario();
    }
}
