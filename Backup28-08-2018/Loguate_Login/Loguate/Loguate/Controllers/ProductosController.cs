using Loguate.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loguate.Models;
using Newtonsoft.Json;

namespace Loguate.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        LogicOneDB2Entities1 db = new LogicOneDB2Entities1();
        // GET: Productos
        [Authorize]
        public ActionResult IndexProducto()
        {
            return View();
        }
        [Authorize]
        public ActionResult VerProductos()
        {

            return View(GetAllProductos());
        }
        IEnumerable<Producto> GetAllProductos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Productos.ToList<Producto>();
            }

        }

        [Authorize]
        public ActionResult AgregarOrEditarProducto(int id = 0)
        {
            Producto producto = new Producto();
            //ProductosEmpresa productoempresa = new ProductosEmpresa();



            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    producto = db.Productos.Where(x => x.ID_Producto == id).FirstOrDefault<Producto>();
                    //productoempresa = db.ProductosEmpresas.Where(x => x.ID_Producto == id).FirstOrDefault<ProductosEmpresa>();


                }
            }
            return View(producto);
        }



        [HttpPost]
        public ActionResult AgregarOrEditarProducto(Producto producto)
        {
            try
            {
                //ProductosEmpresa productoempresa = new ProductosEmpresa();

                //productoempresa.ID_Producto = producto.ID_Producto;
                //productoempresa.Nombre = producto.Nombre;
                //productoempresa.Descripcion = producto.Descripcion;


                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    if (producto.ID_Producto == 0)
                    {
                        db.Productos.Add(producto);
                        //db.ProductosEmpresas.Add(productoempresa);

                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(producto).State = EntityState.Modified;
                        //db.Entry(productoempresa).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerProductos", GetAllProductos()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//fin metodo agragarOrEditarAreaModulos usando post.

        public ActionResult DeleteProducto(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Producto producto = db.Productos.Where(x => x.ID_Producto == id).FirstOrDefault<Producto>();
                    //ProductosEmpresa productoempresa = db.ProductosEmpresas.Where(x => x.ID_Producto == id).FirstOrDefault<ProductosEmpresa>();
                    db.Productos.Remove(producto);
                    //db.ProductosEmpresas.Remove(productoempresa);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerProductos", GetAllProductos()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //Comienza desde aqui el Json
        public JsonResult VerproductoList()
        {
            List<Productss> p = db.Productos.Where(x => x.ID_Producto > 0).Select(x => new Productss
            {
                ID_Producto = x.ID_Producto,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion
            }).ToList();
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerproductoListID(int ID_Producto)
        {
            Producto model = db.Productos.Where(x => x.ID_Producto == ID_Producto).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }//Aqui termina

        public JsonResult SaveData(Productss model)
        {
            var result = false;
            try
            {
                if (model.ID_Producto > 0)
                {
                    Producto p = db.Productos.SingleOrDefault(x => x.ID_Producto == model.ID_Producto);
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Producto p = new Producto();
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.Productos.Add(p);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }//Aqui termina

        public JsonResult Delete(int id)
        {
            bool result = false;
            Producto p = db.Productos.SingleOrDefault(x => x.ID_Producto == id);
            if (p != null)
            {
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }






    }
}