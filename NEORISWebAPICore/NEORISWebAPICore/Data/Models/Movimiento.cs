using System;
using System.Collections.Generic;

namespace NEORISWebAPICore.Data.Models
{
    public partial class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoMovimiento { get; set; }
        public int Valor { get; set; }
        public int? Saldo { get; set; }

        public virtual Cuenta? IdCuentaNavigation { get; set; } = null!;
        public virtual TipoMovimiento? IdTipoMovimientoNavigation { get; set; } = null!;
    }
}
