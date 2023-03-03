using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;

namespace NEORISWebAPICore.DataAccess.Servicios
{
    public class CuentaRepository : ICuentaRepository
    {
        protected readonly BancoNEORISContext _context;
        public CuentaRepository(BancoNEORISContext context) => _context = context;

        public dynamic GetCuentas()
        {
            try
            {
                List<Cuenta> objC = _context.Cuentas.ToList();
                
                foreach (Cuenta cuenta in objC)
                {
                    if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == cuenta.IdTipoCuenta).Count() > 0)
                    {
                        cuenta.NTipoCuenta = _context.TipoCuentas.FirstOrDefault(o => o.IdTipoCuenta == cuenta.IdTipoCuenta).TipoCuenta1;
                    }

                    if (_context.Clientes.Where(x => x.IdCliente == cuenta.IdCliente).Count() > 0)
                    {
                        var idPersona = _context.Clientes.FirstOrDefault(x => x.IdCliente == cuenta.IdCliente).IdPersona;

                        if (_context.Personas.Where(x => x.IdPersona == idPersona).Count() > 0)
                        {
                            cuenta.NombreCliente = _context.Personas.FirstOrDefault(x => x.IdPersona == idPersona).Nombre;
                        }
                    }

                    cuenta.IdClienteNavigation = null;
                    cuenta.IdTipoCuentaNavigation = null;
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = objC
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

        public dynamic GetCuentaById(int id)
        {
            try
            {
                Cuenta objC = _context.Cuentas.Find(id);

                if (objC != null)
                {
                    if (_context.TipoCuentas.Where(o => o.IdTipoCuenta == objC.IdTipoCuenta).Count() > 0)
                    {
                        objC.NTipoCuenta = _context.TipoCuentas.FirstOrDefault(o => o.IdTipoCuenta == objC.IdTipoCuenta).TipoCuenta1;
                    }

                    if (_context.Clientes.Where(x => x.IdCliente == objC.IdCliente).Count() > 0)
                    {
                        var idPersona = _context.Clientes.FirstOrDefault(x => x.IdCliente == objC.IdCliente).IdPersona;

                        if (_context.Personas.Where(x => x.IdPersona == idPersona).Count() > 0)
                        {
                            objC.NombreCliente = _context.Personas.FirstOrDefault(x => x.IdPersona == idPersona).Nombre;
                        }
                    }

                    objC.IdClienteNavigation = null;
                    objC.IdTipoCuentaNavigation = null;

                    return new
                    {
                        success = true,
                        message = "Consulta exitosa",
                        result = objC
                    };
                }
                else
                {
                    return new
                    {
                        success = false,
                        message = "Error: El registro no existe",
                        result = ""
                    };
                }

            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error cargando el registro: " + ex.Message,
                    result = ""
                };
            }
        }

        public dynamic CreateCuentaAsync(Cuenta cuenta)
        {
            try
            {
                _context.Set<Cuenta>().AddAsync(cuenta);
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

        public dynamic UpdateCuentaAsync(Cuenta cuenta)
        {
            try
            {
                _context.Entry(cuenta).State = EntityState.Modified;
                _context.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Registro Actualizado"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error actualizando el registro: " + ex.Message,
                };
            }
        }

        public dynamic DeleteCuentaAsync(Cuenta cuenta)
        {
            try
            {
                if (cuenta is null)
                {
                    return new
                    {
                        success = false,
                        message = "Error: La cuenta que intenta eliminar no existe"
                    };
                }
                _context.Set<Cuenta>().Remove(cuenta);
                _context.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Registro Eliminado"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error eliminando el registro: " + ex.Message
                };
            }
        }
    }
}
