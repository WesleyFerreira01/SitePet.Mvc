using Microsoft.EntityFrameworkCore;
using SitePet.Mvc.Data;
using SitePet.Mvc.Models;
using SitePet.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SitePet.Mvc.Services.Repository
{
    public class PetRepository : IPet<Pet>, IPetRepository
    {

        protected readonly MeuDbContext Db;



        public PetRepository(MeuDbContext db)
        {
            Db = db;

        }

        public async Task Adicionar(Pet entity)
        {
            Db.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(Pet entity)
        {
            Db.Update(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public async Task<Pet> MostrarPorId(int id)
        {
            return await Db.Pets.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pet>> MostrarTodos()
        {
            return await Db.Pets.AsNoTracking().ToListAsync();
        }

        public async Task<List<Pet>> MostrarGatos()
        {
            return await Db.Pets.Where(p => p.Tipo == Tipo.Gato).AsNoTracking().ToListAsync();
        }

        public async Task<List<Pet>> MostrarCaes()
        {
            return await Db.Pets.Where(p => p.Tipo == Tipo.Cachorro).AsNoTracking().ToListAsync();
        }



        public async Task Remover(int id)
        {
            Db.Remove(new Pet { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
