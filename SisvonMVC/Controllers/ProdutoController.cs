using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Controllers
{
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoBo _produtoBo;

        public ProdutoController()
        {
            _produtoBo = new ProdutoBo(_context);
        }
        // GET: Produto
        public ActionResult Detalhes(int? id)
        {
            if (id == null) return View("Error");
            try
            {
                var produtoModel = _produtoBo.GetById((int)id);
                if (produtoModel == null) return View("Error");


                return View(produtoModel);
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("Detalhes");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }


        public ActionResult MostrarFotos(int? produtoOrdem)
        {
            if (produtoOrdem == null) return View("Error");
            try
            {

                return PartialView(BuscarFotos((int)produtoOrdem));
            }
            catch (Exception exception)
            {
                return View("Error");
            }

        }
        public ActionResult MostrarUmaFoto(int? produtoOrdem)
        {
            if (produtoOrdem == null) return PartialView("Error");
            try
            {

                return PartialView(BuscarFotos((int)produtoOrdem));
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return PartialView("Error");
            }

        }
        public ActionResult Buscar(int? page, string busca)
        {
            if (string.IsNullOrEmpty(busca)) return View();
            try
            {
                TempData["busca"] = busca.Trim();
                
                IQueryable<Produto> lista = _produtoBo.BuscarPorNome(busca);
               
                IPagedList<Produto> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 8);

                return View(model);
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return PartialView("Error");
            }

        }


        private string[] BuscarFotos(int produtoOrdem)
        {
            string caminho = Server.MapPath("~/Imagens/Produtos/" + produtoOrdem + "/imagens.json");
            IEnumerable<string> itens = new Collection<string>();

            if (System.IO.File.Exists(caminho))
            {
                string jsonstring = System.IO.File.ReadAllText(caminho);

                try
                {
                    var json = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(jsonstring);
                    itens = json
                        .OrderBy(x => x.Posicao)
                        .Select(x => produtoOrdem + "/" + (string)x.Nome);
                }
                catch (Exception ex)
                {
                    LogMessage = ex;
                }
            }

            return itens.ToArray();
        }
    }
}
