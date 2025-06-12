using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class CourtCreateRequest
    {

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}