using System;
using System.Collections.Generic;

namespace NEORISWebAPICore.Data.Models
{
    public partial class TipoCuenta
    {
        public TipoCuenta()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdTipoCuenta { get; set; }
        public string TipoCuenta1 { get; set; } = null!;

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
