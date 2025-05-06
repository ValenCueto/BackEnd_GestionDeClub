using Domain.Entities;

namespace Application.Models.Response
{
    public class CourtResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public static CourtResponseDto Create(Court court)
        {
            return new CourtResponseDto
            {
                Id = court.Id,
                Name = court.Name,
                Description = court.Description
            };
        }
    }
}