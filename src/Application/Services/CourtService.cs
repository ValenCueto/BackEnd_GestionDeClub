using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;

        public CourtService(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public List<CourtResponseDto> GetAll()
        {
            var courts = _courtRepository.GetAll();
            return courts.Select(court => CourtResponseDto.Create(court)).ToList();
        }

        public CourtResponseDto? GetById(int id)
        {
            var court = _courtRepository.GetById(id);
            return court != null ? CourtResponseDto.Create(court) : null;
        }

        public void CreateCourt()
        {
            var court = new Court();
            _courtRepository.Add(court);
        }

        public void DeleteCourt(int courtId)
        {
            var courtToDelete = _courtRepository.GetById(courtId) ?? throw new NotFoundException("Court", courtId);
            _courtRepository.Delete(courtToDelete);
        }
    }
}