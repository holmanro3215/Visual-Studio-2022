using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEORISWebAPICore.Data.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; } = null!;
        [NotMapped]
        public string? NombrePersona { get; internal set; }
        [NotMapped]
        public string? DireccionPersona { get; internal set; }
        [NotMapped]
        public string? TelefonoPersona { get; internal set; }
    }
}
