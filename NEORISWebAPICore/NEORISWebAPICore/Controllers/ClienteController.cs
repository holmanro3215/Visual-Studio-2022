using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;

namespace NEORISWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetClientesAsync))]
        public dynamic GetClientesAsync()
        {
            var getClientes = _clienteRepository.GetClientes();

            return getClientes;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetClienteById))]
        public dynamic GetClienteById(int id)
        {
            var clienteByID = _clienteRepository.GetClienteById(id);
            
            return clienteByID;
        }

        [HttpPost]
        [ActionName(nameof(CreateClienteAsync))]
        public dynamic CreateClienteAsync(Cliente cliente)
        {
            var clienteGuardado = _clienteRepository.CreateClienteAsync(cliente);
            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.IdCliente }, clienteGuardado);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateCliente))]
        public dynamic UpdateCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            var clienteActualizado = _clienteRepository.UpdateClienteAsync(cliente);

            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.IdCliente }, clienteActualizado);

        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteCliente))]
        public dynamic DeleteCliente(int id)
        {
            var cliente = _clienteRepository.GetClienteById(id);
            
            var clienteEliminado = _clienteRepository.DeleteClienteAsync(cliente.result);

            return clienteEliminado;
        }
    }
}
