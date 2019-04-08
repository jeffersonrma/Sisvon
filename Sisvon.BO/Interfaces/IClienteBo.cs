
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IClienteBo : IBoBase<Cliente>
    {

        Cliente Logar(string txtLogin, string txtSenha);
        bool RgEmUso(string rg);
        bool CpfEmUso(string cpf);
        bool EmailEmUso(string email);
        bool TrocarSenha(Cliente cliente, string txtSenha, string txtSenhaNova);
    }
}
