using System;
using System.Collections.Generic;

namespace NEORISWebAPICore.Data.Models
{
    public partial class TipoMovimiento
    {
        public TipoMovimiento()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdTipoMovimiento { get; set; }
        public string TipoMovimiento1 { get; set; } = null!;

        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
