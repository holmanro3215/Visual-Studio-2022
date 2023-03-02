using System;
using System.Collections.Generic;

namespace NEORISWebAPICore.Data.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Personas = new HashSet<Persona>();
        }

        public int IdGenero { get; set; }
        public string Genero1 { get; set; } = null!;

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
