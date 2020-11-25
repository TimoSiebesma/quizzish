using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{
    public class Player : IHaveId
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string IdentityId { get; set; }
        public virtual IEnumerable<PlayerSection> PlayerSections { get; set; }
        public virtual IEnumerable<Score> Scores { get; set; }
    }
}
