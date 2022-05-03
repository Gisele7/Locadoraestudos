using Filme_Locadora.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Filme_Locadora.Models;
using System;
using System.Collections.Generic;



namespace Filme_Locadora.Controllers
{
    public class FilmesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            //case sensitive. pegamos da classe categorias o tem Id e Descricao

            //Vamos encaminhar p view a lista de categorias pra que quando categoria for selecionada, o id faça parte do produto.
            ViewBag.ListaCategorias = new SelectList(DbFilmes.ListarCategorias(), "Id", "Descricao");
            return View();
        }
        [HttpPost]
        // POSTED DA IMAGEM 
        public ActionResult Incluir(Tbfilmes filme)
        {
            if (!ModelState.IsValid)
            {
                return Incluir();
            }
            //// IMAGEM 
            //if (image != null)
            //{
            //    filme.MimeType = image.ContentType;
            //    filme.Foto = new byte[image.ContentLength];

            //    //Copiando os bytes da imagem recebida como parâmetro, na propriedade foto.
            //    //Parâmetros: 1° Quem vai receber os bytes? | 2° A partir de posição que a gente vai começar a tratar esses bytes? | 3° Tratar até o final 
            //    image.InputStream.Read(filme.Foto, 0, image.ContentLength);
            //}

            DbFilmes.Incluir(filme);
            return RedirectToAction("Listar");
        }

        //IMAGEM 
        public FileResult BuscarProduto(int id)
        {
            try
            {
                var produto = DbFilmes.Buscar(id);
                if (produto != null)
                {
                    if (produto.Foto != null)
                    {
                        return File(produto.Foto, produto.MimeType);
                    }
                }
                return File("~/Images/ND.png", "image/jpeg");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Alterar(string id)
        {
            return ExecutarAction(id, "Alterar");

        }

        public ActionResult Listar()
        {
            return View(DbFilmes.Listar());
        }
        [HttpPost]
        public ActionResult Alterar(Tbfilmes filme)
        {
            if (!ModelState.IsValid)
            {
                return View(filme);
            }
            try
            {
                DbFilmes.Alterar(filme);
                //LISTAR é o nome do método. ELe sai do action que voce está e vai para outro
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                //Quando voce tem um retorno view e passa como parametro a view, o parametro é o nome do arquivo(pág). Aqui ele não sai do action que está
                return View("_Error");
            }
        }
        public ActionResult ExibirDetalhes(string id)
        {
            return ExecutarAction(id, "ExibirDetalhes");
        }
        public ActionResult ExecutarAction(string id, string view)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("É necessário informar o documento.");
                }

                var filme = DbFilmes.Buscar(int.Parse(id));
                if (filme == null)
                {
                    throw new Exception("Nenhum filme encontrado com o documento informado.");
                }
                return View(view, filme);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Error");
            }
        }
        public ActionResult Excluir(string id)
        {
            return ExecutarAction(id, "Excluir");
        }
        [HttpPost]
        public ActionResult Excluir(Tbfilmes filme)
        {
            try
            {
                DbFilmes.Excluir(filme);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Error");
            }

        }
    }

}
