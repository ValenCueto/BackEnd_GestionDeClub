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

        public void CreateCourt(CourtCreateRequest request)
        {
            var court = new Court
            {
                Name = request.Name,
                Description = request.Description
            };

            _courtRepository.Add(court);
        }

        public void UpdateCourt(CourtCreateRequest request, int courtId)
        {
            var courtToUpdate = _courtRepository.GetById(courtId) ??
                throw new NotFoundException("Court", courtId);

            courtToUpdate.Name = request.Name;
            courtToUpdate.Description = request.Description;

            _courtRepository.Update(courtToUpdate);
        }

        public void DeleteCourt(int courtId)
        {
            var courtToDelete = _courtRepository.GetById(courtId) ??
                throw new NotFoundException("Court", courtId);

            _courtRepository.Delete(courtToDelete);
        }
    }
}