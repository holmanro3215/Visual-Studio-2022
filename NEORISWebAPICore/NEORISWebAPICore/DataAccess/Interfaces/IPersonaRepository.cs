using NEORISWebAPICore.Data.Models;

namespace NEORISWebAPICore.DataAccess.Interfaces
{
    public interface IPersonaRepository
    {
        dynamic GetPersonas();
        dynamic GetPersonaById(int id);
        dynamic CreatePersonaAsync(Persona persona);
        dynamic UpdatePersonaAsync(Persona persona);
        dynamic DeletePersonaAsync(Persona persona);
    }
}