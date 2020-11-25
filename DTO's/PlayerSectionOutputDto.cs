using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class PlayerSectionOutputDto
    {
        public int Id { get; set; }
        public PlayerOutputDto Player { get; set; }

        public int Counter { get; set; }
        public int Points { get; set; }
        public string Answers { get; set; }

        public Category Category { get; set; }
    }
}
