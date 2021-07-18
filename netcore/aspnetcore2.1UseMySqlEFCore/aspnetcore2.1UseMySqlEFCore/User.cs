using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore2._1UseMySqlEFCore
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32), Required]
        public string Aaccount { get; set; }

        [MaxLength(32), Required]
        public string Password { get; set; }
    }
}
