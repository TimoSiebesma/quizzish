using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class AnswerInputDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public bool Result { get; set; }
    }
}
