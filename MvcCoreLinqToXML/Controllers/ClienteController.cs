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

        public IActionResult Details(int id)
        {

            Cliente cliente = this.repo.FindCliente(id);
            return View(cliente);
        }

        public IActionResult Delete(int id)
        {

            this.repo.DeleteCliente(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Cliente cliente = this.repo.FindCliente(id);
            return View(cliente);
        }


        [HttpPost]
        public IActionResult Update(Cliente cliente)
        {
            this.repo.UpdateCliente(cliente.Idcliente, cliente.Nombre, cliente.Email, cliente.Direccion,
                cliente.ImagenCliente);
            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            this.repo.CreateCliente(cliente.Idcliente, cliente.Nombre, cliente.Email, cliente.Direccion,
                cliente.ImagenCliente);
            return RedirectToAction("Index");
        }


    }
}
