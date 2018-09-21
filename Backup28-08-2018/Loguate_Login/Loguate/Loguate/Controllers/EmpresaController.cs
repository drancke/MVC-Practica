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
    public class EmpresaController : Controller
    {

        private Empresa empresa = new Empresa();
        private Producto producto = new Producto();
        private LogicOneDB2Entities1 db = new LogicOneDB2Entities1();


        public ActionResult IndexEmpresa()
        {
            return View();
        }
 
        public ActionResult VerEmpresas()

        {
            return View(GetAllEmpresas());
        }
        IEnumerable<Empresa> GetAllEmpresas()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Empresas.ToList<Empresa>();

            }

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
        
        public Empresa Obtener(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {

                   empresa = db.Empresas
                                .Include("Cliente")
                                .Include("Productos")
                                .Where(x => x.ID_Empresas == id)
                                .Single();


                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }



            return empresa;
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


        public ActionResult Actualizar(int id = 0)
        {
           // Empresa em = new Empresa();
           ViewBag.Clientes = GetAllClientes();
            ViewBag.Producto = Todo();
            return View(id > 0 ? Obtener(id) : empresa);

        }

        [HttpPost]
        public ActionResult Guardar(Empresa empresas, int[] productos = null)
        {
            
        
            if (producto != null)
            {
                foreach (var p in productos)
                    empresas.Productos.Add(new Producto { ID_Producto = p });
            }

            gProducto(empresas);
            return Redirect("~/Empresa/IndexEmpresa/");
        }


        public void gProducto(Empresa empresass)
        {
            db.Database.ExecuteSqlCommand(
            "DELETE FROM Empresa_Producto WHERE ID_Empresas = @id",
            new SqlParameter("id", empresass.ID_Empresas));

            var productoDB = empresass.Productos;

            empresass.Productos = null;
            db.Entry(empresass).State = EntityState.Modified;
            empresass.Productos = productoDB;

            foreach (var c in empresass.Productos)
                db.Entry(c).State = EntityState.Unchanged;

            db.SaveChanges();


        }






        public ActionResult AgregarOrEditarEmpresas(int id = 0)
        {

            List<Cliente> clientesList = db.Clientes.ToList();
            ViewBag.clientesList = new SelectList(clientesList, "ID_Cliente", "Nombre");


            Empresa em = new Empresa();

            em.clientes = GetAllClientes();
            em.Productos = GetAllProductos();

            Empresa Empresas = new Empresa();
            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    //El objecto empresa que es x.Id_empresas == id si esto se cumple devuelveme el objecto.
                    em = db.Empresas.Where(x => x.ID_Empresas == id).FirstOrDefault<Empresa>();

                }


                em.clientes = GetAllClientes();
                em.Productos = GetAllProductos();
            }
            return View(em);
        }

        public JsonResult GetEmpleadoListe(int ID_Producto)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //creamos una lista y luego donde(x=>x.id de la table == al parametro id).Tolist();
            List<Producto> productosList = db.Productos.Where(x => x.ID_Producto == ID_Producto).ToList();
            return Json(productosList, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult AgregarOrEditarEmpresas(Empresa Empresas, List<int> lista)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {

                    foreach (int i in lista)
                    {
                        Producto p = db.Productos.Find(i);
                        Empresas.Productos.Add(p);

                    }


                    if (Empresas.ID_Empresas == 0)
                    {
                        db.Empresas.Add(Empresas);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(Empresas).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerEmpresas", GetAllEmpresas()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//fin metodo agragarOrEditarAreaModulos usando post.
        [Authorize]
        public ActionResult DeleteEmpresas(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Empresa Empresas = db.Empresas.Where(x => x.ID_Empresas == id).FirstOrDefault<Empresa>();
                    db.Empresas.Remove(Empresas);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerEmpresas", GetAllEmpresas()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }







    }
}