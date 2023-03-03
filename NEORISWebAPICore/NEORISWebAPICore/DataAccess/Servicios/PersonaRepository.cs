using Microsoft.EntityFrameworkCore;
using NEORISWebAPICore.Data.Context;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;

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
                List<Genero> objG = _context.Generos.ToList();

                foreach (Persona persona in objP)
                {
                    if (objG.Where(o => o.IdGenero == persona.IdGenero).Count() > 0)
                    {
                        persona.GeneroPersona = objG.FirstOrDefault(o => o.IdGenero == persona.IdGenero).Genero1;
                    }
                    persona.IdGeneroNavigation = null;
                }

                return new
                {
                    success = true,
                    message = "Consulta exitosa",
                    result = objP
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
                List<Genero> objG = _context.Generos.ToList();

                if (objP != null)
                {
                    if (objG.Where(o => o.IdGenero == objP.IdGenero).Count() > 0)
                    {
                        objP.GeneroPersona = objG.FirstOrDefault(o => o.IdGenero == objP.IdGenero).Genero1;
                    }
                    objP.IdGeneroNavigation = null;

                    return new
                    {
                        success = true,
                        message = "Consulta exitosa",
                        result = objP
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
