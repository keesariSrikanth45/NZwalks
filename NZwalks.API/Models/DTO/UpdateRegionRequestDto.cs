using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {


        [Required]
        [MinLength(3, ErrorMessage = "Code should have minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should have maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name can't exceed 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

