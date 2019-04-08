
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Controllers
{
    public class DepartamentoController : ControllerBase
    {
        private readonly ISubClasseBo _subClasseBo;
        private readonly IGrupoBo _grupoBo;
        private readonly IProdutoBo _produtoBo;
        public DepartamentoController()
        {
            _subClasseBo = new SubClasseBo(_context);
            _grupoBo = new GrupoBo(_context);
            _produtoBo = new ProdutoBo(_context);
        }


        // GET: Departamento
        public ActionResult Index(int? codDepartamento, int? page)
        {
            try
            {
                if (codDepartamento == null)
                    return View("Error");

                var subclasse =_subClasseBo.GetById((int)codDepartamento);

                if (subclasse == null)
                    return View("Error");

                ViewBag.Departamento = subclasse.Nome;


                IQueryable<Produto> lista = _produtoBo.GetBySubClasse(subclasse);

                IPagedList<Produto> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 20);

                return View(model);

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
        public ActionResult Categoria(int? codCategoria, int? page)
        {
            try
            {
                if (codCategoria == null)
                    return View("Error");

                var categoria = _grupoBo.GetById((int)codCategoria);

                if (categoria == null)
                    return View("Error");

                ViewBag.Categoria = categoria.Nome;
                ViewBag.Departamento = categoria.SubClasse.Nome;
                ViewBag.codDepartamento = categoria.SubClasse.SubClasseId;
                
                IQueryable<Produto> lista = _produtoBo.GetByGrupo(categoria);

                IPagedList<Produto> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 20);

                return View(model);

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

        public ActionResult MenuPrincipal()
        {
            try
            {
                return PartialView(_subClasseBo.GetAll().ToList());
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

    }
}