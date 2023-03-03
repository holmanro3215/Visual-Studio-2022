using Microsoft.AspNetCore.Mvc;
using NEORISWebAPICore.Data.Models;
using NEORISWebAPICore.DataAccess.Interfaces;
using NEORISWebAPICore.DataAccess.Servicios;

namespace NEORISWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetPersonasAsync))]
        public dynamic GetPersonasAsync()
        {
            var getPersonas = _personaRepository.GetPersonas();

            return getPersonas;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetPersonaById))]
        public dynamic GetPersonaById(int id)
        {
            var personaByID = _personaRepository.GetPersonaById(id);
            
            return personaByID;
        }

        [HttpPost]
        [ActionName(nameof(CreatePersonaAsync))]
        public dynamic CreatePersonaAsync(Persona persona)
        {
            var personaGuardado = _personaRepository.CreatePersonaAsync(persona);
            return CreatedAtAction(nameof(GetPersonaById), new { id = persona.IdPersona }, personaGuardado);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdatePersona))]
        public dynamic UpdatePersona(int id, Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return BadRequest();
            }

            var personaActualizado = _personaRepository.UpdatePersonaAsync(persona);

            return CreatedAtAction(nameof(GetPersonaById), new { id = persona.IdPersona }, personaActualizado);

        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeletePersona))]
        public dynamic DeletePersona(int id)
        {
            var persona = _personaRepository.GetPersonaById(id);

            var personaEliminado = _personaRepository.DeletePersonaAsync(persona.result);

            return personaEliminado;
        }
    }
}
