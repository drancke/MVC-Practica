using Loguate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loguate.Controllers;
namespace Loguate.Controllers
{
    [Authorize]
    public class ConsultorController : Controller
    {
        // GET: Consultor
        [Authorize]
        public ActionResult IndexConsultor()
        {
            return View();
        }
        [Authorize]
        public ActionResult VerConsultor()
        {
            return View(GetAllConsultor());
        }
        IEnumerable<Consultor> GetAllConsultor()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Consultors.ToList<Consultor>();
            }
        }

        IEnumerable<roll> GetAllRol()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.rolls.ToList<roll>();
            }
        }

        IEnumerable<AreaConsultor> GetAllAreaModulos()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.AreaConsultors.ToList<AreaConsultor>();
            }
        }


        IEnumerable<Estatu> GetAllAreaEstatus()
        {
            using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
            {
                return db.Estatus.ToList<Estatu>();
            }
        }



        public ActionResult AgregarOrEditarConsultores(int id = 0)
        {


            Consultor consultor = new Consultor();

            consultor.rolls = GetAllRol();
            consultor.Areas = GetAllAreaModulos();
            consultor.estat = GetAllAreaEstatus();



            if (id != 0)
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    consultor = db.Consultors.Where(x => x.ID_Consultor == id).FirstOrDefault<Consultor>();
                }




                consultor.rolls = GetAllRol();
                consultor.Areas = GetAllAreaModulos();
                consultor.estat = GetAllAreaEstatus();
            }

            return View(consultor);
        }
        // Fin metodo AgregarOrEditarConsultores

        [HttpPost]
        public ActionResult AgregarOrEditarConsultores(Consultor consultor)
        {
            try
            {


                if (consultor.archivo != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(consultor.ImageUpload.FileName);
                    string extension = Path.GetExtension(consultor.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    consultor.archivo = "~/AppFiles/Images/" + fileName;
                    consultor.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }

               

                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    if (consultor.ID_Consultor == 0)
                    {
                        db.Consultors.Add(consultor);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(consultor).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerConsultor", GetAllConsultor()), message = "Registro almacenado exitosamente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }//Fin mettodo AgregarOrEditarConsultores


        public ActionResult DeleteConsultor(int id)
        {

            try
            {
                using (LogicOneDB2Entities1 db = new LogicOneDB2Entities1())
                {
                    Consultor consultor = db.Consultors.Where(x => x.ID_Consultor == id).FirstOrDefault<Consultor>();
                    db.Consultors.Remove(consultor);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VerConsultor", GetAllConsultor()), message = "Registro borrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        } //Fin del metodo DeleteConsultor   



    }
}