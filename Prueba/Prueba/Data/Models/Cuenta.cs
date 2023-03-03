using System;
using System.Collections.Generic;

namespace Prueba.Data.Models
{
    public partial class Cuenta
    {
        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public int IdTipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}
