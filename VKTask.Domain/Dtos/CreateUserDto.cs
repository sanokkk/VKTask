using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.Domain.Models;

namespace VKTask.Domain.Dtos
{
    public class CreateUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }

        [Range(1,2, ErrorMessage ="Incorrect value")]
        public int UserGroupId { get; set; }
    }
}
