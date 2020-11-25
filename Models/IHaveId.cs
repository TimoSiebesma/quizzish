using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{
    public interface IHaveId
    {
        int Id { get; set; }
    }
}
