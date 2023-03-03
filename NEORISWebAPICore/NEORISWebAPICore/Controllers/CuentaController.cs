using Microsoft.AspNetCore.Mvc;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.DataAccess.Servicios;

namespace NEORISWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private ICuentaRepository _cuentaRepository;

        public CuentaController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetCuentasAsync))]
        public dynamic GetCuentasAsync()
        {
            var getCuentas = _cuentaRepository.GetCuentas();

            return getCuentas;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetCuentaById))]
        public dynamic GetCuentaById(int id)
        {
            var cuentaByID = _cuentaRepository.GetCuentaById(id);
            
            return cuentaByID;
        }

        [HttpPost]
        [ActionName(nameof(CreateCuentaAsync))]
        public dynamic CreateCuentaAsync(Cuenta cuenta)
        {
            var cuentaGuardado = _cuentaRepository.CreateCuentaAsync(cuenta);
            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.IdCuenta }, cuentaGuardado);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateCuenta))]
        public dynamic UpdateCuenta(int id, Cuenta cuenta)
        {
            if (id != cuenta.IdCuenta)
            {
                return BadRequest();
            }

            var cuentaActualizado = _cuentaRepository.UpdateCuentaAsync(cuenta);

            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.IdCuenta }, cuentaActualizado);

        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteCuenta))]
        public dynamic DeleteCuenta(int id)
        {
            var cuenta = _cuentaRepository.GetCuentaById(id);

            var cuentaEliminado = _cuentaRepository.DeleteCuentaAsync(cuenta.result);

            return cuentaEliminado;
        }
    }
}
