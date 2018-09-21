using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loguate.Models
{
    public class Productss
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public void Producto()
        {
            this.Clientes = new HashSet<Cliente>();
            this.Empresas = new HashSet<Empresa>();
        }

        public long ID_Producto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}