using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class PlayerOutputDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string IdentityId { get; set; }
        public IEnumerable<PlayerSectionOutputDto> PlayerSections { get; set; }
        public IEnumerable<ScoreOutputDto> Scores { get; set; }
    }
}
