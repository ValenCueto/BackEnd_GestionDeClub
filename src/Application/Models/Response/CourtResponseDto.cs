using Domain.Entities;

namespace Application.Models.Response
{
    public class CourtResponseDto
    {
        public int Id { get; set; }

        public static CourtResponseDto Create(Court court)
        {
            return new CourtResponseDto
            {
                Id = court.Id
            };
        }
    }
}