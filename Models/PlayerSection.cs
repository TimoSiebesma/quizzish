using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{
    public class PlayerSection : IHaveId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        public int ChallengeId { get; set; }
        [ForeignKey("ChallengeId")]
        public virtual Challenge Challenge { get; set; }

        public Category Category => Challenge.Category;
        public int Counter { get; set; }
        public int Points { get; set; }
        public string Answers { get; set; }

        public void ProcessUserAnswer(Answer answer)
        {
            Answers = $"{Answers}-{answer.Id}";
            Counter++;

            if (answer.Result)
            {
                Points++;
            }
        }
        
    }
}
