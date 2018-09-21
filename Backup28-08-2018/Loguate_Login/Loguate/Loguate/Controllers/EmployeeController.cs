using Loguate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using Loguate.Controllers;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loguate.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    { //
        // GET: /Employee/
        //pagina principal ejemplo
        [Authorize]
        public ActionResult IndexPrincipal()
        {
            return View();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
        //public ActionResult ClienteView()
        //{
        //    return View();
        //}

        //public ActionResult HerramientasView()
        //{
        //    return View();
        //}

        //public ActionResult FormProyectoView()
        //{
        //    return View();
        //}

        //public ActionResult FormulariosView()
        //{
        //    return View();
        //}


        //public ActionResult EmpresasView()
        //{
        //    return View();
        //}

        //public ActionResult ProductosView()
        //{
        //    return View();
        //}     

        //public ActionResult ConsultoresView()
        //{

        //    return View();
        //}

        //public ActionResult AreaModuloView()
        //{
        //    return View();
        //}

        //public ActionResult VerClientes()
        //{
        //    return View(GetAllClientes());
        //}


        //public ActionResult VerEmpresas()
        //{
        //    return View(GetAllEmpresas());
        //}


        //public ActionResult VerHerramienta()
        //{
        //    return View(GetAllHerramientas());
        //} 

        //public ActionResult VerProducto()
        //{
        //    return View(GetAllProductos());
        //}

        //public ActionResult VerFormProyecto()
        //{
        //    return View(GetAllFormProyecto());
        //}


        //public ActionResult VerFormularios()
        //{
        //    return View(GetAllFormularios());
        //}


        //IEnumerable<FormProyecto> GetAllFormProyecto()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.FormProyecto.ToList<FormProyecto>();
        //    }
        //}

        //IEnumerable<Formularios> GetAllFormularios ()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Formularios.ToList<Formularios>();
        //    }
        //}

        //public ActionResult VerConsultores()
        //{
        //    return View(GetAllConsultores());
        //}

        //public ActionResult VerAreaModulos()
        //{
        //    return View(GetAllAreaModulos());
        //}


        //public ActionResult ViewAll()
        //{
        //    return View(GetAllEmployee());
        //}

        //IEnumerable<Clientes> GetAllClientes()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Clientes.ToList<Clientes>();
        //    }

        //}

        //IEnumerable<Consultor> GetAllConsultores()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Consultor.ToList<Consultor>();
        //    }
        //}


        


        //IEnumerable<Empresas> GetAllEmpresas()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Empresas.ToList<Empresas>();
        //    }
        //}

        //IEnumerable<Productos> GetAllProductos()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Productos.ToList<Productos>();
        //    }
        //}



        //IEnumerable<Herramientas> GetAllHerramientas()
        //{
        //    using (LogicOneDB db = new LogicOneDB())
        //    {
        //        return db.Herramientas.ToList<Herramientas>();
        //    }
        //}

        //IEnumerable<Employee> GetAllEmployee()
        //{
        //    using (LogicOneDBdb = new LogicOneDB())
        //    {
        //        return db.Employee.ToList<Employee>();
        //    }

        //}
       
       
        //public ActionResult AddOrEdit(int id = 0)
        //{
        //    Employee emp = new Employee();
        //    if (id != 0)
        //    {
        //        using(LogicOneDB db = new LogicOneDB())
        //        {
        //            emp = db.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
        //        }
        //    }
        //    return View(emp);
        //}

       
       






        
        
        





        


      
    
      

        

        