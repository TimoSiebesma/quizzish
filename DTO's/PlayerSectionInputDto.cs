using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.DTO_s
{
    public class PlayerSectionInputDto
    {




        [Required]
        public int Counter { get; set; } = 0;

        public PlayerInputDto Player { get; set; }

        [Required]
        public int Points { get; set; } = 0;
        [Required]
        public string Answers { get; set; } = "";
    }
}
