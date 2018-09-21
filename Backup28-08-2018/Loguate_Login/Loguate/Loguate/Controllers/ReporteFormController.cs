using Loguate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
namespace Loguate.Controllers
{
    [Authorize]
    public class ReporteFormController : Controller
    {
        // GET: ReporteForm

        public ActionResult IndexProyecto()
        {
            return View();
        }
       
        public ActionResult VerProyectos()
        {
            return View(GetAllFormProyectos());

        }
        //lista de proyectos
        IEnumerable<FormProyecto> GetAllFormProyectos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())

            {
                return db.FormProyectoes.ToList<FormProyecto>();
            }

        }

       IEnumerable<Consultor> GetAllConsultor()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Consultors.ToList<Consultor>();
            }
        }


        IEnumerable<AreaModulo> GetAllAreaModulos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.AreaModuloes.ToList<AreaModulo>();
            }
        }

        IEnumerable<Producto> GetAllProductos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Productos.ToList<Producto>();
            }

        }

        IEnumerable<Herramienta> GetAllHerramientas()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Herramientas.ToList<Herramienta>();
            }
        }

        IEnumerable<Tipo> GetAllTipos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Tipoes.ToList<Tipo>();
            }
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


        //Fin de la lista de proyectos
        [Authorize]
        public ActionResult AgregarOrEditarFormProyecto(int id = 0)
        {

            FormProyecto reporte = new FormProyecto();
            reporte.consultors = GetAllConsultor();
            reporte.areaModulos = GetAllAreaModulos();
            reporte.herramientas = GetAllHerramientas();
            reporte.empresas = GetAllEmpresas();
            reporte.productos = GetAllProductos();
            reporte.tipos = GetAllTipos();
            reporte.clientes = GetAllClientes();




            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {

                    reporte = db.FormProyectoes.Where(x => x.ID_Proyectos == id).FirstOrDefault<FormProyecto>();
                }

                reporte.consultors = GetAllConsultor();
                reporte.areaModulos = GetAllAreaModulos();
                reporte.herramientas = GetAllHerramientas();
                reporte.empresas = GetAllEmpresas();
                reporte.productos = GetAllProductos();
                reporte.tipos = GetAllTipos();
                reporte.clientes = GetAllClientes();

            }

            return View(reporte);
        }


        public JsonResult GetAllEmpresa(int ID_Clientes)
        {


            List<Empresa> listEmpresa = new List<Empresa>();

            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                db.Configuration.ProxyCreationEnabled = false;
                listEmpresa = (db.Empresas.Where(x => x.ID_Clientes == ID_Clientes)).ToList<Empresa>();
                return Json(listEmpresa, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpPost]
        public ActionResult AgregarOrEditarFormProyecto(FormProyecto reporte)
        {

            try
            {
                if (reporte.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(reporte.ImageUpload.FileName);
                    string extension = Path.GetExtension(reporte.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    reporte.Atachar_Archivo = "~/AppFiles/Images/" + fileName;
                    reporte.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    if (reporte.ID_Proyectos == 0)
                    {
                        db.FormProyectoes.Add(reporte);
                        db.SaveChanges();
                    }
                    else
                    {
                 

                        db.Entry(reporte).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerProyectos", GetAllFormProyectos()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteForm(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    FormProyecto reporte = db.FormProyectoes.Where(x => x.ID_Proyectos == id).FirstOrDefault<FormProyecto>();
                    db.FormProyectoes.Remove(reporte);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerProyectos", GetAllFormProyectos()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}