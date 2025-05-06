using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICourtService
    {
        List<CourtResponseDto> GetAll();
        CourtResponseDto? GetById(int id);
        void CreateCourt(CourtCreateRequest request);
        void UpdateCourt(CourtCreateRequest request, int courtId);
        void DeleteCourt(int courtId);
    }
}