using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEORISWebAPICore.Data.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public int IdTipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; } = null!;
        public virtual TipoCuenta? IdTipoCuentaNavigation { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
        [NotMapped]
        public string? NTipoCuenta { get; internal set; }
        [NotMapped]
        public string? NombreCliente { get; internal set; }
    }
}
