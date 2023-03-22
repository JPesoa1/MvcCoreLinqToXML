using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToXML.Models;
using MvcCoreLinqToXML.Repositories;

namespace MvcCoreLinqToXML.Controllers
{
    public class ClienteController : Controller
    {
        private RepositoryXML repo;

        public ClienteController(RepositoryXML repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Cliente> clientes = this.repo.GetClientes();
            return View(clientes);
        }

        public IActionResult Details(int id) {

            Cliente cliente = this.repo.FindCliente(id);
            return View(cliente);
        }

        public IActionResult Delete(int id) {

            this.repo.DeleteCliente(id);
            return RedirectToAction("Index");
        }
    }
}
