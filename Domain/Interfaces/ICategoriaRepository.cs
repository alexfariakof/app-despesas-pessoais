using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Categoria Get(int id);
        List<Categoria> GetAll();
        Categoria Insert(Categoria categoria);
        Categoria Update(Categoria categoria);
        void Delete(int id);
    }
}
