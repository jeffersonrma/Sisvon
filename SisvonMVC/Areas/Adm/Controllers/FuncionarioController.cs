using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioBo _funcionarioBo;


        public FuncionarioController()
        {
            _funcionarioBo = new FuncionarioBo(_context);
        }
        // GET: Adm/Funcionario
        public ActionResult Index(int? page, string busca)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

                ViewBag.busca = busca;

                IQueryable<Funcionario> lista = _funcionarioBo.GetAll();

                if (!string.IsNullOrEmpty(busca))
                    lista = lista.Where(x => x.Nome.Contains(busca));

                IPagedList<Funcionario> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 10);
                return View(model);

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }

        }

        // GET: Adm/Funcionario/Details/5
        public ActionResult Detalhes(int? id)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");
                if (id == null) View("Error");

                return View(_funcionarioBo.GetById((int)id));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        // GET: Adm/Funcionario/Cadastrar
        public ActionResult Cadastrar()
        {
            if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: Adm/Funcionario/Create
        [HttpPost]
        public ActionResult Cadastrar(Funcionario funcionario)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

                _funcionarioBo.Add(funcionario);
                return RedirectToAction("Index");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        // GET: Adm/Funcionario/Edit/5
        public ActionResult Editar(int? id)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");
                if (id == null) View("Error");

                return View(_funcionarioBo.GetById((int)id));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        // POST: Adm/Funcionario/Edit/5
        [HttpPost]
        public ActionResult Editar(Funcionario funcionario)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");


                Funcionario obj = _funcionarioBo.GetById(funcionario.FuncionarioId);
                obj.Administrador = funcionario.Administrador;
                obj.Login = funcionario.Login;
                obj.Nome = funcionario.Nome;

                _funcionarioBo.Update(obj);

                return RedirectToAction("Index");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        // GET: Adm/Funcionario/Delete/5
        public ActionResult Deletar(int? id)
        {
            try
            {
                if (id == null) return View("Error");
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

                return View(_funcionarioBo.GetById((int)id));
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        // POST: Adm/Funcionario/Delete/5
        [HttpPost]
        public ActionResult Deletar(int id)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

                _funcionarioBo.Remove(
                    _funcionarioBo
                    .GetById(id));

                return RedirectToAction("Index");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        public ActionResult TrocarSenha()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
            try
            { 
                return View(_funcionarioBo.GetById(Usuario.FuncionarioId));
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
           
        }

        [HttpPost]
        public ActionResult TrocarSenha(string txtSenha, string txtNovaSenha)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                if (_funcionarioBo.TrocarSenha(
                    _funcionarioBo.GetById(Usuario.FuncionarioId),
                    txtSenha,
                    txtNovaSenha))

                    SucessoMessage = "Senha Alterada Com Sucesso";
                else
                {
                    ErroMessage = "Senha incorreta";
                    ModelState.AddModelError("txtSenha", "Senha incorreta");
                }
                return RedirectToAction("TrocarSenha");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("TrocarSenha");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
    }
}
