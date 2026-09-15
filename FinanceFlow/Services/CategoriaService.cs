using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;
using FinanceFlow.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FinanceFlow.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public CategoriaService(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            this._context = context;
            this._memoryCache = memoryCache;
        }
        public List<CategoriaDto> GetCategorias()
        {
            string cacheKey = "categorias";

            if (_memoryCache.TryGetValue(cacheKey, out List<Categoria> cachedCategorias))
            {
                return CategoriaMapper.toListDto(cachedCategorias);
            }

            var categorias = _context.Categorias.ToList();

            // Guardar en cache con expiración
            _memoryCache.Set(cacheKey, categorias, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            return CategoriaMapper.toListDto(categorias);
        }
    }
}
