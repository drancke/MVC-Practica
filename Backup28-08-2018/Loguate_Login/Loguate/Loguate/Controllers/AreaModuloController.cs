using Loguate.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Loguate.Controllers
{
    [Authorize]
    public class AreaModuloController : Controller
    {

        LogicOneDB2Entities1 db = new LogicOneDB2Entities1();

        // GET: AreaModulo
        [Authorize]
        public ActionResult IndexArea()
        {
            return View();
        }
        [Authorize]
        public ActionResult VerAreaModulo()
        {
            return View(GetAllAreaModulos());
        }


        IEnumerable<AreaModulo> GetAllAreaModulos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.AreaModuloes.ToList<AreaModulo>();
            }
        }

        [Authorize]
        public ActionResult AgregarOrEditarAreaModulos(int id = 0)
        {
            AreaModulo AreaModulo = new AreaModulo();
            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    AreaModulo = db.AreaModuloes.Where(x => x.ID_AreaModulo == id).FirstOrDefault<AreaModulo>();
                }
            }

            return View(AreaModulo);
        }//Fin metodo AgregarOrEditarModelos


        [HttpPost]
        public ActionResult AgregarOrEditarAreaModulos(AreaModulo AreaModulo)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    if (AreaModulo.ID_AreaModulo == 0)
                    {
                        db.AreaModuloes.Add(AreaModulo);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(AreaModulo).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerAreaModulo", GetAllAreaModulos()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//fin metodo agragarOrEditarAreaModulos usando post.


        public ActionResult DeleteAreaModulos(int id)
        {
            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    AreaModulo AreaModulo = db.AreaModuloes.Where(x => x.ID_AreaModulo == id).FirstOrDefault<AreaModulo>();
                    db.AreaModuloes.Remove(AreaModulo);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerAreaModulo", GetAllAreaModulos()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//Fin metodo DeleteAreaModulos




        [ValidateAntiForgeryToken]
        [Authorize]
        public JsonResult VerAreaList()
        {
            List<Modulo> Mood = db.AreaModuloes.Where(x => x.ID_AreaModulo > 0).Select(x=>new Modulo
            {
                ID_AreaModulo = x.ID_AreaModulo,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion

            }).ToList();

            return Json(Mood, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        public JsonResult VerAreaListID(int ID)
        {
            AreaModulo model = db.AreaModuloes.SingleOrDefault(x => x.ID_AreaModulo == ID);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(AreaModulo model)
        {
            var result = false;
            try
            {
                if (model.ID_AreaModulo > 0)
                {
                    AreaModulo p = db.AreaModuloes.SingleOrDefault(x => x.ID_AreaModulo == model.ID_AreaModulo);
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    AreaModulo p = new AreaModulo();
                    p.Nombre = model.Nombre;
                    p.Descripcion = model.Descripcion;
                    db.AreaModuloes.Add(p);
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





    }
}