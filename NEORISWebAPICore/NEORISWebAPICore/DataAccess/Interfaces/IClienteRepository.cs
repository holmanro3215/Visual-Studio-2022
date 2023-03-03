using NEORISWebAPICore.Data.Models;

namespace NEORISWebAPICore.DataAccess.Interfaces
{
    public interface IClienteRepository
    {
        dynamic GetClientes();
        dynamic GetClienteById(int id);
        dynamic CreateClienteAsync(Cliente cliente);
        dynamic UpdateClienteAsync(Cliente cliente);
        dynamic DeleteClienteAsync(Cliente cliente);
    }
}
