namespace NEORISWebAPICore.Entity
{
    public class EntityMovimiento
    {
        public string MFecha { get; set; } = null!;
        public string MCliente { get; set; } = null!;
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string MMovimiento { get; set; } = null!;
        public int? SaldoDisponible { get; set; }
    }
}
