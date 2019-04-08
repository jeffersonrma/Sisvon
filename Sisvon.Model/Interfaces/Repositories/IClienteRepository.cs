
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        bool PossuiPedido(Cliente obj);
        Cliente Logar(string txtLogin);


        Cliente BuscarPorEmail(string email);

        Cliente BuscarPorCpf(string cpf);

        Cliente BuscarPorRg(string re);
    }
}
