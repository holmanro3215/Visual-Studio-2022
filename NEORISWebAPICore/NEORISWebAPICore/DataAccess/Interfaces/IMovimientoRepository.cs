using NEORISWebAPICore.Data.Models;

namespace NEORISWebAPICore.DataAccess.Interfaces
{
    public interface IMovimientoRepository
    {
        dynamic GetMovimientos();
        dynamic GetMovByUsuario(string numero);
        dynamic CreateMovimientoAsync(Movimiento movimiento);
        dynamic GetMovimientoFechas(string fechaInicio, string fechaFin);
    }
}
