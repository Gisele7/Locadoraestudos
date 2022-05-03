using Filme_Locadora.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Filme_Locadora.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Incluir(Tbclientes cliente)
        {
            if (cliente.Documento != null)
            {
                if (cliente.Documento.Length != 11 && cliente.Documento.Length != 14)
                {
                    //Só passa por aqui se o campo conter qntd de digitos diferentes de 11 ou 14.
                    ModelState.AddModelError("Documento", "O documento deve conter 11 ou 14 dígitos!");
                }
            }
            //Vem para cá DIRETAMENTE quando o campo contém 11 ou 14 digitos
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                DbClientes.Incluir(cliente);
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
        public ActionResult Listar()
        {
            return View(DbClientes.Listar());
        }
        public ActionResult Alterar(string id)
        {
            return ExecutarAction(id, "Alterar");

        }
        [HttpPost]
        public ActionResult Alterar(Tbclientes cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            try
            {
                DbClientes.Alterar(cliente);
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
        public ActionResult Excluir(string id)
        {
            return ExecutarAction(id, "Excluir");
        }

        public ActionResult ExecutarAction(string id, string view)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("É necessário informar o documento.");
                }

                var cliente = DbClientes.Buscar(id);
                if (cliente == null)
                {
                    throw new Exception("Nenhum cliente encontrado com o documento informado.");
                }
                return View(view, cliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Error");
            }
        }
        [HttpPost]
        public ActionResult Excluir(Tbclientes cliente)
        {
            try
            {
                DbClientes.Excluir(cliente);
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
