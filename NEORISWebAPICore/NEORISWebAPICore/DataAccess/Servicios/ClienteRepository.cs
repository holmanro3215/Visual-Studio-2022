using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.Entity;

namespace NEORISWebAPICore.DataAccess.Servicios
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly BancoNEORISContext _context;
        public ClienteRepository(BancoNEORISContext context) => _context = context;

        public dynamic GetClientes()
        {
            try
            {
                List<Cliente> objC = _context.Clientes.ToList();
                List<EntityCliente> ECliente = new List<EntityCliente>();

                foreach (Cliente cliente in objC)
                {
                    EntityCliente entityCliente = new EntityCliente();

                    if (_context.Personas.Where(o => o.IdPersona == cliente.IdPersona).Count() > 0)
                    {
                        entityCliente.Nombres = _context.Personas.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Nombre;
                        entityCliente.Direccion = _context.Personas.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Direccion;
                        entityCliente.Telefono = _context.Personas.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Telefono;
                    }

                    entityCliente.Contrasena = cliente.Contrasena;
                    entityCliente.Estado = cliente.Estado;

                    ECliente.Add(entityCliente);
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = ECliente
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

        public dynamic GetClienteById(int id)
        {
            try
            {
                Cliente objC = _context.Clientes.Find(id);
                EntityCliente entityCliente = new EntityCliente();

                if (objC != null)
                {
                    if (_context.Personas.Where(o => o.IdPersona == objC.IdPersona).Count() > 0)
                    {
                        entityCliente.Nombres = _context.Personas.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Nombre;
                        entityCliente.Direccion = _context.Personas.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Direccion;
                        entityCliente.Telefono = _context.Personas.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Telefono;
                    }

                    entityCliente.Contrasena = objC.Contrasena;
                    entityCliente.Estado = objC.Estado;

                    return new
                    {
                        success = true,
                        message = "Consulta exitosa",
                        result = entityCliente
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
        
        public dynamic CreateClienteAsync(Cliente cliente)
        {
            try
            {
                _context.Set<Cliente>().AddAsync(cliente);
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

        public dynamic UpdateClienteAsync(Cliente cliente)
        {
            try
            {
                _context.Entry(cliente).State = EntityState.Modified;
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

        public dynamic DeleteClienteAsync(Cliente cliente)
        {
            try
            {
                if (cliente is null)
                {
                    return new
                    {
                        success = false,
                        message = "Error: El cliente que intenta eliminar no existe"
                    };
                }
                _context.Set<Cliente>().Remove(cliente);
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
