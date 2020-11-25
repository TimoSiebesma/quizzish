using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class PlayerInputDto
    {
        [Required]
        [MaxLength(11)]
        public string UserName { get; set; }

        [Required]
        public string IdentityId { get; set; }
        public IEnumerable<PlayerSectionInputDto> PlayerSections { get; set; }
        public IEnumerable<ScoreInputDto> Scores { get; set; }
    }
}
