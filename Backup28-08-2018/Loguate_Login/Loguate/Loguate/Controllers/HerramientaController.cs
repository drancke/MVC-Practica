using Loguate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loguate.Controllers;
using Newtonsoft.Json;

namespace Loguate.Controllers
{
    [Authorize]
    public class HerramientaController : Controller
    {
        LogicOneDB2Entities1 db = new LogicOneDB2Entities1();
        // GET: Herramienta
        public ActionResult IndexHerramientas()
        {
            return View();
        }

        public JsonResult VerHerramientaList()
        {
            List<Herramientss> P = db.Herramientas.Where(x => x.ID_Herramientas > 0).Select(x => new Herramientss
            {
                ID_Herramientas = x.ID_Herramientas,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion

            }).ToList();
            return Json(P, JsonRequestBehavior.AllowGet);
        }//Aqui termina
        public JsonResult VerHerramientaListID(int ID_Herramientas)
        {
            Herramienta model = db.Herramientas.Where(x => x.ID_Herramientas == ID_Herramientas).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }//Aqui termina

        public JsonResult SaveData(Herramientss model)
        {
            var result = false;
            try
            {
                if (model.ID_Herramientas > 0)
                {
                    Herramienta p = db.Herramientas.SingleOrDefault(x => x.ID_Herramientas == model.ID_Herramientas);
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Herramienta p = new Herramienta();
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.Herramientas.Add(p);
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
            Herramienta p = db.Herramientas.SingleOrDefault(x => x.ID_Herramientas == id);
            if (p != null)
            {
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult VerHerramientas()
        {
            return View(GetAllHerramientas());
        }
        IEnumerable<Herramienta> GetAllHerramientas()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Herramientas.ToList<Herramienta>();
            }
        }


        public ActionResult AgregarOrEditarHerramienta(int id = 0)
        {
            Herramienta herramienta = new Herramienta();
            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    herramienta = db.Herramientas.Where(x => x.ID_Herramientas == id).FirstOrDefault<Herramienta>();
                }
            }
            return View(herramienta);
        }

        [HttpPost]
        public ActionResult AgregarOrEditarHerramienta(Herramienta herramienta)
        {

            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    if (herramienta.ID_Herramientas == 0)
                    {
                        db.Herramientas.Add(herramienta);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(herramienta).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerHerramientas", GetAllHerramientas()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteHerramienta(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Herramienta herramienta = db.Herramientas.Where(x => x.ID_Herramientas == id).FirstOrDefault<Herramienta>();
                    db.Herramientas.Remove(herramienta);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerHerramientas", GetAllHerramientas()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}