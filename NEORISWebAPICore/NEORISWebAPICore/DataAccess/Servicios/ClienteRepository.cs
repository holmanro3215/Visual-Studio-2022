﻿using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;

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
                List<Persona> objP = _context.Personas.ToList();

                foreach (Cliente cliente in objC)
                {
                    if (objP.Where(o => o.IdPersona == cliente.IdPersona).Count() > 0)
                    {
                        cliente.NombrePersona = objP.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Nombre;
                        cliente.DireccionPersona = objP.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Direccion;
                        cliente.TelefonoPersona = objP.FirstOrDefault(o => o.IdPersona == cliente.IdPersona).Telefono;
                    }
                    cliente.IdPersonaNavigation = null;
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

        public dynamic GetClienteById(int id)
        {
            try
            {
                Cliente objC = _context.Clientes.Find(id);
                List<Persona> objP = _context.Personas.ToList();

                if (objP.Where(o => o.IdPersona == objC.IdPersona).Count() > 0)
                {
                    objC.NombrePersona = objP.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Nombre;
                    objC.DireccionPersona = objP.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Direccion;
                    objC.TelefonoPersona = objP.FirstOrDefault(o => o.IdPersona == objC.IdPersona).Telefono;
                }
                objC.IdPersonaNavigation = null;

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