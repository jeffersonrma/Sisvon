
namespace Sisvon.Model.Entities
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public bool Administrador { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

    }
}