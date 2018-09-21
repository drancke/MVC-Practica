using Loguate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Loguate.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {

        private Cliente cliente = new Cliente();
        private Producto producto = new Producto();


        private LogicOneDB2Entities1 db = new LogicOneDB2Entities1();


        // GET: Clientes
        public ActionResult IndexClientes()
        {
            return View();
        }
   
        public ActionResult VerClientes()
        {
            return View(GetAllClientes());
        }
        IEnumerable<Cliente> GetAllClientes()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Clientes.ToList<Cliente>();
            }
        }


        List<Producto> GetAllProductos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Productos.ToList<Producto>();
            }
        }

        //Metodo que agrega, edita y elimina un Clientes por el metodo GET
        public ActionResult AgregarOrEditarCliente(int id = 0)
        {  

            Cliente Clientes = new Cliente();
            //Clientes.prodductos = GetAllProductos();
            Clientes.Productos = GetAllProductos();


            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Clientes = db.Clientes.Where(x => x.ID_Cliente == id).FirstOrDefault<Cliente>();
                }


                Clientes.Productos = GetAllProductos();
            }
            return View(Clientes);
        }//Fin metodo AgregarOrEditarClientes


        IEnumerable<Cliente> GetClientes()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Clientes.ToList<Cliente>();

            }

        }


        public Cliente Obtener(int id)
        {
            //var cliente = new Cliente();

            try
            {
                using (var contex = new LogicOneDB2Entities1())
                {
                    cliente = contex.Clientes
                                    .Include("Productos")
                                    .Where(x => x.ID_Cliente == id)
                                    .Single();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return cliente;
        }


        public ActionResult Ver(int id)
        {

            return View(Obtener(id));
        }

        public List<Producto> Todo()
        {  
            var producto = new List<Producto>();

            try
            {
                using (var db = new LogicOneDB2Entities1())
                {
                    producto = db.Productos.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }



            return producto;
        }


        public ActionResult Actualizar(int id =0)
        {

            ViewBag.Producto = Todo();

            return View(id > 0 ? Obtener(id) : cliente);

        }

        [HttpPost]
        public ActionResult Guardar(Cliente clientes, int[] productos = null)
        {

            if (producto != null)
            {
                if (productos != null)
                    NewMethod(clientes, productos);
            }

            GProducto(clientes);
            return Redirect("~/Clientes/IndexClientes/");
        }

        private static void NewMethod(Cliente clientes, int[] productos)
        {
            foreach (var p in productos)
                clientes.Productos.Add(new Producto { ID_Producto = p });
        }

        public void GProducto(Cliente clientess)
        {
            try
            {
                db.Database.ExecuteSqlCommand(
                    "DELETE FROM Cliente_Producto WHERE ID_Cliente= @id",
                    new SqlParameter("id", clientess.ID_Cliente));

                var productoDb = clientess.Productos;

                clientess.Productos = null;
                db.Entry(clientess).State = EntityState.Modified;
                clientess.Productos = productoDb;

                foreach (var c in clientess.Productos)
                    db.Entry(c).State = EntityState.Unchanged;
                NewMethod1();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            
            }
          

          
    }
  private void NewMethod1()
            {
                db.SaveChanges();
            }

        [HttpPost]
        public ActionResult AgregarOrEditarCliente(Cliente Clientes, List<int> lista)
        {

            try
            {

                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {

                    foreach (int i in lista)
                    {
                        Producto p = db.Productos.Find(i);
                        Clientes.Productos.Add(p);

                    }


                    if (Clientes.ID_Cliente == 0)
                    {
                        db.Clientes.Add(Clientes);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(Clientes).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerClientes", GetAllClientes()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "El codigo RNC en el cliente debe ser unico." }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult DeleteClientes(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Cliente Clientes = db.Clientes.Where(x => x.ID_Cliente == id).FirstOrDefault<Cliente>();
                    db.Clientes.Remove(Clientes);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerClientes", GetAllClientes()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//fin metodo DeleteClientess


    }
}