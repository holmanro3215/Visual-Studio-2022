namespace NEORISWebAPICore.Entity
{
    public class EntityCuenta
    {
        public string NumeroCuenta { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Cliente { get; set; } = null!;
    }
}
