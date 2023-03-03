using System;
using System.Collections.Generic;

namespace Prueba.Data.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
