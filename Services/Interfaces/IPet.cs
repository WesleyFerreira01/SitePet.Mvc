using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitePet.Mvc.Services.Interfaces
{
    public interface IPet<T> : IDisposable
    {
        Task<List<T>> MostrarTodos();

        Task<T> MostrarPorId(int id);

        Task Adicionar(T entity);

        Task<List<T>> MostrarGatos();

        Task<List<T>> MostrarCaes();

        Task Atualizar(T entity);

        Task Remover(int id);

        Task<int> SaveChanges();
    }
}
