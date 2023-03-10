using System;
using System.Collections.Generic;

namespace NEORISWebAPICore.Data.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
