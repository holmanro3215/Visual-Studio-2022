using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.Entity;

namespace NEORISWebAPICore.DataAccess.Servicios
{
    public class MovimientoRepository : IMovimientoRepository
    {
        protected readonly BancoNEORISContext _context;
        public MovimientoRepository(BancoNEORISContext context) => _context = context;

        public dynamic GetMovimientos()
        {
            try
            {
                List<Movimiento> objM = _context.Movimientos.ToList();
                List<EntityMovimiento> EMovimiento = new List<EntityMovimiento>();
                var tipoMovimiento = "";
                var estadoMovimiento = "";

                foreach (Movimiento movimiento in objM)
                {
                    EntityMovimiento entityMovimiento = new EntityMovimiento();

                    if (_context.Cuentas.Where(o => o.IdCuenta == movimiento.IdCuenta).Count() > 0)
                    {
                        entityMovimiento.NumeroCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).NumeroCuenta;
                        var idTipoCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdTipoCuenta;
                        var idCliente = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdCliente;
                        entityMovimiento.SaldoInicial = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).SaldoInicial;
                        entityMovimiento.Estado = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).Estado;

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            entityMovimiento.TipoCuenta = _context.TipoCuentas.FirstOrDefault(o => o.IdTipoCuenta == idTipoCuenta).TipoCuenta1;
                        }

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            var idPersona = _context.Clientes.FirstOrDefault(o => o.IdCliente == idCliente).IdPersona;

