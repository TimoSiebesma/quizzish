using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class QuestionOutputDto
    {
        public int Id { get; set; }
        public Challenge Challenge { get; set; }
        public string Text { get; set; }
        public IEnumerable<AnswerOutputDto> Answers { get; set; }
        public string Difficulty { get; set; }
        public Category Category { get; set; }
    }
}
