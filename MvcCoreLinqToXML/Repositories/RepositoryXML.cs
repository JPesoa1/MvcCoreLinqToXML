using MvcCoreLinqToXML.Helpers;
using MvcCoreLinqToXML.Models;
using System.Xml.Linq;

namespace MvcCoreLinqToXML.Repositories
{
    public class RepositoryXML
    {
        private HelperPathProvider helper;
        private XDocument documentClientes;
        private string pathClientes;
        public RepositoryXML(HelperPathProvider helper)
        {
            this.helper = helper;
            pathClientes = this.helper.MapPath("ClientesID.xml", Folders.Documents);
            documentClientes = XDocument.Load(pathClientes);
        }

        public List<Joyeria> GetJoyerias()
        {
            string path = helper.MapPath("joyerias.xml", Folders.Documents);

            XDocument document = XDocument.Load(path);
            List<Joyeria> joyerias = new List<Joyeria>();
            var consulta = from datos in document.Descendants("joyeria")
                           select datos;

            foreach (XElement tag in consulta)
            {
                Joyeria joyeria = new Joyeria();

                joyeria.CIF = tag.Attribute("cif").Value;
                joyeria.Nombre = tag.Element("nombrejoyeria").Value;
                joyeria.Telefono = tag.Element("telf").Value;
                joyeria.Direccion = tag.Element("direccion").Value;

                joyerias.Add(joyeria);
            }
            return joyerias;
        }

        public List<Cliente> GetClientes()
        {
            string path = helper.MapPath("ClientesID.xml", Folders.Documents);

            XDocument document = XDocument.Load(path);
            List<Cliente> clientes = new List<Cliente>();
            var consulta = from datos in document.Descendants("CLIENTE")
                           select datos;

            foreach (XElement tag in consulta)
            {
                Cliente cliente = new Cliente();

                cliente.Idcliente = int.Parse(tag.Element("IDCLIENTE").Value);
                cliente.Nombre = tag.Element("NOMBRE").Value;
                cliente.Direccion = tag.Element("DIRECCION").Value;
                cliente.Email = tag.Element("EMAIL").Value;
                cliente.ImagenCliente = tag.Element("IMAGENCLIENTE").Value;

                clientes.Add(cliente);

            }
            return clientes;
        }

        public Cliente FindCliente(int idcliente)
        {
            var consulta = from datos in
                               this.documentClientes.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value ==
                           idcliente.ToString()
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                XElement tag = consulta.FirstOrDefault();
                Cliente cliente = new Cliente();
                cliente.Idcliente = int.Parse(tag.Element("IDCLIENTE").Value);
                cliente.Nombre = tag.Element("NOMBRE").Value;
                cliente.Direccion = tag.Element("DIRECCION").Value;
                cliente.Email = tag.Element("EMAIL").Value;
                cliente.ImagenCliente = tag.Element("IMAGENCLIENTE").Value;
                return cliente;
            }
        }


        private XElement FindXmlCliente(string id)
        {
            var consulta = from datos in this.documentClientes.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void DeleteCliente(int idcliente)
        {
            XElement clienteXML = this.FindXmlCliente(idcliente.ToString());
            clienteXML.Remove();

            this.documentClientes.Save(this.pathClientes);
        }

        public void UpdateCliente
            (int idcliente, string nombre, string email, string direccion, string imagen)
        {

            XElement tag = this.FindXmlCliente(idcliente.ToString());
            Cliente cliente = new Cliente();
            idcliente = int.Parse(tag.Element("IDCLIENTE").Value);
            tag.Element("NOMBRE").Value = nombre;
            tag.Element("DIRECCION").Value = direccion;
            tag.Element("EMAIL").Value = email;
            tag.Element("IMAGENCLIENTE").Value = imagen;
            this.documentClientes.Save(this.pathClientes);

        }

        public void CreateCliente
            (int idcliente, string nombre, string email, string direccion, string imagen)
        {
            XElement rootCliente = new XElement("CLIENTE");
            rootCliente.Add(new XElement("IDCLIENTE",idcliente));    
            rootCliente.Add(new XElement("NOMBRE",nombre));    
            rootCliente.Add(new XElement("DIRECCION",direccion));    
            rootCliente.Add(new XElement("EMAIL",email));    
            rootCliente.Add(new XElement("IMAGENCLIENTE",imagen));

            this.documentClientes.Element("CLIENTES").Add(rootCliente);
            this.documentClientes.Save(this.pathClientes);

        }

    }
}
