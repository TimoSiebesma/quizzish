using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{
    public class Score : IHaveId
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
        public Category Category { get; set; }
        public int Amount { get; set; }

    }
}
