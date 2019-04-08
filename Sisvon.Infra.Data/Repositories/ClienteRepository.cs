using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(SisvonContext db) : base(db)
        {
        }

        public bool PossuiPedido(Cliente obj)
        {
            return new PedidoRepository(Db).GetByCliente(obj).Count != 0;
        }

        public Cliente Logar(string txtLogin)
        {
            return Db.Clientes.FirstOrDefault(x => x.Email == txtLogin);

        }

        public Cliente BuscarPorEmail(string email)
        {
            return Db.Clientes.FirstOrDefault(x => x.Email == email);
        }

        public Cliente BuscarPorCpf(string cpf)
        {
            return Db.Clientes.FirstOrDefault(x => x.Cpf == cpf);
        }

        public Cliente BuscarPorRg(string rg)
        {
            return Db.Clientes.FirstOrDefault(x => x.Rg == rg);
        }
    }
}
