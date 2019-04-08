
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class ClienteBo : BoBase<Cliente>, IClienteBo
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteBo(SisvonContext context)
            : base(context)
        {
            _clienteRepository = new ClienteRepository(context);
        }

        public override void Add(Cliente obj)
        {
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Cidade)) throw new BoException("campo cidade inválido");
            if (string.IsNullOrEmpty(obj.Cpf) || obj.Cpf.Length != 14) throw new BoException("campo CPF inválido");
            if (string.IsNullOrEmpty(obj.Email)) throw new BoException("campo E-mail inválido");
            if (string.IsNullOrEmpty(obj.Numero)) throw new BoException("campo número inválido");
            if (string.IsNullOrEmpty(obj.Rg)) throw new BoException("campo RG inválido");
            if (string.IsNullOrEmpty(obj.Rua)) throw new BoException("campo rua inválido");
            if (string.IsNullOrEmpty(obj.Senha)) throw new BoException("campo senha inválido");
            if (string.IsNullOrEmpty(obj.Telefone)) throw new BoException("campo telefone inválido");
            if (string.IsNullOrEmpty(obj.Cep)) throw new BoException("campo CEP inválido");
            if (string.IsNullOrEmpty(obj.Uf)) throw new BoException("campo Uf inválido");
            if (string.IsNullOrEmpty(obj.Bairro)) throw new BoException("campo Bairro inválido");
            if (obj.Senha.Length < 6 || obj.Senha.Length > 12) throw new BoException("A senha deve ter entre 6 e 12 caracteres");
            obj.Senha = BCrypt.Net.BCrypt.HashPassword(obj.Senha);

            if (EmailEmUso(obj.Email)) throw new BoException("Este e-mail já Esta em uso");
            if (CpfEmUso(obj.Cpf)) throw new BoException("Este CPF já Esta em uso");
            if (RgEmUso(obj.Rg)) throw new BoException("Este RG já Esta em uso");

            base.Add(obj);
        }
        public override void Remove(Cliente obj)
        {
            if (obj.ClienteId == 0) throw new BoException("campo id inválido");
            if (_clienteRepository.PossuiPedido(obj))
                throw new BoException("O cliente não pode ser removido enquanto possuir pedidos");
            base.Remove(obj);
        }

        public override void Update(Cliente obj)
        {
            if (obj.ClienteId == 0) throw new BoException("campo id inválido");
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Cidade)) throw new BoException("campo cidade inválido");
            if (string.IsNullOrEmpty(obj.Cpf) || obj.Cpf.Length != 14) throw new BoException("campo CPF inválido");
            if (string.IsNullOrEmpty(obj.Email)) throw new BoException("campo E-mail inválido");
            if (string.IsNullOrEmpty(obj.Numero)) throw new BoException("campo numero inválido");
            if (string.IsNullOrEmpty(obj.Rg)) throw new BoException("campo RG inválido");
            if (string.IsNullOrEmpty(obj.Rua)) throw new BoException("campo rua inválido");
            if (string.IsNullOrEmpty(obj.Senha)) throw new BoException("campo senha inválido");
            if (string.IsNullOrEmpty(obj.Telefone)) throw new BoException("campo telefone inválido");
            if (string.IsNullOrEmpty(obj.Cep)) throw new BoException("campo CEP inválido");
            if (string.IsNullOrEmpty(obj.Uf)) throw new BoException("campo Uf inválido");
            if (string.IsNullOrEmpty(obj.Bairro)) throw new BoException("campo Bairro inválido");

            if (EmailEmUso(obj.Email)) throw new BoException("Este e-mail já Esta em uso");
            if (CpfEmUso(obj.Cpf)) throw new BoException("Este CPF já Esta em uso");
            if (RgEmUso(obj.Rg)) throw new BoException("Este RG já Esta em uso");
            base.Update(obj);
        }

        public bool RgEmUso(string rg)
        {
            var cliente = _clienteRepository.BuscarPorRg(rg);
            return cliente != null;
        }

        public bool CpfEmUso(string cpf)
        {
            var cliente = _clienteRepository.BuscarPorCpf(cpf);
            return cliente != null;
        }

        public bool EmailEmUso(string email)
        {
            var cliente = _clienteRepository.BuscarPorEmail(email);
            return cliente != null;
        }

        public Cliente Logar(string txtLogin, string txtSenha)
        {
            if (string.IsNullOrEmpty(txtLogin) || string.IsNullOrEmpty(txtSenha)) throw new BoException("login ou senha inválido");
            Cliente cliente = _clienteRepository.Logar(txtLogin);
            if (cliente == null) throw new BoException("login inválido");
            if (!BCrypt.Net.BCrypt.Verify(txtSenha, cliente.Senha)) throw new BoException("Senha inválida");
            return cliente;
        }

        public bool TrocarSenha(Cliente cliente, string txtSenha, string txtSenhaNova)
        {
            if (txtSenhaNova.Length < 6 || txtSenhaNova.Length > 12) throw new BoException("A senha deve ter entre 6 e 12 caracteres");
            if (!BCrypt.Net.BCrypt.Verify(txtSenha, cliente.Senha)) return false;

            txtSenhaNova = BCrypt.Net.BCrypt.HashPassword(txtSenhaNova);
            cliente.Senha = txtSenhaNova;
            Update(cliente);
            return true;
        }
    }
}
