using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class ChallengeOutputDto
    {
        public int Id { get; set; }
        public IEnumerable<QuestionOutputDto> Questions { get; set; }
        public IEnumerable<PlayerSectionOutputDto> PlayerSections { get; set; }

        public string Difficulty { get; set; }
        
        public Category Category { get; set; }
    }
}
