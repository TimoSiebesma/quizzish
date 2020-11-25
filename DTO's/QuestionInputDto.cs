using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class QuestionInputDto
    {
        [Required]
        public string Text { get; set; }

        [MinLength(4), MaxLength(4)]
        public IEnumerable<AnswerInputDto> Answers { get; set; }

        [Required]
        public string Difficulty { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
