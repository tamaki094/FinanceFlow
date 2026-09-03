using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;
using FinanceFlow.Models;

namespace FinanceFlow.Services
{
    public class SueldoService : ISueldoService
    {
        private readonly ApplicationDbContext _context;

        public SueldoService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public SueldoResponseDto ActualizarSueldo(SueldoRequestDto request, long idUsuario)
        {
            //no se castean bien las fechas y bien las de default al llegar aqui
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

            return SueldoMapper.toDTO(nuevoSueldo);
        }

        public List<SueldoResponseDto> BuscarSueldoPorId(long id)
        {
            var listSueldos = _context.SueldosHistoricos.Where(w => w.UsuarioId == id).ToList();
            return SueldoMapper.toListDTO(listSueldos);
        }
    }
}