                            entityMovimiento.MCliente = _context.Personas.FirstOrDefault(o => o.IdPersona == idPersona).Nombre;
                        }
                    }

                    if (_context.TipoMovimientos.Where(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).Count() > 0)
                    {
                        tipoMovimiento = _context.TipoMovimientos.FirstOrDefault(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).TipoMovimiento1;
                    }

                    entityMovimiento.MFecha = movimiento.Fecha.ToString("dd/MM/yyyy");

                    entityMovimiento.SaldoDisponible = movimiento.Saldo;
                    estadoMovimiento = tipoMovimiento + " de " + movimiento.Valor;

                    entityMovimiento.MMovimiento = estadoMovimiento;
                    EMovimiento.Add(entityMovimiento);
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = EMovimiento
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error cargando los registros: " + ex.Message,
                    result = ""
                };
            }
        }

        public dynamic GetMovByUsuario(string numero)
        {
            try
            {
                List<Movimiento> objM = _context.Movimientos.ToList();
                List<EntityMovimiento> EMovimiento = new List<EntityMovimiento>();
                var tipoMovimiento = "";
                var estadoMovimiento = "";

                foreach (Movimiento movimiento in objM)
                {
                    EntityMovimiento entityMovimiento = new EntityMovimiento();
                    var esCliente = false;

                    if (_context.Cuentas.Where(o => o.IdCuenta == movimiento.IdCuenta).Count() > 0)
                    {
                        entityMovimiento.NumeroCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).NumeroCuenta;
                        var idTipoCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdTipoCuenta;
                        var idCliente = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdCliente;
                        entityMovimiento.SaldoInicial = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).SaldoInicial;
                        entityMovimiento.Estado = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).Estado;

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            entityMovimiento.TipoCuenta = _context.TipoCuentas.FirstOrDefault(o => o.IdTipoCuenta == idTipoCuenta).TipoCuenta1;
                        }

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            var idPersona = _context.Clientes.FirstOrDefault(o => o.IdCliente == idCliente).IdPersona;

                            entityMovimiento.MCliente = _context.Personas.FirstOrDefault(o => o.IdPersona == idPersona).Nombre;
                            var numeroId = _context.Personas.FirstOrDefault(o => o.IdPersona == idPersona).Identificacion;

                            if (numeroId == numero)
                            {
                                esCliente = true;
                            }
                        }
                    }

                    if (_context.TipoMovimientos.Where(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).Count() > 0)
                    {
                        tipoMovimiento = _context.TipoMovimientos.FirstOrDefault(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).TipoMovimiento1;
                    }

                    entityMovimiento.MFecha = movimiento.Fecha.ToString("dd/MM/yyyy");

                    entityMovimiento.SaldoDisponible = movimiento.Saldo;
                    estadoMovimiento = tipoMovimiento + " de " + movimiento.Valor;

                    entityMovimiento.MMovimiento = estadoMovimiento;

                    if (esCliente)
                    {
                        EMovimiento.Add(entityMovimiento);
                    }
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = EMovimiento
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error cargando los registros: " + ex.Message,
                    result = ""
                };
            }
        }

        public dynamic CreateMovimientoAsync(Movimiento movimiento)
        {
            try
            {
                var saldoDisponibleC = 0;
                List<Movimiento> saldoDisponibleM = new List<Movimiento>();
                if (_context.Cuentas.Where(x => x.IdCuenta == movimiento.IdCuenta).Count() > 0)
                    saldoDisponibleC = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).SaldoInicial;
                else
                {
                    return new
                    {
                        success = true,
                        message = "La cuenta no existe"
                    };
                }

                if (_context.Cuentas.Where(x => x.IdCuenta == movimiento.IdCuenta).Count() > 0)
                    saldoDisponibleM = _context.Movimientos.Where(o => o.IdCuenta == movimiento.IdCuenta).OrderByDescending(o => o.IdMovimiento).ToList();
                
                if (saldoDisponibleM.Count() > 0)
                {
                    if ((movimiento.Valor > (int)saldoDisponibleM[0].Saldo) && (movimiento.IdTipoMovimiento == 1))
                    {
                        return new
                        {
                            success = true,
                            message = "Saldo no disponible"
                        };
                    }

                    if (movimiento.IdTipoMovimiento == 1)
                        movimiento.Saldo = (int)saldoDisponibleM[0].Saldo - movimiento.Valor;
                    else
                        movimiento.Saldo = (int)saldoDisponibleM[0].Saldo + movimiento.Valor;
                }
                else
                {
                    if ((movimiento.Valor > saldoDisponibleC) && (movimiento.IdTipoMovimiento == 1))
                    {
                        return new
                        {
                            success = true,
                            message = "Saldo no disponible"
                        };
                    }

                    if (movimiento.IdTipoMovimiento == 1)
                        movimiento.Saldo = saldoDisponibleC - movimiento.Valor;
                    else
                        movimiento.Saldo = saldoDisponibleC + movimiento.Valor;
                }

                _context.Set<Movimiento>().AddAsync(movimiento);
                _context.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Registro Guardado"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error creando el registro: " + ex.Message
                };
            }
        }

        public dynamic GetMovimientoFechas(string fechaInicio, string fechaFin)
        {
            try
            {
                List<Movimiento> _Resultado = (from UnMov in _context.Movimientos
                                               where UnMov.Fecha >= Convert.ToDateTime(fechaInicio)
                                               && UnMov.Fecha <= Convert.ToDateTime(fechaFin)
                                               select UnMov).ToList<Movimiento>();

                List<EntityMovimiento> EMovimiento = new List<EntityMovimiento>();
                var tipoMovimiento = "";
                var estadoMovimiento = "";

                foreach (Movimiento movimiento in _Resultado)
                {
                    EntityMovimiento entityMovimiento = new EntityMovimiento();

                    if (_context.Cuentas.Where(o => o.IdCuenta == movimiento.IdCuenta).Count() > 0)
                    {
                        entityMovimiento.NumeroCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).NumeroCuenta;
                        var idTipoCuenta = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdTipoCuenta;
                        var idCliente = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).IdCliente;
                        entityMovimiento.SaldoInicial = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).SaldoInicial;
                        entityMovimiento.Estado = _context.Cuentas.FirstOrDefault(o => o.IdCuenta == movimiento.IdCuenta).Estado;

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            entityMovimiento.TipoCuenta = _context.TipoCuentas.FirstOrDefault(o => o.IdTipoCuenta == idTipoCuenta).TipoCuenta1;
                        }

                        if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == idTipoCuenta).Count() > 0)
                        {
                            var idPersona = _context.Clientes.FirstOrDefault(o => o.IdCliente == idCliente).IdPersona;

                            entityMovimiento.MCliente = _context.Personas.FirstOrDefault(o => o.IdPersona == idPersona).Nombre;
                        }
                    }

                    if (_context.TipoMovimientos.Where(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).Count() > 0)
                    {
                        tipoMovimiento = _context.TipoMovimientos.FirstOrDefault(x => x.IdTipoMovimiento == movimiento.IdTipoMovimiento).TipoMovimiento1;
                    }

                    entityMovimiento.MFecha = movimiento.Fecha.ToString("dd/MM/yyyy");

                    entityMovimiento.SaldoDisponible = movimiento.Saldo;
                    estadoMovimiento = tipoMovimiento + " de " + movimiento.Valor;

                    entityMovimiento.MMovimiento = estadoMovimiento;
                    EMovimiento.Add(entityMovimiento);
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = EMovimiento
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error cargando los registros: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
