using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class ScoreInputDto
    {
        public Category Category { get; set; }
        public int Amount { get; set; }
    }
}
