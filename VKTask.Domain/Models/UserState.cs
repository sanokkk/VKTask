using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKTask.Domain.Models
{
    public class UserState
    {
        public int Id { get; init; }

        public int Code { get; init; }

        public string Description { get; set; }
    }
}
