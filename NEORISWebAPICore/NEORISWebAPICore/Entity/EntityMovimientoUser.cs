namespace NEORISWebAPICore.Entity
{
    public class EntityMovimientoUser
    {

        public int IdMovimiento { get; set; }
        public int IdCuenta { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public int IdTipoMovimiento { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public string MMovimiento { get; set; } = null!;
        public int Valor { get; set; }
        public int Saldo { get; set; }
    }
}
