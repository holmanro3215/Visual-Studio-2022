using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.Entity;

namespace NEORISWebAPICore.DataAccess.Servicios
{
    public class PersonaRepository : IPersonaRepository
    {
        protected readonly BancoNEORISContext _context;
        public PersonaRepository(BancoNEORISContext context) => _context = context;

        public dynamic GetPersonas()
        {
            try
            {
                List<Persona> objP = _context.Personas.ToList();
                List<EntityPersona> EPersona = new List<EntityPersona>();

                foreach (Persona persona in objP)
                {
                    EntityPersona entityPersona = new EntityPersona();

                    if (_context.Generos.Where(o => o.IdGenero == persona.IdGenero).Count() > 0)
                    {
                        entityPersona.Genero = _context.Generos.FirstOrDefault(o => o.IdGenero == persona.IdGenero).Genero1;
                    }

                    entityPersona.Nombre = persona.Nombre;
                    entityPersona.Edad = persona.Edad;
                    entityPersona.Identificacion = persona.Identificacion;
                    entityPersona.Direccion = persona.Direccion;
                    entityPersona.Telefono = persona.Telefono;

                    EPersona.Add(entityPersona);
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = EPersona
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

        public dynamic GetPersonaById(int id)
        {
            try
            {
                Persona objP = _context.Personas.Find(id);
                EntityPersona entityPersona = new EntityPersona();

                if (objP != null)
                {
                    if (_context.Generos.Where(o => o.IdGenero == objP.IdGenero).Count() > 0)
                    {
                        entityPersona.Genero = _context.Generos.FirstOrDefault(o => o.IdGenero == objP.IdGenero).Genero1;
                    }

                    entityPersona.Nombre = objP.Nombre;
                    entityPersona.Edad = objP.Edad;
                    entityPersona.Identificacion = objP.Identificacion;
                    entityPersona.Direccion = objP.Direccion;
                    entityPersona.Telefono = objP.Telefono;

                    return new
                    {
                        success = true,
                        message = "Consulta exitosa",
                        result = entityPersona
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

        public dynamic CreatePersonaAsync(Persona persona)
        {
            try
            {
                _context.Set<Persona>().AddAsync(persona);
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

        public dynamic UpdatePersonaAsync(Persona persona)
        {
            try
            {
                _context.Entry(persona).State = EntityState.Modified;
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

        public dynamic DeletePersonaAsync(Persona persona)
        {
            try
            {
                if (persona is null)
                {
                    return new
                    {
                        success = false,
                        message = "Error: El cliente que intenta eliminar no existe"
                    };
                }
                _context.Set<Persona>().Remove(persona);
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
