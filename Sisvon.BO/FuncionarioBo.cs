using System.Linq;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;


namespace Sisvon.BO
{
    public class FuncionarioBo : BoBase<Funcionario>, IFuncionarioBo
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public FuncionarioBo(SisvonContext context)
            : base(context)
        {
            _funcionarioRepository = new FuncionarioRepository(context);
        }

        public override void Add(Funcionario obj)
        {
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Login)) throw new BoException("campo login inválido");
            if (string.IsNullOrEmpty(obj.Senha)) throw new BoException("campo senha inválido");
            if (obj.Senha.Length < 6 || obj.Senha.Length > 12) throw new BoException("A senha deve ter entre 6 e 12 caracteres");
            obj.Senha = BCrypt.Net.BCrypt.HashPassword(obj.Senha);

            if (_funcionarioRepository.LoginEmUso(obj.Login)) throw new BoException("Este Login já Esta em uso");

            base.Add(obj);
        }

        public override void Remove(Funcionario obj)
        {
            if (obj.FuncionarioId == 0) throw new BoException("campo Id inválido");
            if (UnicoAdm()) throw new BoException("Este usuário e o único administrador do sistema");
            base.Remove(obj);
        }

        private bool UnicoAdm()
        {
            return GetAll().Count(p => p.Administrador) == 1;
        }

        public override void Update(Funcionario obj)
        {
            if (string.IsNullOrEmpty(obj.Nome)) throw new BoException("campo nome inválido");
            if (string.IsNullOrEmpty(obj.Login)) throw new BoException("campo login inválido");
            if (obj.FuncionarioId == 0) throw new BoException("campo Id inválido");

            base.Update(obj);
        }


        public Funcionario Logar(string login, string senha)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha)) throw new BoException("login ou senha inválido");
            Funcionario funcionario = _funcionarioRepository.Logar(login);
            if (funcionario == null) throw new BoException("login inválido");
            if (!BCrypt.Net.BCrypt.Verify(senha, funcionario.Senha)) throw new BoException("Senha inválida");
            return funcionario;
        }

        public bool TrocarSenha(Funcionario funcionario, string senha, string senhaNova)
        {
            if (senhaNova.Length < 6 || senhaNova.Length > 12) throw new BoException("A senha deve ter entre 6 e 12 caracteres");
            if (!BCrypt.Net.BCrypt.Verify(senha, funcionario.Senha)) return false;

            senhaNova = BCrypt.Net.BCrypt.HashPassword(senhaNova);
            funcionario.Senha = senhaNova;
            Update(funcionario);
            return true;
        }

        public void PossuiFuncionario()
        {
            if(!_funcionarioRepository.GetAll().Any())
                Add(
                    new Funcionario()
                    {
                        Nome = "admin",
                        Senha = "admin123",
                        Login = "admin",
                        Administrador = true
                    });
        }
    }
}
