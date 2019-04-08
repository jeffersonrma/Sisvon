using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoBo _produtoBo;
        private readonly ISincronizarBo _sincronizarBo;

        public ProdutoController()
        {
            _sincronizarBo = new SincronizarBo(_context);
            _produtoBo = new ProdutoBo(_context);
        }

        public ActionResult Index(int? page, string busca, bool? ativo)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                ViewBag.busca = busca;
                ViewBag.ativo = ativo;

                IQueryable<Produto> lista = _produtoBo.GetAll();
                if (!string.IsNullOrEmpty(busca))
                    lista = lista.Where(x => x.Nome.Contains(busca) || x.Codigo.Contains(busca));
                if (ativo != null)
                    lista = lista.Where(x => x.Ativo == ativo);
                IPagedList<Produto> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 10);
                return View(model);
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        // GET: Adm/Produto/Details/5
        public ActionResult Detalhes(int? id)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                if (id == null) return View("Error");

                return View(_produtoBo.GetById((int)id));
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View(_produtoBo.GetById((int)id));
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ImportarProdutos()
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                _sincronizarBo.ImportarPorSession(Session.SessionID);

                return RedirectToAction("Importar");
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return RedirectToAction("Importar");
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }

        }


        public ActionResult Importar()
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                return View(_sincronizarBo.BuscarPorSession(Session.SessionID));
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult Importar(ICollection<int> chSincronizar)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                if (chSincronizar == null || chSincronizar.Count == 0)
                {
                    ErroMessage = "Nem um produto selecionado";
                    return RedirectToAction("Importar");
                }

                _produtoBo.GravarProdutosImportados(chSincronizar
                    .Select(id => _sincronizarBo.GetById(id))
                    .ToList());

                SucessoMessage = "Produtos Importados com sucesso";
                return RedirectToAction("Index");
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return RedirectToAction("Importar");
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        public ActionResult Atualizar()
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                return View();
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult AtualizarProdutos()
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                _produtoBo.AtualizarProdutosImportados();
                SucessoMessage = "Produtos atualizados com sucesso!";
                return RedirectToAction("Atualizar");
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return RedirectToAction("Atualizar");
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }


        // GET: Adm/Produto/Edit/5
        public ActionResult Editar(int? id)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                if (id == null) return View("Error");

                return View(_produtoBo.GetById((int)id));
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View(_produtoBo.GetById((int)id));
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        // POST: Adm/Produto/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Editar(Produto obj)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                Produto produto = _produtoBo.GetById(obj.ProdutoId);
                produto.Nome = obj.Nome;
                produto.ValorPac = obj.ValorPac;
                produto.Descricao = obj.Descricao;
                produto.Destaque = obj.Destaque;
                produto.Ativo = obj.Ativo;
                produto.ValorSedex = obj.ValorSedex;
                produto.Vitrine = obj.Vitrine;
                _produtoBo.Update(produto);

                SucessoMessage = "Salvo com sucesso";
                return RedirectToAction("Index");
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View(_produtoBo.GetById(obj.ProdutoId));

            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        // GET: Adm/Produto/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
            if (id == null) return View("Error");
            try
            {
                return View(_produtoBo.GetById((int)id));
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return RedirectToAction("Excluir");
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        // POST: Adm/Produto/Delete/5
        [HttpPost]
        public ActionResult Excluir(Produto produto)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                _produtoBo.Remove(
                    _produtoBo
                    .GetById(produto.ProdutoId));

                SucessoMessage = "Produto excluido com sucesso";
                return RedirectToAction("Index");
            }
            catch (BoException ex)
            {
                ErroMessage = ex.Message;
                return View(_produtoBo.GetById(produto.ProdutoId));
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        public ActionResult MostrarFotosAjax(int? produtoOrdem)
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
            if (produtoOrdem == null) return View("Error");
            try
            {

                return PartialView(BuscarFotos((int)produtoOrdem));
            }
            catch (BoException ex)
            {
                return View();
            }
            catch (Exception ex)
            {
                LogMessage = ex;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UploadFoto(int? produtoordem)
        {
            bool isUploaded = false;
            string message = "Upload Falhou";

            if (produtoordem == null) return Json(new { isUploaded = isUploaded, message = message }, "text/html");

            HttpPostedFileBase fotoUp = Request.Files["inFoto"];

            if (fotoUp == null || fotoUp.ContentLength == 0) return Json(new { isUploaded = isUploaded, message = message }, "text/html");

            string caminho = Server.MapPath("~/Imagens/Produtos/" + produtoordem);

            if (!CriarPasta(caminho)) return Json(new { isUploaded = isUploaded, message = message }, "text/html");
            try
            {
                string nome = GerarNome();
                nome = string.Format("{0}.{1}", nome, fotoUp.FileName.Split('.').Last());
                fotoUp.SaveAs(Path.Combine(caminho, nome));

                SalvarJson(caminho, nome);

                isUploaded = true;
                message = "Foto salva com sucesso";
            }
            catch (Exception exception)
            {
                message = string.Format("{0}: {1}", message, exception.Message);
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");

        }

        private void SalvarJson(string caminho, string nome)
        {
            caminho += "/imagens.json";
            string jsonstring = "";
            ICollection<dynamic> json;

            if (System.IO.File.Exists(caminho))
                jsonstring = System.IO.File.ReadAllText(caminho);

            try
            {
                json = JsonConvert.DeserializeObject<ICollection<dynamic>>(jsonstring);
                json.Add(new { Nome = nome, Posicao = (int)json.OrderBy(x => x.Posicao).Last().Posicao + 1 });
            }
            catch (Exception ex)
            {
                json = new Collection<dynamic> { new { Nome = nome, Posicao = 1 } };
            }
            System.IO.File.WriteAllText(caminho, JsonConvert.SerializeObject(json, Formatting.Indented));
        }

        private void RemoverJson(string caminho, string nome)
        {
            caminho += "/imagens.json";
            string jsonstring = "";
            ICollection<dynamic> json;

            if (System.IO.File.Exists(caminho))
                jsonstring = System.IO.File.ReadAllText(caminho);

            try
            {
                json = JsonConvert.DeserializeObject<ICollection<dynamic>>(jsonstring);
                json.Remove(json.First(x => x.Nome == nome));
            }
            catch (Exception ex)
            {
                json = new Collection<dynamic> {};
            }
            System.IO.File.WriteAllText(caminho, JsonConvert.SerializeObject(json, Formatting.Indented));
        }
        private bool CriarPasta(string path)
        {
            bool result = true;
            if (Directory.Exists(path)) return result;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception exception)
            {
                TempData["Erro"] = "Falha no Upload" + exception.Message;
                result = false;
            }
            return result;
        }
        private string GerarNome()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoString = clsRan.Next(6, 18);

            string nome = "";
            for (Int32 i = 0; i <= tamanhoString; i++)
            {
                nome += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return nome;
        }
        private string[] BuscarFotos(int ProdutoOrdem)
        {
            string caminho = Server.MapPath("~/Imagens/Produtos/" + ProdutoOrdem + "/imagens.json");
            IEnumerable<string> itens = new Collection<string>();

            if (System.IO.File.Exists(caminho))
            {
                string jsonstring = System.IO.File.ReadAllText(caminho);

                try
                {
                    var json = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(jsonstring);
                    itens = json
                        .OrderBy(x => x.Posicao)
                        .Select(x => ProdutoOrdem + "/" + (string)x.Nome);
                }
                catch (Exception ex)
                {
                    LogMessage = ex;
                }
            }

            return itens.ToArray();

        }

        public string RemoverImagen(string nome)
        {
            if (!UsuarioEstaLogado)
                return "Usuario não está Logado";
            try
            {
                string caminho = Server.MapPath("~/Imagens/Produtos/" + nome);
                if (!System.IO.File.Exists(caminho))
                    return "Imagem não emcontrada";

                RemoverJson(Server.MapPath("~/Imagens/Produtos/" + nome.Split('/')[0]), nome.Split('/')[1]);
                System.IO.File.Delete(caminho);
                return "Imagem Removida com Sucesso!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

