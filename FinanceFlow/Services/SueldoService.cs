using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;
using FinanceFlow.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FinanceFlow.Services
{
    public class SueldoService : ISueldoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public SueldoService(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            this._context = context;
            this._memoryCache = memoryCache;
        }

        public SueldoResponseDto ActualizarSueldo(SueldoRequestDto request, long idUsuario)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                SueldoHistorico nuevoSueldo = SueldoMapper.toSueldoModel(request);
                nuevoSueldo.UsuarioId = idUsuario;

                SueldoHistorico? ultimo = _context.SueldosHistoricos
                    .Where(w => w.UsuarioId == nuevoSueldo.UsuarioId)
                    .OrderByDescending(o => o.FechaAsignacion)
                    .FirstOrDefault();

                if (ultimo != null)
                {
                    ultimo.Activo = false;
                    _context.SueldosHistoricos.Update(ultimo);
                }

                _context.SueldosHistoricos.Add(nuevoSueldo);
                _context.SaveChanges();

                transaction.Commit();

                string cacheKey = $"sueldos_{idUsuario}";
                _memoryCache.Remove(cacheKey);

                return SueldoMapper.toDTO(nuevoSueldo);
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            
        }

        public List<SueldoResponseDto> BuscarSueldoPorId(long idUsuario)
        {
            string cacheKey = $"sueldos_{idUsuario}";

            if(_memoryCache.TryGetValue(cacheKey, out List<SueldoResponseDto> sueldos))
            {
                return sueldos;
            }

            var listSueldos = _context.SueldosHistoricos.Where(w => w.UsuarioId == idUsuario).ToList();
            return SueldoMapper.toListDTO(listSueldos);
        }
    }
}
