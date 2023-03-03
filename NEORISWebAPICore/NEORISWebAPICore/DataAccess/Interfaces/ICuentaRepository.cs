using NEORISWebAPICore.Data.Models;

namespace NEORISWebAPICore.DataAccess.Interfaces
{
    public interface ICuentaRepository
    {
        dynamic GetCuentas();
        dynamic GetCuentaById(int id);
        dynamic CreateCuentaAsync(Cuenta cuenta);
        dynamic UpdateCuentaAsync(Cuenta cuenta);
        dynamic DeleteCuentaAsync(Cuenta cuenta);
    }
}
