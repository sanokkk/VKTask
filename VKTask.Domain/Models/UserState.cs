﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKTask.Domain.Models
{
    public class UserState
    {
        public int UserStateId { get; init; }

        public string Code { get; init; }

        public string Description { get; set; }

        //public ICollection<User> Users { get; set; }
    }
}
