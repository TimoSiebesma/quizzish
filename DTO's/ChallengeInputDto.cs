using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class ChallengeInputDto
    {
        [MaxLength(2, ErrorMessage = "There are more than 2 playerSections"), MinLength(2, ErrorMessage = "There are less than 2 playerSections")]
        public virtual IEnumerable<PlayerSectionInputDto> PlayerSections { get; set; } = new PlayerSectionInputDto[2] {new PlayerSectionInputDto(), new PlayerSectionInputDto() };
        [Required]
        public string Difficulty { get; set; }
        [Required]
        public virtual Category Category { get; set; }
    }
}
