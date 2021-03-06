//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace Loguate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    public partial class Consultor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consultor()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
            archivo = "~/AppFiles/Images/default.png";
        }

        public long ID_Consultor { get; set; }
        public string Nombre { get; set; }
        public string Role { get; set; }
        [DisplayName("Modulo")]
        public string AreaModulo { get; set; }
        public string Estatus { get; set; }
        [DisplayName("Fecha estatus")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaEstatus { get; set; }
        [DisplayName("Imagen")]
        public string archivo { get; set; }



        public IEnumerable<AreaConsultor> Areas { get; set; }
        public IEnumerable<roll> rolls { get; set; }
        public IEnumerable<Estatu> estat { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
