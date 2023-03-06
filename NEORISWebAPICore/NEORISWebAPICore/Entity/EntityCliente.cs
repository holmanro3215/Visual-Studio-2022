namespace NEORISWebAPICore.Entity
{
    public class EntityCliente
    {
        public string Nombres { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
