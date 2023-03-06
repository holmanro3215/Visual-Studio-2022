using Microsoft.AspNetCore.Mvc;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.DataAccess.Servicios;

namespace NEORISWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private IMovimientoRepository _movimientoRepository;

        public MovimientoController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetMovimientosAsync))]
        public dynamic GetMovimientosAsync()
        {
            var getMovimientos = _movimientoRepository.GetMovimientos();

            return getMovimientos;
        }

        [HttpGet("{numero}")]
        [ActionName(nameof(GetMovByUsuario))]
        public dynamic GetMovByUsuario(string numero)
        {
            var movimientoByUser = _movimientoRepository.GetMovByUsuario(numero);

            return movimientoByUser;
        }

        [HttpPost]
        [ActionName(nameof(CreateMovimientoAsync))]
        public dynamic CreateMovimientoAsync(Movimiento movimiento)
        {
            var cuentaGuardado = _movimientoRepository.CreateMovimientoAsync(movimiento);
            return cuentaGuardado;
        }

        [HttpGet("{fechaInicio},{fechaFin}")]
        [ActionName(nameof(GetMovimientoFechas))]
        public dynamic GetMovimientoFechas(string fechaInicio, string fechaFin)
        {
            var getMovimientos = _movimientoRepository.GetMovimientoFechas(fechaInicio, fechaFin);

            return getMovimientos;
        }
    }
}
