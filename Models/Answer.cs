using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{
    public class Answer : IHaveId
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get  ;set; }

        public string Text { get; set; }
        public bool Result { get; set; }


    }
}
