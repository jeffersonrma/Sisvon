using System;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;


namespace SisvonMVC.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly IClienteBo _clienteBo;
        private readonly IPedidoBo _pedidoBo;
        public ClienteController()
        {
            _clienteBo = new ClienteBo(_context);
            _pedidoBo = new PedidoBo(_context);
        }

        public ActionResult Cadastrar(bool? pedido)
        {
            ViewData["pedido"] = pedido ?? false;
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Cliente cliente, string confirmarSenha, bool pedido)
        {
            ViewData["pedido"] = pedido;
            try
            {
                if (confirmarSenha != cliente.Senha)
                {
                    ModelState.AddModelError("confirmarSenha", "As senhas não conferem! ");
                    return View();
                }
                _clienteBo.Add(cliente);
                LogarCliente(cliente);
                SucessoMessage = "Cadastrado com sucesso!";
                return pedido ? RedirectToAction("Login", "Pedido") : RedirectToAction("Index", "Home");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }

        public ActionResult MeuCadastro()
        {

            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                return View(_clienteBo.GetById(Usuario.ClienteId));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View(_clienteBo.GetById(Usuario.ClienteId));
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult EditarCadastro()
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                return View(_clienteBo.GetById(Usuario.ClienteId));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View(_clienteBo.GetById(Usuario.ClienteId));
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EditarCadastro(Cliente obj)
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                Cliente cliente = _clienteBo.GetById(obj.ClienteId);

                cliente.Nome = obj.Nome;
                cliente.Telefone = obj.Telefone;
                cliente.Cep = obj.Cep;
                cliente.Uf = obj.Uf;
                cliente.Cidade = obj.Cidade;
                cliente.Rua = obj.Rua;
                cliente.Complemento = obj.Complemento;
                cliente.Numero = obj.Numero;

                _clienteBo.Update(cliente);
                SucessoMessage = "Alterações salvas com sucesso!";
                return RedirectToAction("MeuCadastro");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View(_clienteBo.GetById(Usuario.ClienteId));
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult MudarSenha()
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                return View();
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult MudarSenha(string txtSenha, string txtSenhaNova, string txtSenhaRepetir)
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                if (txtSenhaNova != txtSenhaRepetir)
                {
                    ModelState.AddModelError("txtSenhaRepetir", "Repita a senha corretamente");
                    return View();
                }

                if (_clienteBo.TrocarSenha(_clienteBo.GetById(Usuario.ClienteId), txtSenha, txtSenhaNova))
                {
                    SucessoMessage = "Senha Trocada com sucesso";
                    return RedirectToAction("MeuCadastro");
                }
                ModelState.AddModelError("txtSenha", "Senha incorreta");
                return View();
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }


        public ActionResult MeusPedidos()
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            try
            {
                return View(_pedidoBo.BuscarPorCliente(Usuario));

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult DetalhePedido(int? id)
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Você precisa estar logado!";
                return RedirectToAction("index", "Home");
            }
            if (id == null) return View("Error");

            try
            {
                return View(_pedidoBo.GetOneByCliente(Usuario, (int)id));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("MeusPedidos");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        [HttpPost]
        public string VerificarClienteEmUso(string Email, string Cpf, string Rg)
        {
            if (!string.IsNullOrEmpty(Email))
                return (!_clienteBo.EmailEmUso(Email)).ToString().ToLower();
            if (!string.IsNullOrEmpty(Cpf))
                return (!_clienteBo.CpfEmUso(Cpf)).ToString().ToLower();
            if (!string.IsNullOrEmpty(Rg))
                return (!_clienteBo.RgEmUso(Rg)).ToString().ToLower();

            return "true";

        }
    }
}
